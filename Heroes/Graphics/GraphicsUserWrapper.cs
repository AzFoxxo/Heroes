using Heroes.Graphics;
using Heroes.Graphics.Renderers.SDL2;

namespace Heroes;


// TODO: MUST OF THESE RELY ON THE SDL2 RENDERER SO MUST BE RUN IN THE SDL2 RENDERER THREAD

public static class Window
{
    public static void SetTitle(string title) => Renderer.Instance!.WindowSetTitle(title);
    public static string GetTitle() => Renderer.Instance!.WindowGetTitle();
    public static void SetSize(int width, int height) => Renderer.Instance!.WindowSetSize(width, height);
    public static (int width, int height) GetSize() {
        var size = Renderer.Instance!.WindowGetSize();
        
        // Index 0 is width, 1 is height
        return (size[0], size[1]);
    }

    public static void SetFullscreen(bool fullscreen) => Renderer.Instance!.WindowSetFullscreen(fullscreen);
    public static bool GetFullscreen() => Renderer.Instance!.WindowGetFullscreen();
}

public static class RendererGraphics {
    /// <summary> Set the background colour </summary>
    /// <param name="colour"> The colour to set the background to </param>
    public static void SetBackgroundColour(SDLColour colour) => Renderer.Instance!.SetBackgroundColour(colour);
    
    /// <summary> Get the background colour </summary>
    /// <returns> The background colour </returns>
    public static SDLColour GetBackgroundColour() => Renderer.Instance!.GetBackgroundColour();

    /// <summary> Load and add a sprite to the renderer (a takes one cycle due to queueing the laod) </summary>
    /// <param name="path"> The path to the sprite </param>
    /// <param name="x"> The x position of the sprite </param>
    /// <param name="y"> The y position of the sprite </param>
    /// <param name="width"> The width of the sprite </param>
    /// <param name="height"> The height of the sprite </param>
    /// <returns> The sprite </returns>
    public static Sprite LoadSprite(string path, int x, int y, int width, int height) {
        // Create the sprite
        var sprite = new Sprite(-1, x, y, width, height);
        
        // Enqueue the texture
        // Will update the sprite once the texture is loaded
        Renderer.Instance!.EnqueueTexture(path, sprite);

        Renderer.Instance.AddSprite(sprite);

        return sprite;
    }
}