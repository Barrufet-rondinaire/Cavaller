using Heirloom;

namespace MovimentImatge;

public class Cavaller
{
    private readonly Image _imatge;
    private Vector _posicio;
    private readonly int _velocitat;

    public Cavaller(int x = 0, int y = 0)
    {
        _imatge = new Image("imatges/cavaller.png");
        _velocitat = 3;
        _posicio = new Vector(x, y);
    }

    public void Pinta(GraphicsContext gfx)
    {
        gfx.DrawImage(_imatge, _posicio);
        
    }

    public Rectangle PosicioActual()
    {
        return new Rectangle(_posicio,_imatge.Size);
    }
    public void Mou(Rectangle finestra)
    {
        var novaPosicio = new Rectangle(_posicio,_imatge.Size);
        if(Input.CheckKey(Key.A, ButtonState.Down) || Input.CheckKey(Key.Left, ButtonState.Down))
        {
            novaPosicio.X -= _velocitat;
        }
        
        if(Input.CheckKey(Key.D, ButtonState.Down) || Input.CheckKey(Key.Right, ButtonState.Down))
        {
            novaPosicio.X += _velocitat;
        }
        if(Input.CheckKey(Key.W, ButtonState.Down) || Input.CheckKey(Key.Up, ButtonState.Down))
        {
            novaPosicio.Y -= _velocitat;
        }
        if(Input.CheckKey(Key.S, ButtonState.Down) || Input.CheckKey(Key.Down, ButtonState.Down))
        {
            novaPosicio.Y += _velocitat;
        }

        if (finestra.Contains(novaPosicio))
        {
            _posicio.X = novaPosicio.X;
            _posicio.Y = novaPosicio.Y;
        }
            
    }

    public bool HaCapturatLaGranota(Granota granota)
    {
        var rectCavaller = PosicioActual();
        var rectGranota = granota.PosicioActual();
        return rectCavaller.Overlaps(rectGranota);
    }

    public void CentraAlRectangle(Rectangle rectangleFinestra)
    {
        _posicio.X = (rectangleFinestra.Width - _imatge.Width) / 2;
        _posicio.Y = (rectangleFinestra.Height - _imatge.Height) / 2;
    }
}