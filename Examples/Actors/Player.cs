using Heroes;
using Heroes.Graphics.Renderers.SDL2;

namespace App;

[AutoInitialise]
public class Player : Hero
{
    // Lerp variables
    double startTime = 0;

    Sprite self;

    public override void OnStart()
    {
        // Set the window title
        Window.SetTitle("Player");

        // Set the window size
        Window.SetSize(800, 600);

        // Set the background colour
        RendererGraphics.SetBackgroundColour(new Heroes.Graphics.SDLColour(69, 69, 69, 255));

        // Load a sprite
        self = RendererGraphics.LoadSprite("Examples/Sprites/Player.png", 0, 0, 32, 32);

        // Set fullscreen
        // Window.SetFullscreen(true);

        // Set the current time
        startTime = Time.GetTime();
    }

    public override void OnUpdate()
    {
        // If one second has passed, move x by 1
        if (Time.GetTime() - startTime >= 10)
        {
            // Set the current time
            startTime = Time.GetTime();

            // Move x by 1
            self.X += 1;
        }
        

        // Movement
        // TODO: Add a way to get the input
    }
}