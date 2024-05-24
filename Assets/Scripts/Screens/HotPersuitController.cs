using System.Collections.Generic;
using UnityEngine;

public class HotPersuitController : MonoBehaviour
{
    [SerializeField] List<CarController> playerCarList = null;
    CarController playerCar;

    private void Start()
    {
        playerCar = playerCarList[GameManager.GetCurrentActiveCarIndex()];
        playerCar.DisplayCar(true, true, true);
    }
}
