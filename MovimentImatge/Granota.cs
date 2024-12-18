using Heirloom;

namespace MovimentImatge;

public class Granota
{
    private static readonly Random Random = new Random();
    private Vector _posicio;
    private readonly Image _imatge;
    public bool EsPrincesa { get; }
    
    public Granota(Rectangle pantalla, bool esPrincesa = false)
    {
        _imatge = new Image("imatges/granota.png");
        EsPrincesa = esPrincesa;
        Posiciona(pantalla);
    }

    public void Mou(Rectangle finestra)
    {
        var novaPosicio = new Rectangle(_posicio,_imatge.Size);
        novaPosicio.X += Random.Next(-5, 5);
        novaPosicio.Y += Random.Next(-5, 5);
        
        if (finestra.Contains(novaPosicio))
        {
            _posicio.X = novaPosicio.X;
            _posicio.Y = novaPosicio.Y;
        } 
        
    }
    public void Posiciona(Rectangle pantalla)
    {
        int posicio = Random.Next(0, 4);

        var lowx = (int) pantalla.X;
        var lowy = (int) pantalla.Y;
        var loww = (int) pantalla.Width;
        var lowh = (int) pantalla.Height;
        
        switch (posicio)
        {
            case 0: // Dalt 
                lowx = 0;
                loww = (int) pantalla.Width - _imatge.Width;
                lowy = 0;
                lowh = (int) (pantalla.Height * 0.25) - _imatge.Height;
                break;
            case 1: // Baix
                lowx = 0;
                loww = (int) pantalla.Width - _imatge.Width;
                lowy = (int)(pantalla.Height * 0.75);
                lowh = (int) pantalla.Height - _imatge.Height;
                break;
            case 2: // Esquerra
                lowx = 0;
                loww = (int)(pantalla.Width * 0.25) - _imatge.Width;
                lowy = 0;
                lowh = (int) pantalla.Height - _imatge.Height;
                break;
            case 3: // Dreta
                lowx = (int)(pantalla.Width * 0.75) - _imatge.Width;
                loww = (int) pantalla.Width - _imatge.Width;
                lowy = 0;
                lowh = (int) pantalla.Height - _imatge.Height;                
                break;
        }
        
        _posicio.X = Random.Next(lowx, loww);
        _posicio.Y = Random.Next(lowy, lowh);
    }
    
    public Rectangle PosicioActual()
    {
        return new Rectangle(_posicio,_imatge.Size);
    }
    
    public void Pinta(GraphicsContext gfx)
    {
        gfx.DrawImage(_imatge, _posicio);
        
    }
}