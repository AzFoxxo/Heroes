namespace Heroes {
    // Hero object class (base class)
    public class Hero {
        // Properties
        

        // Hero object constructor
        public Hero() {
            // Hero early start method
            OnEarlyStart();

            // Hero start method
            OnStart();
        }

        public virtual void OnEarlyStart() {}
        public virtual void OnStart() {}
        public virtual void OnUpdate() {}
        public virtual void OnDestroy() {}
    }
}