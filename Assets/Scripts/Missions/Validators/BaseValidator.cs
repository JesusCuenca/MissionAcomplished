using System;
using MissionAcomplished.Cards;

namespace MissionAcomplished.Missions.Validators
{
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

        protected string GetColorHex(Colors color)
        {
            switch (color)
            {
                case Colors.Red: return "#ff595e";
                case Colors.Green: return "#8ac926";
                case Colors.Blue: return "#1982c4";
                case Colors.Yellow: return "#ffca3a";
            }
            return "";
        }

        protected string GetColorTranslated(Colors color, bool wrapInColor = true)
        {
            string translated = "";
            switch (color)
            {
                case Colors.Red: translated = "rojas"; break;
                case Colors.Green: translated = "verdes"; break;
                case Colors.Blue: translated = "azules"; break;
                case Colors.Yellow: translated = "amarillas"; break;
            }

            if (wrapInColor)
            {
                string colorHex = GetColorHex(color);
                return $"<color={colorHex}>{translated}</color>";
            }

            return translated;
        }

        protected void ValidateArgumentCount(string[] arguments, int count)
        {
            if (arguments.Length != count)
            {
                throw new System.Exception(string.Format(
                    "{0} expects exactly {1} parameter{2}, {3} given.",
                    this.GetType().Name,
                    count,
                    count != 1 ? "s" : "",
                    arguments.Length
                ));
            }
        }
        protected Colors ArgumentToColor(string color, int index)
        {
            switch (color)
            {
                case "red": return Colors.Red;
                case "green": return Colors.Green;
                case "blue": return Colors.Blue;
                case "yellow": return Colors.Yellow;
            }
            throw new System.Exception(string.Format("{0} expects parameter #{1} to be a valid Colors.", this.GetType().Name, index));
        }

        protected int ArgumentToInt(string argument, int index)
        {
            try
            {
                return int.Parse(argument);
            }
            catch (FormatException)
            {
                throw new System.Exception(string.Format("{0} expects parameter #{1} to be an integer.", this.GetType().Name, index));
            }
        }


        protected abstract bool ValidatePiles();

        public abstract string GetCardText();
    }
}
