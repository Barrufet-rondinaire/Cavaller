using Heirloom;
using Heirloom.Desktop;

namespace MovimentImatge;

class Program
{
    private const int Amplada = 800;
    private const int Altura = 600;
    private const int Numgranotes = 4;
    private const int Vides = 10;
    private  static Window _finestra = null!;
    private static StaticImage _final = null!;
    private static StaticImage _failed = null!;
    private static Cavaller _cavaller = null!;
    private static readonly List<Granota> Granotes = new();
    private static Marcador _marcador = null!;
    private static string _fase = "playing";
    
    private static bool FullScreen = false;
    
    static void Main()
    {
        Application.Run(() =>
        {
            // Crea la finestra
            _finestra = new Window("La princesa granota", (Amplada, Altura)) { IsResizable = false };
            _finestra.MoveToCenter();
            
            // Mida de la finestra, cal per poder posar els personatges a dins
            var rectangleFinestra = new Rectangle(
                0, 0,
                _finestra.Width, _finestra.Height
            );
            
            // Crea les granotes
            var esPrincesa = true;
            for(var i=0; i < Numgranotes; i++)
            {
                Granotes.Add(new Granota(rectangleFinestra, esPrincesa));
                esPrincesa = false;
            }
            
            // Crea el cavaller
            _cavaller = new Cavaller();
            _cavaller.CentraAlRectangle(rectangleFinestra);
            
            // Pantalles de victòria o derrota
            _final = new StaticImage("imatges/princess.png");
            _failed = new StaticImage("imatges/failed.png", "Press enter to continue...");
            
            // Marcador
            _marcador = new Marcador(_finestra, Vides);
            
            // Comencem jugant!
            _fase = "playing";
            
            var loop = GameLoop.Create(_finestra.Graphics, OnUpdate);
            loop.Start();
        });
    }

    private static void OnUpdate(GraphicsContext gfx, float dt)
    {
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
                OnUpdatePlaying(gfx, dt, rectangleFinestra);
                break;
            // Ha capturat una granota que no és la princesa
            // Mostra el cavaller 
            case "failed":
                OnPaintFailed(gfx, rectangleFinestra);
                if (_failed.Continue())
                {
                    _fase = _marcador.Vides == 0 ? "game over" : "playing";
                }
                OnPaint(gfx, dt);
                break;
            // Ha acabat el joc, mostra la imatge segons
            // si ha anat bé o ha fallat
            case "game over":
                if (_marcador.Vides > 0)
                {
                    OnPaintFinal(gfx, rectangleFinestra);
                }
                else
                {
                    _failed.RemoveText();
                    OnPaintFailed(gfx, rectangleFinestra);
                }

                break;
        }
    }

    /// <summary>
    /// Comprova si s'han premut tecles que no tenen a veure directament
    /// amb el joc:
    /// - ESC acaba el programa
    /// - F11 passa o treu de pantalla sencera
    /// </summary>
    private static void CheckProgramKeys()
    {
        // ESC tanca la finestra
        if (Input.CheckKey(Key.Escape, ButtonState.Pressed))
        {
            _finestra.Close();
        }

        if (Input.CheckKey(Key.F11, ButtonState.Pressed))
        {
            if (FullScreen)
            {
                _finestra.BeginFullscreen();
            }
            else
            {
                _finestra.EndFullscreen();
            }
            FullScreen = !FullScreen;
        }
    }

     
    /// <summary>
    ///  El cavaller i les granotes es mouen fins que en capturi alguna
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="dt"></param>
    /// <param name="rectangleFinestra">Mida de la pantalla</param>
    private static void OnUpdatePlaying(GraphicsContext gfx, float dt, Rectangle rectangleFinestra)
    {
        
        _cavaller.Mou(rectangleFinestra);
        foreach (var granota in Granotes.ToList())
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
                Granotes.Add(new Granota(rectangleFinestra));
                foreach (var granoteta in Granotes.ToList())
                {
                    granoteta.Posiciona(rectangleFinestra);
                }
                _marcador.DescomptaVida();
                _cavaller.CentraAlRectangle(rectangleFinestra);
                break;
            }

        }            
        OnPaint(gfx, dt);
    }

    /// <summary>
    /// Pinta el cavaller i les granotes per pantalla
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="dt"></param>
    private static void OnPaint(GraphicsContext gfx, float dt)
    {
        gfx.Clear(Color.Blue);
        gfx.PushState(true);
        {
            foreach (var granota in Granotes)
            {
                granota.Pinta(gfx);
            }

            _cavaller.Pinta(gfx);
            _marcador.Pinta();
        }
        gfx.PopState();
    }

    /// <summary>
    /// Pinta la pantalla de victòria
    /// </summary>
    /// <param name="gfx"></param>
    /// <param name="rectangleFinestra"></param>
    private static void OnPaintFinal(GraphicsContext gfx, Rectangle rectangleFinestra)
    {
        gfx.Clear(Color.Pink);
        _final.Pinta(gfx, rectangleFinestra);
    }

    // Pinta la pantalla de "ha fallat"
    private static void OnPaintFailed(GraphicsContext gfx, Rectangle rectangleFinestra)
    {
        gfx.Clear(Color.White);
        _failed.Pinta(gfx, rectangleFinestra);
    }
}