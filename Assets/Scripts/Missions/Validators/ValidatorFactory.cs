namespace MissionAcomplished.Missions.Validators
{
    public class ValidatorFactory
    {
        public static BaseValidator make(MissionDefinition definition)
        {
            string type = definition.type;
            string[] arguments = definition.arguments;

            switch (type)
            {
                case "all.even-odd-two-by-two":
                    return new EvenOddAlternateValidator(type);

                case "all.even":
                    return new AllEvenValidator(type);

                case "all.greater-than-4":
                    return new AllGreaterThan4Validator(type);

                case "all.lower-than-4":
                    return new AllLowerThan4Validator(type);

                case "all.odd":
                    return new AllOddValidator(type);

                case "colors.2-adjacent-blue":
                case "colors.2-adjacent-green":
                case "colors.2-adjacent-red":
                case "colors.2-adjacent-yellow":
                    return new TwoAdjacentColorsValidator(type, arguments);

                case "colors.2-alternate-blue":
                case "colors.2-alternate-green":
                case "colors.2-alternate-red":
                case "colors.2-alternate-yellow":
                    return new TwoAlternateColorsValidator(type, arguments);

                case "colors.2-not-adjacent-blue":
                case "colors.2-not-adjacent-green":
                case "colors.2-not-adjacent-red":
                case "colors.2-not-adjacent-yellow":
                    return new TwoNotAdjacentColorsValidator(type, arguments);

                case "colors.3-blue":
                case "colors.3-green":
                case "colors.3-red":
                case "colors.3-yellow":
                    return new ThreeColorsValidator(type, arguments);

                case "flush.3-straight":
                    return new Flush3StraightValidator(type);

                case "flush.4-random":
                    return new Flush4RandomValidator(type);

                case "no-repetition.colors":
                case "no-repetition.colors-ranks":
                case "no-repetition.ranks":
                    return new NoRepetitionValidator(type, arguments);

                case "sum.colors-double.blue-red":
                case "sum.colors-double.green-blue":
                case "sum.colors-double.red-green":
                case "sum.colors-double.red-yellow":
                case "sum.colors-double.yellow-blue":
                case "sum.colors-double.yellow-green":
                    return new ColorsSumDoubleValidator(type, arguments);

                case "sum.colors.blue-yellow":
                case "sum.colors.green-blue":
                case "sum.colors.green-yellow":
                case "sum.colors.red-blue":
                case "sum.colors.red-green":
                case "sum.colors.red-yellow":
                    return new ColorsSumEqualValidator(type, arguments);

                case "sum.total.10":
                case "sum.total.15":
                case "sum.total.18":
                case "sum.total.20":
                    return new AllSumValidator(type, arguments);

                case "sum.total.blue.3":
                case "sum.total.blue.9":
                case "sum.total.green.6":
                case "sum.total.green.7":
                case "sum.total.red.10":
                case "sum.total.red.4":
                case "sum.total.yellow.11":
                case "sum.total.yellow.2":
                    return new AllColorsSumValidator(type, arguments);

                default:
                    throw new System.Exception($"No validator defined for mission type {type}");
            }
        }
    }
}
