public abstract class BaseValidator
{
    protected ValidatorPiles piles;

    protected string type;

    public BaseValidator(string type)
    {
        this.type = type;
    }

    public bool IsAcomplished(CardBase[] piles)
    {
        this.piles = new ValidatorPiles(piles);
        return this.ValidatePiles();
    }

    protected string GetColorHex(string color) {
     switch (color)
        {
            case "red": return "#ff595e";
            case "green": return "#8ac926";
            case "blue": return "#1982c4";
            case "yellow": return "#ffca3a";
        }
        return "";
    }

    protected string GetColorTranslated(string color, bool wrapInColor = true) {
        string translated = "";
        switch (color)
        {
            case "red": translated = "rojas"; break;
            case "green": translated = "verdes"; break;
            case "blue": translated = "azules"; break;
            case "yellow": translated = "amarillas"; break;
        }

        if (wrapInColor)
        {
            string colorHex = GetColorHex(color);
            return $"<color={color}>{translated}</color>";
        }
        
        return translated;
    }

    protected abstract bool ValidatePiles();

    public abstract string GetCardText();
}
