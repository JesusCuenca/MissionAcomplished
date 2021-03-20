using UnityEngine;
using UnityEngine.EventSystems;

namespace MissionAcomplished.Cards
{
    public class CardsInHandContainer
       : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;

            CardInHand comp = eventData.pointerDrag.GetComponent<CardInHand>();
            if (comp != null) comp.EnableCardGap();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;

            CardInHand comp = eventData.pointerDrag.GetComponent<CardInHand>();
            if (comp != null) comp.DisableCardGap();
        }
    }
}
