#define GRAPHICS
#define GRAPHICS_SDL2

using Heroes.Debug.Paws;
using Heroes.Graphics.Renderers.SDL2;

namespace Heroes.Internal;


public static class Program
{
    // Main loop
    public static GameLoop gameLoop = new();

    // Entry point
    public static void Main(string[] args) {
        // Initialise the Paws logger
        PawsLogger.Setup(true, true, "logs/", Levels.Debug);
        PawsLogger.Info("Paws logger initialised");

        
        #if GRAPHICS
            // Create a new thread for the game loop
            var gameLoopThread = new Thread(gameLoop.Enter);
            gameLoopThread.Start();
            
            #if GRAPHICS_SDL2
                // SDL2 Renderer (game loop is called inside the renderer)
                var renderer = new Renderer();
                renderer.Entry();
            #else
                // Error no valid graphics renderer
                PawsLogger.Error("No valid graphics renderer selected");
            #endif
        #else 
            // Run the game loop
            GameLoop.FreezeExecuting = false; // Unfreeze the game loop
            gameLoop.Enter();
        #endif
    }
}
