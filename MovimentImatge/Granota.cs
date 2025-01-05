using Heirloom;

namespace MovimentImatge;

public class Granota
{
    private readonly Random _random = new Random();
    private Vector _posicio;
    private Image _imatge;
    public bool EsPrincesa { get; }

    public Granota(bool esPrincesa)
    {
        _imatge = new Image("imatges/granota.png");
        _posicio = new Vector(_random.Next(800 - _imatge.Width)
                    ,_random.Next(600) - _imatge.Height);
        EsPrincesa = esPrincesa;

    }

    public Rectangle Posicio()
    {
        return new Rectangle(_posicio, _imatge.Size);
    }


    public void Pinta(GraphicsContext gfx)
    {
        gfx.DrawImage(_imatge, _posicio);
    }
}