using System.Collections.Generic;
using UnityEngine;

public class PoliceCarFactory : MonoBehaviour
{
    [SerializeField] List<GameObject> policeCarTypes = null;
    [SerializeField] List<Transform> spawnPoints = null;
    List<GameObject> policeCars;

    private void Awake()
    {
        policeCars = new List<GameObject>();
        foreach (Transform spawnPoint in spawnPoints)
        {
            int randomIndex = Random.Range(0, policeCarTypes.Count);
            GameObject policeCar = Instantiate(policeCarTypes[randomIndex], spawnPoint);
            policeCars.Add(policeCar);
        }
    }
}