using System.Linq;

namespace MissionAcomplished.Missions.Validators
{
    public class ThreeColorsValidator : BaseValidator
    {
        protected Colors color;

        public ThreeColorsValidator(string type, params string[] arguments) : base(type)
        {
            this.ValidateArgumentCount(arguments, 1);
            this.color = this.ArgumentToColor(arguments[0], 1);
        }

        public override string GetCardText()
        {
            return string.Format("Hay tres cartas {0}", this.GetColorTranslated(this.color));
        }

        protected override bool ValidatePiles()
        {
            var results = this.piles.GroupByColor();
            foreach (var result in results)
            {
                if (result.Key == this.color && result.Count() == 3)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

