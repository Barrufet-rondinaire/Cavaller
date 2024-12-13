using Heirloom;
using Heirloom.Desktop;

namespace MovimentImatge;

class Program
{
    private static Window finestra;
    private static Cavaller cavaller;
    
    static void Main()
    {
        Application.Run(() =>
        {
            finestra = new Window("La finestra", (800, 600));
            finestra.MoveToCenter();
            cavaller = new Cavaller(10,10);
            var loop = GameLoop.Create(finestra.Graphics, OnUpdate);
            loop.Start();
        });
    }

    private static void OnUpdate(GraphicsContext gfx, float dt)
    {
        var rectangleFinestra = new Rectangle(
            0, 0, finestra.Width, finestra.Height
            );
        gfx.Clear(Color.Blue);
        cavaller.Mou(rectangleFinestra);
        cavaller.Pinta(gfx);
    }
}