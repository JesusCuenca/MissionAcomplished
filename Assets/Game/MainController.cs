using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private DeckManager deck;
    public GameObject CardPrefab;
    public GameObject cardsInHandContainer;
    public GameObject[] piles;

    private void OnEnable()
    {
        PileDropZone.cardDroppedIntoPile += CardDroppedIntoPile;
        this.deck = new DeckManager();
    }

    private void Start() {
        for (int i = 0; i < 4; i++) 
            this.HandOutCard();

        foreach (GameObject pile in piles)
            pile.GetComponent<PileDropZone>().Value = this.deck.Draw();
    }

    private void HandOutCard() {
        if (this.deck.Empty) return;
        GameObject card = Instantiate(this.CardPrefab, this.cardsInHandContainer.transform);
        card.GetComponent<CardInHand>().Value = this.deck.Draw();
    }

    public void CardDroppedIntoPile(int cardValue, int pileIndex)
    {

        Debug
            .Log("Card with value " +
            cardValue +
            " was dropped into pile " +
            pileIndex);
        this.HandOutCard();
    }

    private void OnDisable()
    {
        PileDropZone.cardDroppedIntoPile -= CardDroppedIntoPile;
    }
}
