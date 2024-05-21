using UnityEngine;

public class SelfDrivingCar : Seek
{
    public Path path;
    public float pathOffset = 0.5f;
    float currentParam;
    public float avoidDistance = 5.0f;
    public float lookAhead = 10.0f;

    public override void Awake()
    {
        base.Awake();
        target = new GameObject();
        currentParam = 0f;
        float minPathOffset = 0.5f;
        float maxPathOffset = 3.5f;
        pathOffset = Random.Range(minPathOffset, maxPathOffset);
        transform.parent = null;
    }

    public override Steering GetSteering()
    {
        currentParam = path.GetParam(transform.position, currentParam);
        float targetParam = currentParam + pathOffset;
        target.transform.position = path.GetPosition(targetParam);
        Steering steering = new Steering();
        Vector3 position = transform.position;
        Vector3 rayVector = agent.velocity.normalized * lookAhead;
        Vector3 direction = rayVector;
        RaycastHit hit;
        if (Physics.Raycast(position, direction, out hit, lookAhead))
        {
            position = hit.point + hit.normal * avoidDistance;
            target.transform.position = position;
            steering = base.GetSteering();
        }
        return base.GetSteering();
    }
}