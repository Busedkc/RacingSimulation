using System.Collections.Generic;
using UnityEngine;

public class CarFactory : MonoBehaviour
{
    [SerializeField] List<GameObject> carMeshes = null;
    [SerializeField] List<Material> rimMaterials = null;
    [SerializeField] List<Transform> spawnPoints = null;
    [SerializeField] List<Path> paths = null;
    [SerializeField] GameObject carStartingDirectionObject = null;
    static List<CarController> cars = null;
    int counter;
    int currentTrack;
    bool showHeadlights;

    private void Start()
    {
        cars = new List<CarController>();
        currentTrack = int.Parse(NavigationManager.SceneData["track"]);
        showHeadlights = currentTrack % 2 == 0;
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            SpawnRandomCar(spawnPoints[i], i);
        }
    }

    public GameObject SpawnRandomCar(Transform spawnPoint, int index)
    {
        int randomBodyIndex = Random.Range(0, carMeshes.Count - 1);
        GameObject car = Instantiate(carMeshes[randomBodyIndex], spawnPoint);
        car.transform.parent = null;
        CarController carController = car.GetComponent<CarController>();
        carController.SetCarLabel("CPU" + counter);
        counter++;
        Color randomBodyColor = Random.ColorHSV();
        carController.SetCarColor(randomBodyColor);
        int randomRimIndex = Random.Range(0, rimMaterials.Count - 1);
        Material rimMaterial = rimMaterials[randomRimIndex];
        carController.SetRimMaterial(rimMaterial);
        SelfDrivingCar pf = car.GetComponent<SelfDrivingCar>();
        pf.path = index <= 2 ? paths[0] : paths[1];
        carController.gameObject.transform.rotation = carStartingDirectionObject.transform.rotation;
        carController.ToogleHeadLightFlares(showHeadlights);
        cars.Add(carController);
        return car;
    }

    public static void EnableAICars()
    {
        foreach (CarController car in cars)
        {
            car.EnableAICar();
        }
    }
}