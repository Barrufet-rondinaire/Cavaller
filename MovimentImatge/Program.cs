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

    private static Joc _joc = null!;
    
    private static bool FullScreen = false;
    
    static void Main()
    {
        Application.Run(() =>
        {
            // Crea la finestra
            _finestra = new Window("La princesa granota", (Amplada, Altura)) { IsResizable = false };
            _finestra.MoveToCenter();
            
            _joc = new Joc(_finestra, Numgranotes, Vides, FullScreen);
            _joc.inicialitza();
            
            var loop = GameLoop.Create(_finestra.Graphics, OnUpdate);
            loop.Start();
        });
    }

    private static void OnUpdate(GraphicsContext gfx, float dt)
    {
        _joc.MouPersonatges();
    }



}