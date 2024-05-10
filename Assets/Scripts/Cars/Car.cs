using UnityEngine;

public class Car : MonoBehaviour
{

    float originalYPos;
    bool hasOriginalYPosBeenSet;

    public virtual void Start()
    {
        Invoke("SetoriginalYPos", 1.5f);
    }

    public virtual void Update()
    {
        if (hasOriginalYPosBeenSet)
        {
            Vector3 positionWithConstantYPos = new Vector3(transform.position.x,
                                                           originalYPos, transform.position.z);
            transform.position = positionWithConstantYPos;
        }
    }
    private void SetOriginalYPos()
    {
        originalYPos = transform.position.y;
        hasOriginalYPosBeenSet = true;
    }
}

