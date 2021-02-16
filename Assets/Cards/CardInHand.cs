using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInHand
    : CardBase, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject CardHandGapPrefab;

    private GameObject cardGap;

    private Transform returnTransform;

    private int returnSiblingIndex;

    private Vector3 mouseDiff;

    private CanvasGroup canvasGroupCache;

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
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector3 newPosition = mousePosition - this.mouseDiff;
        this.transform.position = new Vector3(newPosition.x, newPosition.y, 10);

        int siblingIndex = this.returnTransform.childCount;
        for (int i = 0; i < this.returnTransform.childCount; i++)
        {
            if (this.transform.position.x < this.returnTransform.GetChild(i).position.x) {
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
        this.transform.SetParent(this.returnTransform);
        this.transform.SetSiblingIndex(this.cardGap.transform.GetSiblingIndex());
        this.CanvasGroup.blocksRaycasts = true;
        Destroy(this.cardGap);
    }

    private void SpawnCardGap(Transform parent, int siblingIndex)
    {
        this.cardGap = Instantiate(CardHandGapPrefab, parent);
        this.cardGap.transform.SetSiblingIndex(siblingIndex);
    }

   
}
