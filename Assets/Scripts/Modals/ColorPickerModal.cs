using UnityEngine;
using UnityEngine.UI;
using System;

public class ColorPickerModal : OneButtonModal
{
    [SerializeField] Slider redSlider = null;
    [SerializeField] Slider greenSlider = null;
    [SerializeField] Slider blueSlider = null;
    [SerializeField] Image colorImage = null;
    [SerializeField] PercentText redText = null;
    [SerializeField] PercentText greenText = null;
    [SerializeField] PercentText blueText = null;
    [SerializeField] Image handle = null;
    [SerializeField] Canvas canvas = null;

    public Action ColorPickerModalClosed;

    bool isGrabbed;
    Color colorImageColor;
    Color carColor;

    protected override void OnEnable()
    {
        base.OnEnable();
        colorImageColor = GarageController.currentColor;
        float red = colorImageColor.r;
        float green = colorImageColor.g;
        float blue = colorImageColor.b;
        colorImage.color = colorImageColor;
        redSlider.value = red;
        greenSlider.value = green;
        blueSlider.value = blue;
        redText.SetValue(red);
        greenText.SetValue(green);
        blueText.SetValue(blue);
        redSlider.onValueChanged.AddListener(HandleRedSliderChange);
        greenSlider.onValueChanged.AddListener(HandleGreenSliderChange);
        blueSlider.onValueChanged.AddListener(HandleBlueSliderChange);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        redSlider.onValueChanged.RemoveListener(HandleRedSliderChange);
        greenSlider.onValueChanged.RemoveListener(HandleGreenSliderChange);
        blueSlider.onValueChanged.RemoveListener(HandleBlueSliderChange);
    }

    protected override void HandleButtonClicked()
    {
        base.HandleButtonClicked();
        ColorPickerModalClosed?.Invoke();
        HideModal();
    }

    private void Update()
    {
        isGrabbed = Input.GetKey(KeyCode.Mouse0);
        if (isGrabbed)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);

            Vector3[] corners = new Vector3[4];
            colorImage.rectTransform.GetWorldCorners(corners);
            Rect newRect = new Rect(corners[0], corners[2] - corners[0]);
            if (newRect.Contains(Input.mousePosition))
            {
                handle.transform.position = canvas.transform.TransformPoint(pos);
                Vector2 size = new Vector2(512, 512);
                Vector2 pixelCoord = Input.mousePosition - corners[0];
                pixelCoord /= colorImage.rectTransform.rect.size;
                pixelCoord *= size;
                carColor = colorImage.sprite.texture.GetPixel((int)pixelCoord.x, (int)pixelCoord.y);
                HandleChangeColorImage(carColor.r, carColor.g, carColor.b);
            }
        }
    }

    void HandleRedSliderChange(float value)
    {
        Color temp = GarageController.currentColor;
        temp.r = value;
        GarageController.currentColor = temp;
        redText.SetValue(temp.r);
        colorImage.color = temp;
    }

    void HandleGreenSliderChange(float value)
    {
        Color temp = GarageController.currentColor;
        temp.g = value;
        GarageController.currentColor = temp;
        greenText.SetValue(temp.g);
        colorImage.color = temp;
    }

    void HandleBlueSliderChange(float value)
    {
        Color temp = GarageController.currentColor;
        temp.b = value;
        GarageController.currentColor = temp;
        blueText.SetValue(temp.b);
        colorImage.color = temp;
    }

    void HandleChangeColorImage(float red, float green, float blue)
    {
        Color temp = GarageController.currentColor;
        temp.r = red;
        temp.g = green;
        temp.b = blue;
        float h;
        float s;
        float v;
        Color.RGBToHSV(colorImage.color, out h, out s, out v);
        float h2;
        float s2;
        float v2;
        Color.RGBToHSV(temp, out h2, out s2, out v2);
        GarageController.currentColor = Color.HSVToRGB(h, s, v2);
    }


}


