using Heirloom;
using Heirloom.Desktop;

namespace MovimentImatge;

class Program
{
    private static Window _finestra = null!;
    private static Cavaller _cavaller = null!;
    
    static void Main()
    {
        Application.Run(() =>
        {
            // Crea tots els objectes del programa
            _finestra = new Window("La finestra", (800, 600));
            _finestra.MoveToCenter();
            _cavaller = new Cavaller(10,10);
            
            
            
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
        var rectangleFinestra = new Rectangle(
            0, 0, _finestra.Width, _finestra.Height
            );
        gfx.Clear(Color.Blue);
        _cavaller.Mou(rectangleFinestra);
        _cavaller.Pinta(gfx);
    }
}