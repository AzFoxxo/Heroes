namespace App;
using Heroes;

// A test hero
public class HeroTest2 : Hero
{
    // Update method
    public override void OnUpdate()
    {
        // Print a number
        Print("Hello, World from TestHero2!");
    }
}