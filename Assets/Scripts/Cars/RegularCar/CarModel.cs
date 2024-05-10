using UnityEngine;

public class CarModel : MonoBehaviour
{
    [SerializeField] string carName = null;
    [SerializeField] float carPrice = 0f;
    string label;
    int numberOfCheckpointsHit;
    float distanceToNextCheckpoint;

    public string GetCarName()
    {
        return carName;
    }

    public string GetCarLabel()
    {
        return label;
    }

    public void SetCarLabel(string label)
    {
        this.label = label;
    }

    public int GetNumberOfCheckpointsHit()
    {
        return numberOfCheckpointsHit;
    }

    public float GetCarPrice()
    {
        return carPrice;
    }

    public void CheckpointWasHit()
    {
        numberOfCheckpointsHit++;
    }

    public float GetDistanceToNextCheckpoint()
    {
        return distanceToNextCheckpoint;
    }

    public void SetDistanceToNextCheckpoint(float distance)
    {
        distanceToNextCheckpoint = distance;
    }

}
