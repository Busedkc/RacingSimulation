using System;

public class TimeText : FormattedText
{
    private float value;

    public void SetValue(float value)
    {
        this.value = value;
        FormatText();
    }

    protected override void FormatText()
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        string valueAsTime = time.ToString(@"hh\:mm\:ss");
        formattedText.SetText(prefix + " " + valueAsTime);
    }
}
