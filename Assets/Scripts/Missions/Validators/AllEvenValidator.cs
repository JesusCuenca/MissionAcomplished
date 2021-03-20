namespace MissionAcomplished.Missions.Validators
{
    public class AllEvenValidator : BaseValidator
    {
        public AllEvenValidator(string type) : base(type) { }

        public override string GetCardText()
        {
            return "Todas las cartas son <b>pares</b>";
        }

        protected override bool ValidatePiles()
        {
            return this.piles.Filter(card => card.rank % 2 == 0).Length == this.piles.Length;
        }
    }
}
