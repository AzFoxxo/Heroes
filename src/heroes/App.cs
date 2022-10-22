using Heroes.Core;

namespace Heroes {
    // Application class
    public static class App {
        ///<summary>Quit the application.</summary>
        public static void End() => GameLoop.running = false;

    }
}