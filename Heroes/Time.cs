namespace Heroes;

using System.Diagnostics;

public class Time
{
    private static Stopwatch _stopwatch = Stopwatch.StartNew();
    private static double _lastTime = GetTime();
    private static double _deltaTime = 0;
    private static double _timeScale = 1;

    /// <summary>
    /// Get the current time in milliseconds.
    /// </summary>
    /// <returns>Current time in milliseconds</returns>
    public static long GetTime()
    {
        return _stopwatch.ElapsedMilliseconds;
    }

    /// <summary>
    /// Calculate the delta time.
    /// </summary>
    internal static void CalculateDeltaTime()
    {
        var currentTime = GetTime();
        _deltaTime = ((currentTime - _lastTime) / 1000.0) * _timeScale;
        _lastTime = currentTime;
    }

    /// <summary>
    /// Get the delta time.
    /// </summary>
    /// <returns>Delta time (double)</returns>
    public static double GetDeltaTime()
    {
        return _deltaTime;
    }

    /// <summary>
    /// Set the time scale.
    /// </summary>
    /// <param name="timeScale">Time scale (double)</param>
    public static void SetTimeScale(double timeScale)
    {
        _timeScale = timeScale;
    }
}
