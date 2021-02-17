using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PileDropZone
    : CardBase, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void CardDroppedIntoPile(int cardValue, int pileIndex);
    public static event CardDroppedIntoPile cardDroppedIntoPile;

    public int pileIndex;

    protected override Image ImageComp {
        get {
            if ( this.imageCache == null ) {
                this.imageCache = this.transform.GetChild(0).GetComponent<Image>();
            }
            return this.imageCache;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {      
        if (eventData.pointerDrag == null) return;

        CardInHand comp = eventData.pointerDrag.GetComponent<CardInHand>();
        if (comp != null && cardDroppedIntoPile != null) {
            cardDroppedIntoPile(comp.Value, this.pileIndex);
            this.Value = comp.Value;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        CardInHand comp = eventData.pointerDrag.GetComponent<CardInHand>();
        if (comp != null) comp.DisableCardGap();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        CardInHand comp = eventData.pointerDrag.GetComponent<CardInHand>();
        if (comp != null) comp.EnableCardGap();
    }
}
