using System.Linq;

public class NoRepetitionValidator : BaseValidator
{
    protected const string NO_REPEAT_COLORS = "colors";
    protected const string NO_REPEAT_RANKS = "ranks";
    protected const string NO_REPEAT_COLORS_NOR_RANKS = "colors-ranks";
    protected string noRepeat;
    
    public NoRepetitionValidator(string type, params string[] arguments) : base(type) {
        this.ValidateArgumentCount(arguments, 1);
        this.noRepeat = arguments[0];

        if (this.noRepeat != NoRepetitionValidator.NO_REPEAT_COLORS
            && this.noRepeat != NoRepetitionValidator.NO_REPEAT_RANKS
            && this.noRepeat != NoRepetitionValidator.NO_REPEAT_COLORS_NOR_RANKS
        ) {
            throw new System.Exception(string.Format(
                "{0} expects argument #0 to be one of ({1}, {2}, {3}), but \"{4}\" given.",
                this.GetType().Name,
                NoRepetitionValidator.NO_REPEAT_COLORS,
                NoRepetitionValidator.NO_REPEAT_RANKS,
                NoRepetitionValidator.NO_REPEAT_COLORS_NOR_RANKS,
                this.noRepeat
            ));
        }
    }

    public override string GetCardText()
    {
        switch (this.noRepeat)
        {
            case NoRepetitionValidator.NO_REPEAT_COLORS:
                return "No hay ningún color repetido";

            case NoRepetitionValidator.NO_REPEAT_RANKS:
                return "No hay ningún número repetido";

            case NoRepetitionValidator.NO_REPEAT_COLORS_NOR_RANKS:
                return "No hay ningún color ni ningún número repetido";
        }
        return "";
    }

    protected override bool ValidatePiles()
    {
       switch (this.noRepeat)
        {
            case NoRepetitionValidator.NO_REPEAT_COLORS:
                return this.ValidateNoRepeatColors();

            case NoRepetitionValidator.NO_REPEAT_RANKS:
                return this.ValidateNoRepeatRanks();

            case NoRepetitionValidator.NO_REPEAT_COLORS_NOR_RANKS:
                return this.ValidateNoRepeatColorsNorRanks();
        }
        return false;
    }

    protected bool ValidateNoRepeatColors() {
        return this.piles.GroupByColor().Count() == this.piles.cards.Length;
    }
    
    protected bool ValidateNoRepeatRanks() {
        return this.piles.GroupByRank().Count() == this.piles.cards.Length;
    }

    protected bool ValidateNoRepeatColorsNorRanks() {
        return this.ValidateNoRepeatColors() && this.ValidateNoRepeatRanks();
    }
}
