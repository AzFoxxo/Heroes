using Heroes.Graphics.Renderers.SDL2;

namespace Heroes;

public class SpriteObj : Attachables.Attachable {
    Graphics.Renderers.SDL2.Sprite sprite;

    bool isLoaded = false;

    // Load the texture
    public void SetSprite(string image, Vec2 pos, Vec2 size) {
        // // Load an image into memory
        var texture = Renderer.Instance.LoadImage("Examples/Sprites/Player.png");

        // // Add a sprite to the renderer
        Renderer.Instance.AddSprite(new Heroes.Graphics.Renderers.SDL2.Sprite(texture, 0, 0, 32, 32));
    }

    // Update the sprite
    public override void Update()
    {
        // Check sprite is loaded
        if (!isLoaded) {

            // Set loaded to true
            isLoaded = true;
        }

        // Call the modify function
        
    }
}