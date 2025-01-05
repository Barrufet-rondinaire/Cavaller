using Heirloom;
using Heirloom.Desktop;

namespace MovimentImatge;

public class Joc
{
    private readonly int _numGranotes;
    private readonly Window _finestra;
    private Cavaller _cavaller = null!;
    private readonly List<Granota> _granotes = new();

    public Joc(Window espai, int quantesGranotes)
    {
        _numGranotes = quantesGranotes;
        _finestra = espai;
    }

    public void Inicialitza() {
        _cavaller = new Cavaller(10,10);
        var princesa = true;
        for (int i = 0; i < _numGranotes; i++)
        {
            _granotes.Add(new Granota(princesa));
            princesa = false;
        }
    }

    public void MouPersonatges() {
        var rectangleFinestra = new Rectangle(
            0, 0, _finestra.Width, _finestra.Height
            );
        _cavaller.Mou(rectangleFinestra);
        foreach (var granota in _granotes)
        {
            if (_cavaller.HaCapturatUnaGranota(granota))
            {
                if (granota.EsPrincesa)
                {
                    Console.WriteLine("Princesa capturada");
                }
                else
                {
                    Console.WriteLine("No és una princesa");
                }
            }
            
        }
    }

    public void Pinta() {
         _finestra.Graphics.Clear(Color.Blue);

        _cavaller.Pinta(_finestra.Graphics);
        foreach (var granota in _granotes)
        {
            granota.Pinta(_finestra.Graphics);
        }
    }
}