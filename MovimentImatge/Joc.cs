using Heirloom;
using Heirloom.Desktop;

namespace MovimentImatge;

public class Joc
{
    private readonly int _numGranotes;
    private readonly int _vides;
    private string _fase = "playing";
    private static bool _fullScreen;

    private readonly Window _finestra;
    private Cavaller _cavaller = null!;
    private readonly List<Granota> _granotes = new();
    private StaticImage _final = null!;
    private StaticImage _failed = null!;
    private Marcador _marcador = null!;

    
    public Joc(Window espai, int quantesGranotes = 4, int vides = 10, bool fullscreen = false)
    {
        _numGranotes = quantesGranotes;
        _finestra = espai;
        _fullScreen = fullscreen;
        _vides = vides;
    }

    public void Inicialitza() {
            // Mida de la finestra, cal per poder posar els personatges a dins
            var rectangleFinestra = new Rectangle(
                0, 0,
                _finestra.Width, _finestra.Height
            );
            
            // Crea les granotes
            var esPrincesa = true;
            for(var i=0; i < _numGranotes; i++)
            {
                _granotes.Add(new Granota(rectangleFinestra, esPrincesa));
                esPrincesa = false;
            }
            
            // Crea el cavaller
            _cavaller = new Cavaller();
            _cavaller.CentraAlRectangle(rectangleFinestra);
            
            // Pantalles de victòria o derrota
            _final = new StaticImage("imatges/princess.png");
            _failed = new StaticImage("imatges/failed.png", "Press enter to continue...");
            
            // Marcador
            _marcador = new Marcador(_finestra, _vides);
            
            // Comencem jugant!
            _fase = "playing";
    }

    /// <summary>
    /// Comprova si s'han premut tecles que no tenen a veure directament
    /// amb el joc:
    /// - ESC acaba el programa
    /// - F11 passa o treu de pantalla sencera
    /// </summary>
    private void CheckProgramKeys()
    {
        // ESC tanca la finestra
        if (Input.CheckKey(Key.Escape, ButtonState.Pressed))
        {
            _finestra.Close();
        }

        if (Input.CheckKey(Key.F11, ButtonState.Pressed))
        {
            if (_fullScreen)
            {
                _finestra.BeginFullscreen();
            }
            else
            {
                _finestra.EndFullscreen();
            }
            _fullScreen = !_fullScreen;
        }
    }


    public void MouPersonatges() {
        CheckProgramKeys();

        // Dimensions de la pantalla per evitar que quan es moguin
        // surtin de la pantalla
        var rectangleFinestra = new Rectangle(
            0, 0,
            _finestra.Width, _finestra.Height
        );
        
        // Fes el que toca en funció de la fase en que estem
        switch (_fase)
        {
            // El cavaller ha de capturar alguna granota
            case "playing":
                Juga(rectangleFinestra);
                break;
            // Ha capturat una granota que no és la princesa
            // Mostra el cavaller 
            case "failed":
                PantallaError(rectangleFinestra);
                if (_failed.Continue())
                {
                    _fase = _marcador.Vides == 0 ? "game over" : "playing";
                }
                Pinta();
                break;
            // Ha acabat el joc, mostra la imatge segons
            // si ha anat bé o ha fallat
            case "game over":
                if (_marcador.Vides > 0)
                {
                    GameOver(rectangleFinestra);
                }
                else
                {
                    _failed.RemoveText();
                    PantallaError(rectangleFinestra);
                }

                break;
        }
    }

    /// <summary>
    ///  El cavaller i les granotes es mouen fins que en capturi alguna
    /// </summary>
    /// <param name="rectangleFinestra">Mida de la pantalla</param>
    private void Juga(Rectangle rectangleFinestra)
    {
        
        _cavaller.Mou(rectangleFinestra);
        foreach (var granota in _granotes.ToList())
        {
            granota.Mou(rectangleFinestra);
            if (!_cavaller.HaCapturatLaGranota(granota)) continue;
            
            if (granota.EsPrincesa)
            {
                _fase = "game over";
            }
            else
            {
                _fase = "failed";
                _granotes.Add(new Granota(rectangleFinestra));
                foreach (var granoteta in _granotes.ToList())
                {
                    granoteta.Posiciona(rectangleFinestra);
                }
                _marcador.DescomptaVida();
                _cavaller.CentraAlRectangle(rectangleFinestra);
                break;
            }

        }            
        Pinta();
    }

    private void Pinta() {
        _finestra.Graphics.Clear(Color.Blue);
        _finestra.Graphics.PushState(true);
        {
            foreach (var granota in _granotes)
            {
                granota.Pinta(_finestra.Graphics);
            }

            _cavaller.Pinta(_finestra.Graphics);
            _marcador.Pinta();
        }
        _finestra.Graphics.PopState();
    }

    /// <summary>
    /// Pinta la pantalla de victòria
    /// </summary>
    /// <param name="rectangleFinestra"></param>
    private void GameOver(Rectangle rectangleFinestra)
    {
        _finestra.Graphics.Clear(Color.Pink);
        _final.Pinta(_finestra.Graphics, rectangleFinestra);
    }

    // Pinta la pantalla de "ha fallat"
    private void PantallaError(Rectangle rectangleFinestra)
    {
        _finestra.Graphics.Clear(Color.White);
        _failed.Pinta(_finestra.Graphics, rectangleFinestra);
    }
}