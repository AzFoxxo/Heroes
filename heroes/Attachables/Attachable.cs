namespace Heroes.Attachables;

public class Attachable : Common {

    // Parent hero
    public Hero? ParentHero { get; private set; }

    // Constructor
    public Attachable() {
        // Run initialisation code
        Initialisation();
    }

    public virtual void Initialisation() {}
    public virtual void Update() {}
    public virtual void Destroy() {}
}