using Heroes;

namespace Heroes.Core {
    public struct HeroManager {
        public static List<Hero> heroes = new(); // List of the heroes

        public static bool RebuildList = false; // Rebuild the list of heroes

        // Create a new hero from a class which inherits from the Hero class
        public static void CreateHeroLoader<T>() where T : Hero, new() {
            // Create a new hero
            Hero hero = new T();

            // Add the hero to the list
            heroes.Add(hero);

            // Rebuild the list
            RebuildList = true;
        }
    }
}