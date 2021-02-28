using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PileDropZone
    : CardBase, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void CardDroppedIntoPile(int cardValue, int pileIndex);
    public static event CardDroppedIntoPile cardDroppedIntoPile;

    public static UnityEngine.Color DroppableColor = new UnityEngine.Color(0, 0, 0, 0.2f);
    public static UnityEngine.Color DefaultColor = new UnityEngine.Color(0, 0, 0, 0.0f);

    public int pileIndex;

    private Image dropImageCache;

    protected Image DropImageComp {
        get {
            if ( this.dropImageCache ==  null)
                this.dropImageCache = this.GetComponent<Image>();
            return this.dropImageCache;
        }
    }

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
        if (eventData.pointerDrag == null || cardDroppedIntoPile == null) return;

        CardInHand comp = eventData.pointerDrag.GetComponent<CardInHand>();
        if (comp != null && this.CardIsValid(comp)) {
            comp.CardDroppedIntoPile = true;
            cardDroppedIntoPile(comp.Value, this.pileIndex);
            this.Value = comp.Value;
            this.SetDefaultUI();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        CardInHand comp = eventData.pointerDrag.GetComponent<CardInHand>();
        if (comp != null && this.CardIsValid(comp)) this.SetDroppableCardUI();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        CardInHand comp = eventData.pointerDrag.GetComponent<CardInHand>();
        if (comp != null && this.CardIsValid(comp)) this.SetDefaultUI();
    }

    private bool CardIsValid(CardInHand card) {
        return this.Color == card.Color ||
            this.Rank == card.Rank;
    }

    private void SetDroppableCardUI() {
        this.DropImageComp.color = DroppableColor;
    }

    private void SetDefaultUI() {
        this.DropImageComp.color = DefaultColor;
    }
}
