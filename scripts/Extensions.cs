using Godot;

namespace Cirllet2;

public static class Extensions
{
    public static Vector2 ToVector2(this Vector2I vector2I) => new(vector2I.X, vector2I.Y);

    public static T FindParent<T>(this Node node) where T: Node
    {
        var parent = node.GetParent<T>();
        if (parent != null) return parent;
        var parent2 = node.GetParent();
        if (parent2 == null) return null;
        return parent2.FindParent<T>();
    }
}