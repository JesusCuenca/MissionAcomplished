using System.Linq;

namespace MissionAcomplished.Missions.Validators
{
    public class Flush4RandomValidator : BaseValidator
    {
        public Flush4RandomValidator(string type) : base(type) { }

        public override string GetCardText()
        {
            return "Hay escalera de tres cartas en orden";
        }

        protected override bool ValidatePiles()
        {
            int[] ranks = this.piles.GetRanks().OrderBy(rank => rank).ToArray();
            for (int i = 0; i < ranks.Length - 1; i++)
            {
                if ((ranks[i] + 1) != ranks[i + 1])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
