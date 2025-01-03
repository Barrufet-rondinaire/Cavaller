using Heirloom;
using Heirloom.Desktop;

namespace MovimentImatge;

public class Joc
{
    private int numGranotes = 0;
    private int Vides = 10;

    private string _fase = "playing";
    private static bool FullScreen = false;

    private Window finestra;
    private Cavaller _cavaller = null!;
    private List<Granota> _granotes = new();
    
    private StaticImage _final = null!;
    private StaticImage _failed = null!;

    private Marcador _marcador = null!;



    public Joc(Window espai, int quantesGranotes = 4, int vides = 10, bool fullscreen = false)
    {
        numGranotes = quantesGranotes;
        finestra = espai;
        FullScreen = fullscreen;
    }

    public void inicialitza() {
            // Mida de la finestra, cal per poder posar els personatges a dins
            var rectangleFinestra = new Rectangle(
                0, 0,
                finestra.Width, finestra.Height
            );
            
            // Crea les granotes
            var esPrincesa = true;
            for(var i=0; i < numGranotes; i++)
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
            _marcador = new Marcador(finestra, Vides);
            
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
            finestra.Close();
        }

        if (Input.CheckKey(Key.F11, ButtonState.Pressed))
        {
            if (FullScreen)
            {
                finestra.BeginFullscreen();
            }
            else
            {
                finestra.EndFullscreen();
            }
            FullScreen = !FullScreen;
        }
    }


    public void MouPersonatges() {
        CheckProgramKeys();

        // Dimensions de la pantalla per evitar que quan es moguin
        // surtin de la pantalla
        var rectangleFinestra = new Rectangle(
            0, 0,
            finestra.Width, finestra.Height
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
    /// <param name="gfx"></param>
    /// <param name="dt"></param>
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

    public void Pinta() {
        finestra.Graphics.Clear(Color.Blue);
        finestra.Graphics.PushState(true);
        {
            foreach (var granota in _granotes)
            {
                granota.Pinta(finestra.Graphics);
            }

            _cavaller.Pinta(finestra.Graphics);
            _marcador.Pinta();
        }
        finestra.Graphics.PopState();
    }

    /// <summary>
    /// Pinta la pantalla de victòria
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="rectangleFinestra"></param>
    private void GameOver(Rectangle rectangleFinestra)
    {
        finestra.Graphics.Clear(Color.Pink);
        _final.Pinta(finestra.Graphics, rectangleFinestra);
    }

    // Pinta la pantalla de "ha fallat"
    private void PantallaError(Rectangle rectangleFinestra)
    {
        finestra.Graphics.Clear(Color.White);
        _failed.Pinta(finestra.Graphics, rectangleFinestra);
    }
}