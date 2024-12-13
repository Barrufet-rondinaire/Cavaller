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


    public void Mou(Rectangle finestra)
    {
        var novaPosicio = new Rectangle(posicio,_imatge.Size);
        if(Input.CheckKey(Key.A, ButtonState.Down))
        {
            novaPosicio.X -= velocitat;
        }
        
        if(Input.CheckKey(Key.D, ButtonState.Down))
        {
            novaPosicio.X += velocitat;
        }
        if(Input.CheckKey(Key.W, ButtonState.Down))
        {
            novaPosicio.Y -= velocitat;
        }
        if(Input.CheckKey(Key.S, ButtonState.Down))
        {
            novaPosicio.Y += velocitat;
        }

        if (finestra.Contains(novaPosicio))
        {
            posicio.X = novaPosicio.X;
            posicio.Y = novaPosicio.Y;
        }
            
    }
}