using Heirloom;

namespace MovimentImatge;

public class Cavaller
{
    private Image _imatge;
    private Vector posicio;
    private int velocitat;

    public Cavaller(int x, int y)
    {
        _imatge = new Image("imatges/cavaller.png");
        velocitat = 3;
        posicio = new Vector(x, y);
    }

    public void Pinta(GraphicsContext gfx)
    {
        gfx.DrawImage(_imatge, posicio);
        
    }


    public void Mou()
    {
        if(Input.CheckKey(Key.A, ButtonState.Down))
        {
            posicio.X -= velocitat;
        }
        
        if(Input.CheckKey(Key.D, ButtonState.Down))
        {
            posicio.X += velocitat;
        }
        if(Input.CheckKey(Key.W, ButtonState.Down))
        {
            posicio.Y -= velocitat;
        }
        if(Input.CheckKey(Key.S, ButtonState.Down))
        {
            posicio.Y += velocitat;
        }
            
    }
}