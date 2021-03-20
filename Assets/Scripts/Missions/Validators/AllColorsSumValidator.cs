namespace MissionAcomplished.Missions.Validators
{
    public class AllColorsSumValidator : BaseValidator
    {
        private Colors color;

        private int sum;

        public AllColorsSumValidator(string type, params string[] arguments) :
            base(type)
        {
            this.ValidateArgumentCount(arguments, 2);
            this.color = this.ArgumentToColor(arguments[0], 0);
            this.sum = this.ArgumentToInt(arguments[1], 1);
        }

        public override string GetCardText()
        {
            return string.Format("Las cartas {0} suman {1}", this.GetColorTranslated(this.color), this.sum);
        }

        protected override bool ValidatePiles()
        {
            return this.piles.SumColorRanks(this.color) == this.sum;
        }
    }
}
