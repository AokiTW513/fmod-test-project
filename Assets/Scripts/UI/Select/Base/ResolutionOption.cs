using UnityEngine;

[System.Serializable]
public class ResolutionOption
{
    public int width;
    public int height;

    public override string ToString()
    {
        return width + " x " + height;
    }
}