using Heirloom;
using Heirloom.Desktop;

namespace MovimentImatge;

public class Joc
{
    private int numGranotes = 0;
    private Window finestra;
    private Cavaller _cavaller = null!;
    private List<Granota> _granotes = new();

    public Joc(Window espai, int quantesGranotes)
    {
        numGranotes = quantesGranotes;
        finestra = espai;
    }

    public void inicialitza() {
        _cavaller = new Cavaller(10,10);
        bool princesa = true;
        for (int i = 0; i < numGranotes; i++)
        {
            _granotes.Add(new Granota(princesa));
            princesa = false;
        }
    }

    public void MouPersonatges() {
        var rectangleFinestra = new Rectangle(
            0, 0, finestra.Width, finestra.Height
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
         finestra.Graphics.Clear(Color.Blue);

        _cavaller.Pinta(finestra.Graphics);
        foreach (var granota in _granotes)
        {
            granota.Pinta(finestra.Graphics);
        }
    }
}