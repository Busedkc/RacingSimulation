using UnityEngine;
using System;

public class CheckpointTrigger : MonoBehaviour
{
    public Action<CarController> CheckpointHit;

    private void OnTriggerEnter(Collider other)
    {
        CarController carController = other.transform.parent.gameObject.GetComponent<CarController>();
        if(carController != null)
        {
            carController.HandleCheckpointWasHit();
            CheckpointHit?.Invoke(carController);
        }
    }
}