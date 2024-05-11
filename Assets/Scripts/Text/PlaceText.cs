using UnityEngine;

public class PlaceText : FormattedText
{
    [SerializeField] string suffix = null;
    private int value;

    public void SetValue(int value)
    {
        this.value = value;
        FormatText();
    }

    protected override void FormatText()
    {
        string placeText = "";
        switch (value)
        {
            case 1:
                placeText += "1<sup>st</sup>";
                break;
            case 2:
                placeText += "2<sup>nd</sup>";
                break;
            case 3:
                placeText += "3<sup>rd</sup>";
                break;
            case 4:
                placeText += "4<sup>th</sup>";
                break;
            case 5:
                placeText += "5<sup>th</sup>";
                break;
            case 6:
                placeText += "6<sup>th</sup>";
                break;
            case 7:
                placeText += "7<sup>th</sup>";
                break;
        }
        placeText += suffix;
        formattedText.SetText(placeText);
    }
}
