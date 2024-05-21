using UnityEngine;

public class AIBehavior : MonoBehaviour
{
    public GameObject target;
    protected AI agent;

    public virtual void Awake()
    {
        agent = gameObject.GetComponent<AI>();
    }

    public virtual void Update()
    {
        agent.SetSteering(GetSteering());
    }

    public virtual Steering GetSteering()
    {
        return new Steering();
    }
}