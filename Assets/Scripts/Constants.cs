using System.Collections.Generic;

public enum Ranks
{
    NoRank = -1,
    One = 0,
    Two = 1,
    Three = 2,
    Four = 3,
    Five = 4,
    Six = 5,
    Seven = 7
}

public enum Colors
{
    NoColor = -1,
    Red = 0,
    Green = 1,
    Blue = 2,
    Yellow = 3
}


public class Constants {
    public const int RANK_COUNT = 7;
    public const int COLOR_COUNT = 4;
    public const int CARD_OF_EACH_COLOR_AND_RANK_COUNT = 2;
    public static Dictionary<Colors, string> ColorToSpriteName = new Dictionary<Colors, string>() {
        { Colors.Red, "r" },
        { Colors.Green, "g" },
        { Colors.Blue, "b" },
        { Colors.Yellow, "y" },
    };

    public const string HEX_COLOR_RED = "#ff595e";
    public const string HEX_COLOR_GREEN = "#8ac926";
    public const string HEX_COLOR_BLUE = "#1982c4";
    public const string HEX_COLOR_YELLOW = "#ffca3a";

}