using Heirloom;

namespace MovimentImatge;

public class StaticImage
{
    private readonly Image _imatge;
    private string _textToPrint;


    public StaticImage(string imageName, string text = "")
    {
        _imatge = new Image(imageName);
        _textToPrint = text;
        
    }
    
    // public void Centra(Rectangle rectangleFinestra)
    // {
    //     _posicio.X = (rectangleFinestra.Width - _imatge.Width) / 2;
    //     _posicio.Y = (rectangleFinestra.Height - _imatge.Height) / 2;
    // }

    public Rectangle AjustaPosicio(Rectangle rectangleFinestra)
    {
        var proporcio = (rectangleFinestra.Height / _imatge.Height);

        var rectangleNovaPosicio = new Rectangle(new Vector(0, 0), _imatge.Size);
        rectangleNovaPosicio.Inflate(proporcio);
        rectangleNovaPosicio.X = (rectangleFinestra.Width - _imatge.Width) / 2;
        rectangleNovaPosicio.Y = (rectangleFinestra.Height - _imatge.Height) / 2;

        return rectangleNovaPosicio;
    }
    
    public void Pinta(GraphicsContext gfx, Rectangle pantalla)
    {
        // Centra(pantalla);
        gfx.DrawImage(_imatge, AjustaPosicio(pantalla));
        var posicio = ((Vector)gfx.Surface.Size)* 0.70f;
        gfx.DrawText(_textToPrint, 
            posicio,
            Font.Default,
            30,
            TextAlign.Right | TextAlign.Top);
    }

    public bool Continue()
    {
        return Input.CheckKey(Key.Enter, ButtonState.Down);
    }

    public void RemoveText()
    {
        _textToPrint = "";
    }
}