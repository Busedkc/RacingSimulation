public class MoneyText : FormattedText
{
    public float value;

    public void SetValue(float value)
    {
        this.value = value;
        FormatText();
    }

    protected override void FormatText()
    {
        string valueAsCurrency = value.ToString("C");
        formattedText.SetText(valueAsCurrency);
    }
}
