using UnityEngine;
using UnityEngine.EventSystems;

namespace MissionAcomplished.Cards
{
    public class CardInHand
       : CardBase, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public GameObject CardHandGapPrefab;

        private GameObject cardGap;

        private Transform returnTransform;

        private int returnSiblingIndex;

        private Vector3 mouseDiff;

        private CanvasGroup canvasGroupCache;

        public bool CardDroppedIntoPile { get; set; }

        private CanvasGroup CanvasGroup
        {
            get
            {
                if (canvasGroupCache == null)
                    canvasGroupCache = this.GetComponent<CanvasGroup>();
                return this.canvasGroupCache;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            this.returnTransform = this.transform.parent.transform;
            this.mouseDiff = Camera.main.ScreenToWorldPoint(eventData.position) - this.transform.position;

            this.SpawnCardGap(this.returnTransform, this.transform.GetSiblingIndex());
            this.transform.SetParent(this.transform.parent.parent);
            this.CanvasGroup.blocksRaycasts = false;
            this.CardDroppedIntoPile = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(eventData.position);
            Vector3 newPosition = mousePosition - this.mouseDiff;
            this.transform.position = new Vector3(newPosition.x, newPosition.y, 10);

            int siblingIndex = this.returnTransform.childCount;
            for (int i = 0; i < this.returnTransform.childCount; i++)
            {
                if (this.transform.position.x < this.returnTransform.GetChild(i).position.x)
                {
                    siblingIndex = i;
                    if (this.cardGap.transform.GetSiblingIndex() < siblingIndex)
                        siblingIndex--;
                    break;
                }
            }
            this.cardGap.transform.SetSiblingIndex(siblingIndex);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!this.CardDroppedIntoPile)
            {
                this.transform.SetParent(this.returnTransform);
                this.transform.SetSiblingIndex(this.cardGap.transform.GetSiblingIndex());
                this.CanvasGroup.blocksRaycasts = true;
            }
            else
            {
                Destroy(this.gameObject);
            }
            Destroy(this.cardGap);
        }

        private void SpawnCardGap(Transform parent, int siblingIndex)
        {
            this.cardGap = Instantiate(CardHandGapPrefab, parent);
            this.cardGap.transform.SetSiblingIndex(siblingIndex);
        }

        public void EnableCardGap()
        {
            if (this.cardGap != null)
                this.cardGap.SetActive(true);
        }

        public void DisableCardGap()
        {
            if (this.cardGap != null)
                this.cardGap.SetActive(false);
        }
    }
}
