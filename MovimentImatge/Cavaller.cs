using Heirloom;

namespace MovimentImatge;

public class Cavaller
{
    private readonly Image _imatge;
    private Vector _posicio;
    private readonly int _velocitat;

    public Cavaller(int x, int y)
    {
        _imatge = new Image("imatges/cavaller.png");
        _velocitat = 3;
        _posicio = new Vector(x, y);
    }

    public void Pinta(GraphicsContext gfx)
    {
        gfx.DrawImage(_imatge, _posicio);
        
    }
    
    public void Mou(Rectangle finestra)
    {
        var novaPosicio = new Rectangle(_posicio,_imatge.Size);
        if(Input.CheckKey(Key.A, ButtonState.Down))
        {
            novaPosicio.X -= _velocitat;
        }
        
        if(Input.CheckKey(Key.D, ButtonState.Down))
        {
            novaPosicio.X += _velocitat;
        }
        if(Input.CheckKey(Key.W, ButtonState.Down))
        {
            novaPosicio.Y -= _velocitat;
        }
        if(Input.CheckKey(Key.S, ButtonState.Down))
        {
            novaPosicio.Y += _velocitat;
        }

        if (finestra.Contains(novaPosicio))
        {
            _posicio.X = novaPosicio.X;
            _posicio.Y = novaPosicio.Y;
        }
            
    }

    public bool HaCapturatUnaGranota(Granota granota)
    {
        var rectangleCavaller = new Rectangle(_posicio, _imatge.Size);
        var rectangleGranota = granota.Posicio();

        return rectangleCavaller.Overlaps(rectangleGranota);
    }
}