using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] Color gizmoColor;
    public List<GameObject> nodes;
    List<PathNode> segments;

    private void Start()
    {
        segments = GetSegments();
    }

    public List<PathNode> GetSegments()
    {
        List<PathNode> segments = new List<PathNode>();
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            Vector3 src = nodes[i].transform.position;
            Vector3 dst = nodes[i + 1].transform.position;
            PathNode segment = new PathNode(src, dst);
            segments.Add(segment);
        }
        return segments;
    }

    public float GetParam(Vector3 position, float lastParam)
    {
        float param = 0f;
        PathNode currentSegment = null;
        float tempParam = 0f;
        foreach (PathNode ps in segments)
        {
            tempParam += Vector3.Distance(ps.a, ps.b);
            if (lastParam <= tempParam)
            {
                currentSegment = ps;
                break;
            }
        }
        if (currentSegment == null)
        {
            return 0f;
        }
        Vector3 currPos = position - currentSegment.a;
        Vector3 segmentDirection = currentSegment.b - currentSegment.a;
        segmentDirection.Normalize();
        Vector3 pointInSegment = Vector3.Project(currPos, segmentDirection);
        param = tempParam - Vector3.Distance(currentSegment.a, currentSegment.b);
        param += pointInSegment.magnitude;
        return param;
    }

    public Vector3 GetPosition(float param)
    {
        Vector3 position = Vector3.zero;
        PathNode currentSegment = null;
        float tempParam = 0f;
        foreach (PathNode ps in segments)
        {
            tempParam += Vector3.Distance(ps.a, ps.b);
            if (param <= tempParam)
            {
                currentSegment = ps;
                break;
            }
        }
        if (currentSegment == null)
        {
            return Vector3.zero;
        }
        Vector3 segmentDirection = currentSegment.b - currentSegment.a;
        segmentDirection.Normalize();
        tempParam -= Vector3.Distance(currentSegment.a, currentSegment.b);
        tempParam = param - tempParam;
        position = currentSegment.a + segmentDirection * tempParam;
        return position;
    }

    private void OnDrawGizmos()
    {
        Vector3 direction;
        Color tmp = Gizmos.color;
        Gizmos.color = gizmoColor;
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            Vector3 src = nodes[i].transform.position;
            Vector3 dst = nodes[i + 1].transform.position;
            direction = dst - src;
            Gizmos.DrawRay(src, direction);
        }
        Gizmos.color = tmp;
    }
}