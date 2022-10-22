using Heroes;

namespace Heroes.Core {
    public struct HeroManager {
        public static List<Hero> heroes = new(); // List of the heroes

        public static bool RebuildList = false; // Rebuild the list of heroes

        // Create a new hero from a class which inherits from the Hero class
        public static void CreateHero<T>() where T : Hero, new() {
            // Create a new hero
            Hero hero = new T();

            // Add the hero to the list
            heroes.Add(hero);

            // Rebuild the list
            RebuildList = true;
        }

        // Destroy a hero
        public static void DestroyHero(Hero hero) {
            // Run de-initialisation methods
            hero.OnDestroy();

            // Remove the hero from the list of heroes
            heroes.Remove(hero);

            // Rebuild the list of heroes
            RebuildList = true;

            // If there are no more heroes, kill the game
            if (heroes.Count == 0) GameLoop.Kill("No more heroes!");
        }
    }
}