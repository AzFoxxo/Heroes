using Heroes;

namespace App;

[AutoInitialise]
public class Player : Hero {
    public override void OnStart() {
        // Create a sprite
        var sprite = Attach<SpriteObj>();
        
        // Set the sprite
        // sprite.SetSprite("Examples/Sprites/Player.png", new Vec2(10, 10), new Vec2(32, 32));
        // sprite.SetSprite("Examples/Sprites/Player.png", new Vec2(10, 10), new Vec2(32, 32));
    }

    public override void OnUpdate() {
        // Movement
        // TODO: Add a way to get the input
    }
}