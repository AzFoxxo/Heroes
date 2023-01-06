namespace Heroes;

/// <summary> A static class containing maths functions </summary>
public class Maths {
    /// <summary> Linearly interpolate two values </summary>
    /// <param name="start"> The start value </param>
    /// <param name="end"> The end value </param>
    /// <param name="amount"> The amount to interpolate by </param>
    /// <returns> The interpolated value </returns>
    public static float Lerp(float start, float end, float amount) => start + (end - start) * amount;
    public static double Lerp(double start, double end, double amount) => start + (end - start) * amount;
}