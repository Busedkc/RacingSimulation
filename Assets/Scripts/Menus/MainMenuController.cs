using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button raceButton = null;
    [SerializeField] Button hotPursuitButton = null;
    [SerializeField] Button garageButton = null;
    [SerializeField] Button shopButton = null;
    [SerializeField] List<CarController> carList = null;

    private void Awake()
    {
        foreach(CarController car in carList)
        {
            car.DisplayCar(false);
        }
        CarController activeCar = carList[GameManager.GetCurrentActiveCarIndex()];
        activeCar.SetColorAndRims();
        activeCar.DisplayCar(true,true);
    }

    private void OnEnable()
    {
        raceButton.onClick.AddListener(HandleRaceButtonClicked);
        hotPursuitButton.onClick.AddListener(HandleHotPursuitButtonClicked);
        garageButton.onClick.AddListener(HandleGarageButtonClicked);
        shopButton.onClick.AddListener(HandleShopButtonClicked);
    }

    private void OnDisable()
    {
        raceButton.onClick.RemoveListener(HandleRaceButtonClicked);
        hotPursuitButton.onClick.RemoveListener(HandleHotPursuitButtonClicked);
        garageButton.onClick.RemoveListener(HandleGarageButtonClicked);
        shopButton.onClick.RemoveListener(HandleShopButtonClicked);
    }
    
    void HandleRaceButtonClicked()
    {
        Events.OkButtonClicked?.Invoke();
        NavigationManager.LoadScene(Scenes.SELECT_A_TRACK);
    }

    void HandleHotPursuitButtonClicked()
    {
        Events.OkButtonClicked?.Invoke();
        NavigationManager.LoadScene(Scenes.CITY);
    }

    void HandleGarageButtonClicked()
    {
        Events.OkButtonClicked?.Invoke();
        NavigationManager.LoadScene(Scenes.GARAGE);
    }

    void HandleShopButtonClicked()
    {
        Events.OkButtonClicked?.Invoke();
        NavigationManager.LoadScene(Scenes.SHOP);
    }
}