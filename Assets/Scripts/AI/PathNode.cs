using UnityEngine;

public class PathNode
{
    public Vector3 a;
    public Vector3 b;

    public PathNode() : this(Vector3.zero, Vector3.zero) { }
    public PathNode(Vector3 a, Vector3 b)
    {
        this.a = a;
        this.b = b;
    }
}
