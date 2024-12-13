using Heirloom;
using Heirloom.Desktop;

namespace MovimentImatge;

class Program
{
    private static Window finestra;
    private static Image cavaller;
    
    static void Main()
    {
        Application.Run(() =>
        {
            finestra = new Window("La finestra", (800, 600));
            cavaller = new Image("imatges/cavaller.png");
            var loop = GameLoop.Create(finestra.Graphics, OnUpdate);
            loop.Start();
        });
    }

    private static void OnUpdate(GraphicsContext gfx, float dt)
    {
        gfx.Clear(Color.Blue);
        gfx.DrawImage(cavaller, new Vector(10,10));
    }
}