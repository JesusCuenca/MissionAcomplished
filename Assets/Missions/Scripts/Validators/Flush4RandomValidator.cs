using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flush4RandomValidator : BaseValidator
{
    public Flush4RandomValidator(string type) : base(type) {}

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
