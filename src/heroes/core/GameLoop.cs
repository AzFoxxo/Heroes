using Heroes.Core.Logging;

namespace Heroes.Core
{
    public class GameLoop
    {
        public static bool running = true;


        // Game loop methods
        public void Start()
        {
            // Loader code
            GameCode.Loader.HeroesFSLoader.Load();

            // Create a copy of the list of heroes
            List<Hero> heroes = new(HeroManager.heroes);

            // Update the game loop
            while (running)
            {
                // Invoke the update method for each hero
                foreach (Hero hero in heroes)
                {
                    hero.OnUpdate(); // Update the hero

                    if (!running) break; // Quit
                    
                    // Rebuild the list of heroes
                    if (HeroManager.RebuildList)
                    {
                        // Rebuild the copied list of heroes
                        heroes = new(HeroManager.heroes);

                        // Reset the rebuild list
                        HeroManager.RebuildList = false;
                    }
                }
            }

            // Invoke the destroy method for each hero
            foreach (Hero hero in HeroManager.heroes)
            {
                hero.OnDestroy();
            }
        }
    }
}
