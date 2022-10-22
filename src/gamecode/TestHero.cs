using Heroes;

namespace GameCode {
    // A test hero
    public class HeroTest : Hero {
        ulong i = 0; // A counter

        Hero hero2; // A second hero

        public override void OnStart() {
            Print("Welcome to the test hero!");
            hero2 = Create<HeroTest2>();
        }

        // Update method
        public override void OnUpdate() {
            // Print a number
            i++;
            Console.WriteLine(i);

            if (i >= 100) {
                // Destroy the hero
                if (hero2 != null) Destroy(hero2);
                
            }

            if (i >= 105) {
                App.End();
            }
        }
    }
}