namespace Heroes {
    // Hero object class (base class)
    public class Hero : Common { // Inherits from Common so print method is available, etc.
        // Properties
        

        // Hero object constructor
        public Hero() {
            // Hero early start method
            OnEarlyStart();

            // Hero start method
            OnStart();
        }

        // Events
        public virtual void OnEarlyStart() {}
        public virtual void OnStart() {}
        public virtual void OnUpdate() {}
        public virtual void OnDestroy() {}

        // Methods
        ///<summary>Create a hero</summary>
        ///<param name="hero">The hero to create</param>
        ///<returns>The created hero</returns>
        public static Hero Create<T>() where T : Hero, new() {
            // Create a new hero
            Hero hero = new T();

            // Add the hero to the list of heroes
            Heroes.Core.HeroManager.heroes.Add(hero);

            // Rebuild the list of heroes
            Heroes.Core.HeroManager.RebuildList = true;

            // Return the hero
            return hero;
        }
        ///<summary>Destroy a hero</summary>
        ///<param name="hero">The hero to destroy</param>
        public static void Destroy(Hero hero) {
            // Run de-initialisation methods
            hero.OnDestroy();

            // Remove the hero from the list of heroes
            Heroes.Core.HeroManager.heroes.Remove(hero);

            // Rebuild the list of heroes
            Heroes.Core.HeroManager.RebuildList = true;

            // If there are no more heroes, kill the game loop
            if (Heroes.Core.HeroManager.heroes.Count == 0) App.End();
        }
    }
}