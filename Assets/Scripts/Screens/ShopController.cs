using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI carNameText = null;
    [SerializeField] TextMeshProUGUI priceText = null;
    [SerializeField] Button buyButton = null;
    [SerializeField] CarCarousel carCarousel = null;
    [SerializeField] GameObject noCarsAvailableMessage = null;
    [SerializeField] TwoButtonModal confirmPurchaseModal = null;
    [SerializeField] TwoButtonModal congratulationsModal = null;
    [SerializeField] OneButtonModal notEnoughMoneyModal = null;

    private void OnEnable()
    {
        SetupCarsForSaleList();
        DisplayShopContent();
        buyButton.onClick.AddListener(HandleBuyButtonPressed);
        confirmPurchaseModal.Init(HandleConfirmPurchaseModalConfirm, HideConfirmPurchaseModal);
        congratulationsModal.Init(HandleMainMenuButtonClicked, HandleGarageButtonClicked);
        notEnoughMoneyModal.Init(HandleNotEnoughMoneyModalCloseButtonClicked);
        carCarousel.CarChanged += UpdateScreen;
    }

    private void OnDisable()
    {
        buyButton.onClick.RemoveListener(HandleBuyButtonPressed);
        carCarousel.CarChanged -= UpdateScreen;

    }

    void SetupCarsForSaleList()
    {
        List<bool> carStates = GameManager.GetCarStates();
        for(int i = 0; i < carStates.Count; i++)
        {
            bool doesPlayerHaveCar = carStates[i];
            if (doesPlayerHaveCar)
            {
                carCarousel.RemoveCarAtIndex(i);
            }

        }
    }
    void DisplayShopContent()
    {
        if(carCarousel.AreAllCarsNull())
        {
            noCarsAvailableMessage.SetActive(true);

        }else
        {
            carCarousel.ShowCarAtIndex(0);
            UpdateScreen(carCarousel.GetCurrentItemIndex());

        }
    }

    void HandleBuyButtonPressed()
    {
        confirmPurchaseModal.ShowModal();
    }

    void UpdateScreen(int index)
    {
        carNameText.SetText(carCarousel.GetCurrentCar().GetCarName());
        priceText.SetText(carCarousel.GetCurrentCar().GetCarPrice().ToString("C"));

    }

    void HandleConfirmPurchaseModalConfirm()
    {
        float playersMoney = GameManager.GetMoneyAsFloat();
        float carPrice = carCarousel.GetCurrentCar().GetCarPrice();
        if(playersMoney >= carPrice)
        {
            Events.PurchaseButtonClicked?.Invoke();
            GameManager.PurchaseCar(carPrice, carCarousel.GetCurrentItemIndex());
            congratulationsModal.ShowModal();
            confirmPurchaseModal.HideModal();
        }
        else
        {
            notEnoughMoneyModal.ShowModal();
        }
    }
    void hideConfirmPurchaseModal()
    {
        confirmPurchaseModal.HideModal();
    }

    void HandleMainMenuButtonClicked()
    {
        NavigationManager.LoadScene(Scenes.MAIN_MENU);
    }

    void HandleGarageButtonClicked()
    {
        NavigationManager.LoadScene(Scenes.GARAGE);
    }

    void HandleNotEnoughMoneyModalCloseButtonClicked()
    {
        confirmPurchaseModal.HideModal();
        notEnoughMoneyModal.HideModal();
    }
}
