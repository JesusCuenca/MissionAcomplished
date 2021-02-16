using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase : MonoBehaviour
{
    private int _value;

    public Ranks Rank {
        get =>(Ranks)( this._value % Constants.RANK_COUNT );
    }
    public Colors Color {
        get => (Colors)( this._value/Constants.RANK_COUNT );
    }
}
