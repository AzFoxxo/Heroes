using Heroes;

namespace App.Actors;

// [AutoInitialise]
internal class Sandbox : Hero
{
    private double _amount = 0;
    private bool _reached = false;

    // Called when the game starts
    public override void OnStart()
    {
        PrintLine((object)"Hello, world!");

        // Register a ton of components
        Attach<Transformation>();
        Attach<Transformation>();
        Attach<Transformation2D>();
        Attach<Transformation2D>();
        Attach<Transformation2D>();

        // Delete one of 2D transformations
        DeleteAttachable<Transformation2D>();

        // Get a list of all components
        var components = GetAttachables();

        // Loop through the components and print their names
        GayPrint("Components: ");
        // Length
        GayPrint("Length: " + components!.Length);
        foreach (var component in components!)
        {
            if (component == null) PrintLine((object)"null");
            else GayPrint(component.GetType().Name);
        }

        // Get a list of all components of type Transformation2D
        var components2D = GetAttachables<Transformation2D>();

        // Loop through the components and print their names
        GayPrint("Components2D: ");
        foreach (var component in components2D!)
        {
            PrintLine(component);
        }

        // Delete all components
        DeleteAllAttachable();

        // Check there are no components
        if (GetAttachables() != null) PrintLine((object)"No components");

        // Create a transformation and transformation 2D
        var transformation = Attach<Transformation>();
        var transformation2D = Attach<Transformation2D>();

        // Set the position of the transformation
        PrintLine(transformation);
        PrintLine(transformation2D);

        // Set the position of the transformation 2D
        transformation2D.position = new(10, 10);
        PrintLine(transformation2D);

        // Set the position of the transformation
        transformation.position = new(69, 69, 621);
        PrintLine(transformation);

        // Set the position of the transformation 2D to the transformation
        var pos = transformation.position;
        transformation2D.position = new(pos.x, pos.y);
        PrintLine(transformation2D);

        // Assign transformation position to random values
        transformation.position = new(420, 420, 420);
        PrintLine(transformation);

        // Set the position of the transformation to the transformation 2D
        transformation.position = transformation2D.position;
        PrintLine(transformation);


        // Test vectors (2D)
        var vec = new Vec2(10, 10);
        GayPrint("Vec2: " + vec);
        // Print the length of the vector
        PrintLine((object)$"Length: {Vec2.Length(vec)}");
        // Print the normalized vector
        PrintLine((object)$"Normalised: {Vec2.Normalize(vec)}");
        // Print the dot product of the vector and itself
        PrintLine((object)$"Dot: {Vec2.Dot(vec, vec)}");
        // Print the cross product of the vector and itself
        PrintLine((object)$"Cross: {Vec2.Cross(vec, vec)}");
        // Print the angle between the vector and itself
        PrintLine((object)$"Angle: {Vec2.Angle(vec, vec)}");
        // Print the distance between the vector and itself
        PrintLine((object)$"Distance: {Vec2.Distance(vec, vec)}");

        // Test vectors (3D)
        var vec3 = new Vec3(10, 10, 10);
        GayPrint("Vec3: " + vec3);
        // Print the length of the vector
        PrintLine((object)$"Length: {Vec3.Length(vec3)}");
        // Print the normalized vector
        PrintLine((object)$"Normalised: {Vec3.Normalize(vec3)}");
        // Print the dot product of the vector and itself
        PrintLine((object)$"Dot: {Vec3.Dot(vec3, vec3)}");
        // Print the cross product of the vector and itself
        PrintLine((object)$"Cross: {Vec3.Cross(vec3, vec3)}");
        // Print the angle between the vector and itself
        PrintLine((object)$"Angle: {Vec3.Angle(vec3, vec3)}");
        // Print the distance between the vector and itself
        PrintLine((object)$"Distance: {Vec3.Distance(vec3, vec3)}");

        // Vector math
        var vec1 = new Vec2(10, 10);
        var vec2 = new Vec2(20, 20);
        GayPrint("Vector Maths 1:");
        GayPrint("Vec1: " + vec1);
        GayPrint("Vec2: " + vec2);

        // Add
        PrintLine((object)$"Add: {vec1 + vec2}");
        // Subtract
        PrintLine((object)$"Subtract: {vec1 - vec2}");
        // Multiply
        PrintLine((object)$"Multiply: {vec1 * vec2}");
        // Divide
        PrintLine((object)$"Divide: {vec1 / vec2}");
    }

    // Early update
    public override void OnEarlyUpdate()
    {
        // Exit if reached
        if (_reached) return;

        // Display executing early update
        GayPrint("Early update");
    }

    // Late update
    public override void OnLateUpdate()
    {
        // Exit if reached
        if (_reached) return;

        // Display executing late update
        GayPrint("Late update");
    }


    // Called every frame
    public override void OnUpdate()
    {
        // Exit if reached
        if (_reached) return;


        // Increment the amount
        _amount += Time.GetDeltaTime();

        // Print the amount
        PrintLine(_amount);

        if (_amount >= 1) {
            Time.SetTimeScale(0.1);

            if (_amount >= 1.1) {
                Time.SetTimeScale(1);

                // Re-run the start method
                OnStart();

                // Set reached to true
                _reached = true;

                // Create the test attachable
                var CountTo10Com = Attach<Attachments.CountTo10Com>();

                // End the application
                // Application.Quit();

                // End the application by destroying the hero
                // Destroy(this);
            }

        }

        // Worlds
        // Heroes.Internal.HeroManager.WorldLoadHero<World1>("World1");
        
    }

    // Print the the transformation
    private void PrintLine(Transformation transformation)
    {
        GayPrint("Transformation: ");
        PrintLine((object)$"Position: {transformation.position.x}, {transformation.position.y}, {transformation.position.z}");
        PrintLine((object)$"Rotation: {transformation.rotation.x}, {transformation.rotation.y}, {transformation.rotation.z}");
        PrintLine((object)$"Scale: {transformation.scale.x}, {transformation.scale.y}, {transformation.scale.z}");
    }
    private void Print(Transformation2D transformation)
    {
        GayPrint("Transformation2D: ");
        PrintLine((object)$"Position: {transformation.position.x}, {transformation.position.y}");
        PrintLine((object)$"Rotation: {transformation.angle}");
        PrintLine((object)$"Scale: {transformation.scale.x}, {transformation.scale.y}");
    }
}
