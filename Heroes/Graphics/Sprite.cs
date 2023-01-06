/// <summary> Sprite </summary>
public class Sprite
{
    public IntPtr Texture { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int SizeX { get; set; }
    public int SizeY { get; set; }

    public Sprite(IntPtr texture, int x, int y, int sizeX, int sizeY)
    {
        Texture = texture;
        X = x;
        Y = y;
        SizeX = sizeX;
        SizeY = sizeY;
    }
}