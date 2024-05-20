using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] List<CarController> cars = null;
    [SerializeField] GameObject speedometerNeedle = null;

    const float minRotation = 135.0f;
    const float maxRotation = -135.0f;
    const float maxSpeed = 50.0f;
    float rotation;

    CarController car;

    private void Awake()
    {
        car = cars[GameManager.GetCurrentActiveCarIndex()];
    }

    private void FixedUpdate()
    {
        UpdateSpeedometer();
    }

    void UpdateSpeedometer()
    {
        float currentSpeedPercentage = car.GetSpeed() / maxSpeed;
        rotation = currentSpeedPercentage >= 0 ? Mathf.Lerp(minRotation, maxRotation, currentSpeedPercentage) :
                                                 Mathf.Lerp(minRotation,maxRotation, -currentSpeedPercentage) ;
        speedometerNeedle.transform.eulerAngles = new Vector3(speedometerNeedle.transform.eulerAngles.x,
                                                              speedometerNeedle.transform.eulerAngles.y,
                                                              rotation);
    }
}
