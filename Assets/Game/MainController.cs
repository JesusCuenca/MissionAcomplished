using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private DeckManager deck;
    private MissionManager missions;

    public GameObject CardPrefab;
    public GameObject MissionPrefab;

    public GameObject cardsInHandContainer;
    public GameObject missionsContainer;
    public GameObject[] piles;
    public GameObject[] missionsCards;
    public CardBase[] pilesCards;

    private void OnEnable()
    {
        PileDropZone.cardDroppedIntoPile += CardDroppedIntoPile;
        this.deck = new DeckManager();
        this.missions = new MissionManager();

        this.missionsCards = new GameObject[4];
        this.pilesCards = new CardBase[4];
        for (int i = 0; i < this.piles.Length; i++)
        {
            this.pilesCards[i] = this.piles[i].GetComponent<CardBase>();
        }
    }

    private void Start() {
        StartCoroutine(DealInitialCards());
    }

    private IEnumerator DealInitialCards() {
        for (int i = 0; i < 4; i++) {
            yield return new WaitForSeconds(0.1f);
            this.HandOutCard();
        }
                
        foreach (GameObject pile in piles){
            yield return new WaitForSeconds(0.1f);
            pile.GetComponent<PileDropZone>().Value = this.deck.Draw();
        }
        for (int i = 0; i < 4; i++) {
            yield return new WaitForSeconds(0.1f);
            this.HandOutMission();
        }
    }

    private void HandOutCard() {
        if (this.deck.Empty) return;
        GameObject card = Instantiate(this.CardPrefab, this.cardsInHandContainer.transform);
        card.GetComponent<CardInHand>().Value = this.deck.Draw();
    }

    private void HandOutMission() {
        MissionDefinition? def = this.missions.Draw();
        if(def == null) return;
        GameObject mission = Instantiate(this.MissionPrefab, this.missionsContainer.transform);
        mission.GetComponent<MissionCard>().Value = def.Value;
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
