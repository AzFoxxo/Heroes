using Heroes;

namespace GameCode {
    // A test hero
    public class HeroTest : Hero {
        ulong i = 0;

        public override void OnStart() {
            Console.WriteLine("Hello, world!");
        }

        // Update method
        public override void OnUpdate() {
            // Print a number
            i++;
            Console.WriteLine(i);

            if (i >= 10000) {
                // Destroy the hero
                Heroes.Core.HeroManager.DestroyHero(this);
                // Heroes.Core.GameLoop.Kill();
            }
        }
    }
}