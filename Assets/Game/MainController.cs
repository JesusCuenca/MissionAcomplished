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
        MissionUserInputController.missionClicked += CheckMissionAcomplished;
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
            this.HandOutMission(null);
        }
    }

    private void HandOutCard() {
        if (this.deck.Empty) return;
        GameObject card = Instantiate(this.CardPrefab, this.cardsInHandContainer.transform);
        card.GetComponent<CardInHand>().Value = this.deck.Draw();
    }

    private void HandOutMission(int? siblingIndex) {
        MissionDefinition? def = this.missions.Draw();
        if(def == null) return;
        GameObject mission = Instantiate(this.MissionPrefab, this.missionsContainer.transform);
        if(siblingIndex != null) mission.transform.SetSiblingIndex(siblingIndex.Value);
        mission.GetComponent<MissionCard>().Value = def.Value;
    }

    public void CardDroppedIntoPile(int cardValue, int pileIndex)
    {
        this.HandOutCard();
    }

    public void CheckMissionAcomplished(GameObject missionGO) {
        MissionCard mission = missionGO.GetComponent<MissionCard>();
        if(!mission) {
            Debug.Log("No se ha encontrado el componente de la misión.");
            return;
        }
        if (!mission.IsAcomplished(this.pilesCards))
        {
            Debug.Log("La missión no se ha completado!");
            return;
        }
        this.HandOutMission(missionGO.transform.GetSiblingIndex());
        Destroy(missionGO);
    }

    private void OnDisable()
    {
        PileDropZone.cardDroppedIntoPile -= CardDroppedIntoPile;
        MissionUserInputController.missionClicked -= CheckMissionAcomplished;
    }
}
