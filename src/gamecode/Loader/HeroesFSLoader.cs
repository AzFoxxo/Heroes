using Heroes.Core;

// The framework looks for GameCode.Loader.HeroesFSLoader.Load() to load the gamecode
// This code should be in a file called src\gamecode\Loader\HeroesFSLoader.cs
// It is responsible for creating the heroes present when the game first starts up

namespace GameCode.Loader {
    public class HeroesFSLoader {
        public static void Load() {

            // Create a hero
            HeroManager.CreateHero<HeroTest>();
        }
    }
}