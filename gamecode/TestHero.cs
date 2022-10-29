namespace GameCode;
using Heroes;

// A test hero
public class HeroTest : Hero
{
    ulong i = 0; // A counter

    Hero? hero2; // A second hero

    public override void OnStart()
    {
        Print("Welcome to the test hero!");
        hero2 = Create<HeroTest2>();
    }

    // Update method
    public override void OnUpdate()
    {
        // Print a number
        i++;
        Console.WriteLine(i);

        if (i >= 100)
        {
            // Destroy the hero
            Destroy(hero2);

        }

        if (i >= 105)
        {
            var text = Read("Enter some text: ");
            PrintPrompt("You entered: " + text + "\n");
            // App.End();

            // Check if the hero is persistent
            CheckPersistence();

            // Make the hero persistent (disable this and the game will immediately end due to the hero being destroyed)
            // MakePersistent(this, true);

            // Check if the hero is persistent
            CheckPersistence();

            Heroes.Internal.HeroManager.WorldDestroyAllHeroes();

            // Restart the game
            if (text == "restart")
            {
                App.Restart();
            }
        }
    }

    // Check persistence
    private void CheckPersistence()
    {
        // Check if the hero is persistent
        if (IsPersistent(this)) Print("The hero is persistent.");
        else Print("The hero is not persistent.");
    }
}
