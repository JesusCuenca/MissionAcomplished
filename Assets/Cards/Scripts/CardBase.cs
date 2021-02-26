using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class CardBase : MonoBehaviour
{
    public SpriteAtlas atlas;
    private int _value;
    protected Image imageCache;

    protected virtual Image ImageComp {
        get {
            if(this.imageCache == null) this.imageCache = this.GetComponent<Image>();
            return this.imageCache;
        }
    }

    public int Value {
        get => this._value;
        set {
            this._value = value;
            UpdateImage();
        }
    }
    public Ranks Rank {
        get =>(Ranks)( this._value % Constants.RANK_COUNT );
    }
    public Colors Color {
        get => (Colors)( this._value/Constants.RANK_COUNT );
    }
    public int Number {
        get => (int)this.Rank + 1;
    }

    protected virtual void UpdateImage() {
        this.ImageComp.sprite = GetSprite();
    }

    protected Sprite GetSprite()  {
        string spriteName = string.Format("{0}-{1}", this.Number, Constants.ColorToSpriteName[this.Color]);
        return this.atlas.GetSprite(spriteName);
    }

    public static int GetValueFromRankAndColor(int rank, int color) {
        return (Constants.RANK_COUNT * color) + rank;
    }
}
