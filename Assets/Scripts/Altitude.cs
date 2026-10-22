/// <summary>
/// Holds the player's current altitude.
///
/// Because `Value` is `static`, any script anywhere can read or
/// write it by typing `Altitude.Value` (same idea as
/// `Clickable.Clicks`). Whatever object controls the altitude
/// should set it, e.g. `Altitude.Value = 120f;`
/// </summary>
public static class Altitude
{
    public static float Value = 0f;
    public static float Max = 180f; // the balloon can't go higher than this
}
