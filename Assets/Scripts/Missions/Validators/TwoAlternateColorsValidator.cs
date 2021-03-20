using System.Linq;

namespace MissionAcomplished.Missions.Validators
{
    public class TwoAlternateColorsValidator : BaseValidator
    {
        protected Colors color;

        public TwoAlternateColorsValidator(string type, params string[] arguments) :
            base(type)
        {
            this.ValidateArgumentCount(arguments, 1);
            this.color = this.ArgumentToColor(arguments[0], 1);
        }

        public override string GetCardText()
        {
            return string
                .Format("Hay dos cartas {0} alternas",
                this.GetColorTranslated(this.color));
        }

        protected override bool ValidatePiles()
        {
            int[] indexes = this.piles.cards
                .Select((card, index) => new { card, index })
                .Where(obj => obj.card.color == this.color)
                .Select(obj => obj.index)
                .ToArray();

            if (indexes.Length < 2) return false;

            for (int i = 0; i < indexes.Length - 1; i++)
            {
                if (indexes[i + 1] - indexes[i] == 2)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
