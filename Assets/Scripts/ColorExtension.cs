using UnityEngine;

public static class ColorExtension
{
    public static Color GetRandomColor()
    {
        return Random.ColorHSV(
            hueMin: 0f, hueMax: 1f,
            saturationMin: 0.65f, saturationMax: 1f,
            valueMin: 0.75f, valueMax: 1f,
            alphaMin: 1f, alphaMax: 1f
        );
    }
}
