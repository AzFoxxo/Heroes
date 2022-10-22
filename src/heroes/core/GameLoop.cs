using Heroes.Core.Logging;

namespace Heroes.Core
{
    public class GameLoop
    {
        private static bool GameRunning = true;
        public static void Kill(string error = "")
        {
            GameRunning = false;
            if (error != "") Logging.Logger.Log(error);
        }


        // Game loop methods
        public void Start()
        {
            // Loader code
            GameCode.Loader.HeroesFSLoader.Load();

            // Create a copy of the list of heroes
            List<Hero> heroes = new(HeroManager.heroes);

            // Update the game loop
            while (GameRunning)
            {
                // Invoke the update method for each hero
                foreach (Hero hero in heroes)
                {
                    hero.OnUpdate(); // Update the hero
                    if (!GameRunning) break; // Quit
                                             // Rebuild the list of heroes
                    if (HeroManager.RebuildList)
                    {
                        heroes = new(HeroManager.heroes);
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
