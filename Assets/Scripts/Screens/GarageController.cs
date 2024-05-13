using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GarageController : MonoBehaviour
{
    [SerializeField] List<Slider> hsvSliders = null;
    [SerializeField] Button colorPickerButton = null;
    [SerializeField] ColorPickerModal colorPickerModal = null;
    [SerializeField] CarCarousel carCarousel = null;
    Material[] carBodyMaterials;

    private void OnEnable()
    {
        carCarousel.ShowCarAtIndex(GameManager.GetCurrentActiveCarIndex(), true);
        for (int i = 0; i < hsvSliders.Count; i++)
        {
            //int position = i;
            //hsvSliders[position].onValueChanged.AddListener((value) => { HandleHSVSliderChanged(position, value); });
            //colorPickerButton.onClick.AddListener(colorPickerModal.ShowModal);
            //priorColor = GameManager.GetCarColor();
            //float h, s, v;
            //Color.RGBToHSV(priorColor, out h, out s, out v);
            //hsvSliders[0].value = h;
            //hsvSliders[1].value = s;
            //hsvSliders[2].value = v;
            //colorPickerButton.image.color = GameManager.GetCarColor();
            //colorPickerModal.ColorPickerModalClosed += HandleColorPickerModalClosed;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < hsvSliders.Count; i++)
        {
            hsvSliders[i].onValueChanged.RemoveAllListeners();
        }
        colorPickerButton.onClick.RemoveListener(colorPickerModal.ShowModal);
        colorPickerModal.ColorPickerModalClosed -= HandleColorPickerModalClosed;
    }

    private void Update()
    {
        //colorPickerButton.image.color = currentColor;
        //if (carBodyMaterials.Length >= 1)
        //{
          //  foreach (Material material in carBodyMaterials)
            //{
              //  material.color = currentColor;
            //}
        //}
    }

    void HandleHSVSliderChanged(int indexOfSlider, float value)
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
            //currentColor = material.color;
        }
    }

    void SaveCarColor(Color color)
    {
        foreach (Material material in carBodyMaterials)
        {
            material.color = color;
            //Events.CarColorChosen?.Invoke(color);
            colorPickerButton.image.color = color;
        }
    }

    private void HandleColorPickerModalClosed()
    {
        float h, s, v;
        //Color.RGBToHSV(currentColor, out h, out s, out v);
        //hsvSliders[0].value = h;
        //hsvSliders[1].value = s;
        //hsvSliders[2].value = v;
    }
}

