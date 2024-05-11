using UnityEngine;

public class Rotate360 : MonoBehaviour
{
    [SerializeField] float speed;
    private void Update()
    {
        transform.rotation = transform.rotation * Quaternion.AngleAxis(speed, Vector3.up);
    }
}
