using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentText : FormattedText
{
    private float value;

    public void SetValue(float value)
    {
        this.value = value;
        FormatText();
    }

    protected override void FormatText()
    {
        string valueAsPercent = (value * 100).ToString("0.00") + "%";
        formattedText.SetText(valueAsPercent);
    }
}
 

