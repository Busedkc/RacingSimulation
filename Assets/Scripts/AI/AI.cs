using UnityEngine;

public class AI : MonoBehaviour
{
    public float maxSpeed;
    public float maxAccel;
    public float orientation;
    public float rotation;
    public Vector3 velocity;
    protected Steering steering;
    private Vector3 oldPos;

    private void Start()
    {
        velocity = Vector3.zero;
        steering = new Steering();
    }

    public void SetSteering(Steering steering)
    {
        this.steering = steering;
    }

    public virtual void FixedUpdate()
    {
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        transform.Translate(displacement, Space.World);
        RotateCarToFaceForward();
    }

    public virtual void LateUpdate()
    {
        velocity += steering.linear * Time.deltaTime;
        rotation += steering.angular * Time.deltaTime;
        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity = velocity * maxSpeed;
        }
        if (steering.angular == 0.0f)
        {
            rotation = 0.0f;
        }
        if (steering.linear.sqrMagnitude == 0.0f)
        {
            velocity = Vector3.zero;
        }
        steering = new Steering();
        oldPos = transform.position;
    }

    void RotateCarToFaceForward()
    {
        Vector3 direction = (transform.position - oldPos) * Time.fixedDeltaTime;
        Quaternion lookAtRotation = Quaternion.LookRotation(direction);
        Quaternion lookAtRotationOnlyY = Quaternion.Euler(transform.rotation.eulerAngles.x, lookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = lookAtRotationOnlyY;
    }
}