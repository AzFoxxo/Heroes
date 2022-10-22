using Heroes.Core.Logging;
using Heroes.Core;

namespace Heroes {
    // Entry point of the framework
    public static class Program {
        public static void Main(string[] args) {
            // Heroes entry point
            Logger.Log("Welcome to the Heroes framework!");
            Logger.Log("Initialisation...");

            // Create a new game loop
            GameLoop gameLoop = new();

            // Start the game loop
            gameLoop.Start();

            // Exit the program
            Logger.Log("Exiting...");
            Logger.Log("Goodbye!");
        }
    }
}