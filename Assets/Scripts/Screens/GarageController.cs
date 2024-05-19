using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GarageController : MonoBehaviour
{
    [SerializeField] List<Slider> hsvSliders = null;
    [SerializeField] Button colorPickerButton = null;
    [SerializeField] ColorPickerModal colorPickerModal = null;
    [SerializeField] CarCarousel carCarousel = null;
    [SerializeField] Slider metallicSlider = null;
    [SerializeField] Slider smoothnessSlider = null;
    [SerializeField] List<Button> rimCustomizationButtons = null;
    [SerializeField] List<Material> rimCustomizationMaterials = null;
    [SerializeField] Button saveButton = null;
    [SerializeField] Button resetButton = null;
    Color priorColor;
    public static Color currentColor = Color.white;
    float priorMetallic;
    float currentMetallic;
    float priorSmoothness;
    float currentSmoothness;

    Material[] carBodyMaterials;
    Material priorRimMaterial;

    private void OnEnable()
    {
        carCarousel.ShowCarAtIndex(GameManager.GetCurrentActiveCarIndex(), true);
        for (int i = 0; i < hsvSliders.Count; i++)
        {
            int position = i;
            hsvSliders[position].onValueChanged.AddListener((value) => { HandleHSVSliderValueChanged(position, value); });
            colorPickerButton.onClick.AddListener(colorPickerModal.ShowModal);
            priorColor = GameManager.GetCarColor();
            float h, s, v;
            Color.RGBToHSV(priorColor, out h, out s, out v);
            hsvSliders[0].value = h;
            hsvSliders[1].value = s;
            hsvSliders[2].value = v;
            colorPickerButton.image.color = GameManager.GetCarColor();
            colorPickerModal.ColorPickerModalClosed += HandleColorPickerModalClosed;
        }
        metallicSlider.onValueChanged.AddListener(HandleMetallicSliderValueChanged);
        smoothnessSlider.onValueChanged.AddListener(HandleSmoothnessSliderValueChanged);
        for (int i = 0; i < rimCustomizationButtons.Count; i++)
        {
            int position = i;
            rimCustomizationButtons[position].onClick.AddListener(() =>
            {
                HandleRimCustomizationButtonClicked(rimCustomizationMaterials[position]);
            });
        }
        saveButton.onClick.AddListener(HandleSaveButtonClicked);
        resetButton.onClick.AddListener(HandleResetButtonClicked);
        priorColor = GameManager.GetCarColor();
        carBodyMaterials = carCarousel.GetCurrentCar().GetCarBodyMaterials();
    }

    private void OnDisable()
    {
        for (int i = 0; i < hsvSliders.Count; i++)
        {
            hsvSliders[i].onValueChanged.RemoveAllListeners();
        }
        colorPickerButton.onClick.RemoveListener(colorPickerModal.ShowModal);
        colorPickerModal.ColorPickerModalClosed -= HandleColorPickerModalClosed;
        metallicSlider.onValueChanged.RemoveListener(HandleMetallicSliderValueChanged);
        smoothnessSlider.onValueChanged.RemoveListener(HandleSmoothnessSliderValueChanged);
        for (int i = 0; i < rimCustomizationButtons.Count; i++)
        {
            rimCustomizationButtons[i].onClick.RemoveAllListeners();
        }
        saveButton.onClick.RemoveListener(HandleSaveButtonClicked);
        resetButton.onClick.RemoveListener(HandleResetButtonClicked);

    }

    private void Update()
    {
        colorPickerButton.image.color = currentColor;
        if (carBodyMaterials.Length >= 1)
        {
            foreach (Material material in carBodyMaterials)
            {
                material.color = currentColor;
            }
        }
    }

    void HandleHSVSliderValueChanged(int indexOfSlider, float value)
    {
        carBodyMaterials = carCarousel.GetCurrentCar().GetCarBodyMaterials();
        foreach (Material material in carBodyMaterials)
        {
            float h, s, v;
            Color.RGBToHSV(material.color, out h, out s, out v);
            switch (indexOfSlider)
            {
                case 0:
                    material.color = Color.HSVToRGB(value, s, v);
                    break;
                case 1:
                    material.color = Color.HSVToRGB(h, value, v);
                    break;
                case 2:
                    material.color = Color.HSVToRGB(h, s, value);
                    break;
            }
            currentColor = material.color;
        }
    }

    void SaveCarColor(Color color, float metallic, float smoothness)
    {
        foreach (Material material in carBodyMaterials)
        {
            material.color = color;
            Events.CarColorChosen?.Invoke(color, metallic, smoothness);
            colorPickerButton.image.color = color;
        }
    }

    private void HandleColorPickerModalClosed()
    {
        float h, s, v;
        Color.RGBToHSV(currentColor, out h, out s, out v);
        hsvSliders[0].value = h;
        hsvSliders[1].value = s;
        hsvSliders[2].value = v;
    }

    void HandleMetallicSliderValueChanged(float value)
    {
        carBodyMaterials = carCarousel.GetCurrentCar().GetCarBodyMaterials();
        foreach (Material material in carBodyMaterials)
        {
            priorMetallic = material.GetFloat("_Metallic");
            material.SetFloat("_Metallic", value);
            currentMetallic = value;
        }
    }

    void HandleSmoothnessSliderValueChanged(float value)
    {
        carBodyMaterials = carCarousel.GetCurrentCar().GetCarBodyMaterials();
        foreach (Material material in carBodyMaterials)
        {
            priorSmoothness = material.GetFloat("_Glossiness");
            material.SetFloat("_Glossiness", value);
            currentSmoothness = value;
        }
    }

    void HandleRimCustomizationButtonClicked(Material material)
    {
        CarController car = carCarousel.GetCurrentCar();
        priorRimMaterial = car.GetRimMaterial();
        car.SetRimMaterial(material);
        Events.RimColorChoosen?.Invoke(material);
    }

    void HandleSaveButtonClicked()
    {
        priorColor = GameManager.GetCarColor();
        SaveCarColor(currentColor, currentMetallic, currentSmoothness);
    }

    void HandleResetButtonClicked()
    {
        HandleRimCustomizationButtonClicked(priorRimMaterial);
        HandleMetallicSliderValueChanged(priorMetallic);
        HandleSmoothnessSliderValueChanged(priorSmoothness);
        SaveCarColor(priorColor, priorMetallic, priorSmoothness);
    }
}