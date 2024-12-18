using Heirloom;
using Heirloom.Desktop;

namespace MovimentImatge;

class Program
{
    const int GranotesInicials = 4;
    private static Window _finestra = null!;
    private static Cavaller _cavaller = null!;
    private static List<Granota> _granotes = new();
    
    static void Main()
    {
        Application.Run(() =>
        {
            // Crea tots els objectes del programa
            _finestra = new Window("La finestra", (800, 600));
            _finestra.MoveToCenter();
            _cavaller = new Cavaller(10,10);
            bool princesa = true;
            for (int i = 0; i < GranotesInicials; i++)
            {
                _granotes.Add(new Granota(princesa));
                princesa = false;
            }
            
            var loop = GameLoop.Create(_finestra.Graphics, OnUpdate);
            loop.Start();
        });
    }

    /// <summary>
    /// Bucle del programa. S'executa repetidament
    /// </summary>
    /// <param name="gfx">On pintar les coses</param>
    /// <param name="dt"></param>
    private static void OnUpdate(GraphicsContext gfx, float dt)
    {
        // Fer moviments
        var rectangleFinestra = new Rectangle(
            0, 0, _finestra.Width, _finestra.Height
            );
        _cavaller.Mou(rectangleFinestra);
        foreach (var granota in _granotes)
        {
            if (_cavaller.HaCapturatUnaGranota(granota))
            {
                if (granota.EsPrincesa)
                {
                    Console.WriteLine("Princesa capturada");
                }
                else
                {
                    Console.WriteLine("No és una princesa");
                }
            }
            
        }
        
        // Pinta
        gfx.Clear(Color.Blue);

        _cavaller.Pinta(gfx);
        foreach (var granota in _granotes)
        {
            granota.Pinta(gfx);
        }
        
    }
}