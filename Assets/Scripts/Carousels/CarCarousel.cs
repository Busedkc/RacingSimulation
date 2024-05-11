using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarCarousel : MonoBehaviour, ICarousel
{
    [SerializeField] List<CarController> cars = null;
    [SerializeField] Button leftButton = null;
    [SerializeField] Button rightButton = null;
    [SerializeField] bool saveActiveCar;

    public Action<int> CarChanged;
    int index;
    bool goingRight;

    void Start()
    {
        leftButton.onClick.AddListener(ShowPreviousItem);
        rightButton.onClick.AddListener(ShowNextItem);
    }

    void OnDestroy()
    {
        leftButton.onClick.RemoveListener(ShowPreviousItem);
        rightButton.onClick.RemoveListener(ShowNextItem);
    }

    public void ShowNextItem()
    {
        if (AreAllCarsNull()) return;
        Events.LeftOrRightButtonClicked?.Invoke();
        goingRight = true;
        index = index < cars.Count - 1 ? index + 1 : 0;
        ShowCarAtIndex(index);
        if (saveActiveCar)
        {
            GameManager.SetActiveCar(index);
        }
    }

    public void ShowPreviousItem()
    {
        if (AreAllCarsNull()) return;
        Events.LeftOrRightButtonClicked?.Invoke();
        goingRight = false;
        index = index > 0 ? index - 1 : cars.Count - 1;
        ShowCarAtIndex(index);
        if (saveActiveCar)
        {
            GameManager.SetActiveCar(index);
        }
    }

    public void ShowCarAtIndex(int index, bool applyCustomizations = false)
    {
        HideCars();

        if (cars[index] != null)
        {
            cars[index].DisplayCar(true, applyCustomizations);
        }
        else
        {
            if (goingRight)
            {
                ShowNextItem();
            }
            else
            {
                ShowPreviousItem();
            }
        }
        CarChanged?.Invoke(index);
    }

    void HideCars()
    {
        foreach(CarController car in cars)
        {
            if(car != null)
            {
                car.DisplayCar(false);
            }
        }
    }

    public int GetCurrentItemIndex()
    {
        return index;
    }

    public CarController GetCurrentCar()
    {
        if (AreAllCarsNull())
        {
            return null;
        }
        else if (cars[index] != null)
        {
            return cars[index];
        }
        else
        {
            index = index < cars.Count - 1 ? index + 1 : 0;
            return GetCurrentCar();
        }
    }

    public void RemoveCarAtIndex(int index)
    {
        cars[index] = null;
    }

    public bool AreAllCarsNull()
    {
        foreach(CarController car in cars)
        {
            if(car != null)
            {
                return false;
            }
        }
        return true;
    }



}
