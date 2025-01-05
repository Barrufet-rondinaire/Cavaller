using Heirloom;
using Heirloom.Desktop;

namespace MovimentImatge;

public class Marcador
{
    public int Vides { get; set; }
    private readonly Window _pantalla;

    public Marcador(Window pantalla, int vides)
    {
        this._pantalla = pantalla;
        Vides = vides;
    }

    public void DescomptaVida()
    {
        Vides--;
    }
    public void Pinta()
    {
        var posicio = ((Vector)_pantalla.Graphics.Surface.Size)* 0.95f;
        _pantalla.Graphics.DrawText($"Et queden {Vides} vides", 
            posicio,
            Font.Default,
            30,
            TextAlign.Right | TextAlign.Top);
    }
}