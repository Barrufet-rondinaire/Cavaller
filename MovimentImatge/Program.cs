using Heirloom;
using Heirloom.Desktop;

namespace MovimentImatge;

class Program
{
    const int GranotesInicials = 4;
    private static Window _finestra = null!;
    private static Joc _joc = null!;
    
    static void Main()
    {
        Application.Run(() =>
        {
            // Crea tots els objectes del programa
            _finestra = new Window("La finestra", (800, 600));
            _finestra.MoveToCenter();
           _joc = new Joc(_finestra, GranotesInicials);       
           _joc.inicialitza();     
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
        _joc.MouPersonatges();
        _joc.Pinta();            
    }
                   
}
