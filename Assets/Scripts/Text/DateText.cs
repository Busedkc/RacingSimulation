using System;

public class DateText : FormattedText
{
    private long value;

    public void SetValue(long value)
    {
        this.value = value;
        FormatText();
    }

    protected override void FormatText()
    {
        DateTime date = DateTime.FromBinary(value);
        string valueAsDate = date.ToString("MM/dd/yyyy");
        formattedText.SetText(valueAsDate);
    }
}
