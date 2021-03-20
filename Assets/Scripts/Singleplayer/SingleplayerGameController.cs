using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SingleplayerGameController : MonoBehaviour
{
    private DeckManager deck;
    private MissionManager missions;

    public GameObject CardPrefab;
    public GameObject MissionPrefab;
    public GameObject EndOfGameDialog;
    public Text EndOfGameScore;

    public GameObject cardsInHandContainer;
    public GameObject missionsContainer;
    public GameObject[] piles;
    public PileDropZone[] pilesCards;
    public GameObject[] missionsCards;
    public Text remainingCardsTxt;
    public Text missionsAcomplishedTxt;

    private int AcomplishedMissions { get => Mathf.Max(0, this.missions.Total - this.missions.Count - 4); }

    private void OnEnable()
    {
        PileDropZone.cardDroppedIntoPile += CardDroppedIntoPile;
        MissionUserInputController.missionClicked += CheckMissionAcomplished;

        this.missionsCards = new GameObject[4];
        this.pilesCards = new PileDropZone[4];
        for (int i = 0; i < this.piles.Length; i++)
        {
            this.pilesCards[i] = this.piles[i].GetComponent<PileDropZone>();
        }
    }

    private void Start() {
        InitializeGame();
    }

    public void ResetGame() {
        this.RemoveCardsFromBoard();
        this.InitializeGame();
    }

    private void InitializeGame() {
        if(this.EndOfGameDialog != null) {
            this.EndOfGameDialog.SetActive(false);
        }
        this.deck = new DeckManager();
        this.missions = new MissionManager();
        StartCoroutine(DealInitialCards());
    }

    private void RemoveCardsFromBoard() {
        for(int i = 0; i < this.cardsInHandContainer.transform.childCount; i++) {
            Destroy(this.cardsInHandContainer.transform.GetChild(i).gameObject);
        }
        foreach (var card in this.pilesCards)
        {
            card.Value = -1;
        }
        for(int i = 0; i < this.missionsContainer.transform.childCount; i++) {
            Destroy(this.missionsContainer.transform.GetChild(i).gameObject);
        }
    }

    private IEnumerator DealInitialCards() {
        for (int i = 0; i < 4; i++) {
            yield return new WaitForSeconds(0.1f);
            this.HandOutCard();
        }
                
        foreach (GameObject pile in piles){
            yield return new WaitForSeconds(0.1f);
            pile.GetComponent<PileDropZone>().Value = this.deck.Draw();
            this.UpdateRemainingCardsText();
        }
        for (int i = 0; i < 4; i++) {
            yield return new WaitForSeconds(0.1f);
            this.HandOutMission(null);
        }
        this.CheckGameEnd();
    }

    private void HandOutCard() {
        if (this.deck.Empty) return;
        GameObject card = Instantiate(this.CardPrefab, this.cardsInHandContainer.transform);
        card.GetComponent<CardInHand>().Value = this.deck.Draw();
        this.UpdateRemainingCardsText();
    }

    private void HandOutMission(int? siblingIndex) {
        MissionDefinition? def = this.missions.Draw();
        if(def == null) return;
        GameObject mission = Instantiate(this.MissionPrefab, this.missionsContainer.transform);
        if(siblingIndex != null) mission.transform.SetSiblingIndex(siblingIndex.Value);
        mission.GetComponent<MissionCard>().Value = def.Value;
        this.UpdateMissionsAcomplishedText();
    }

    public void CardDroppedIntoPile(int cardValue, int pileIndex)
    {
        this.HandOutCard();
        this.CheckGameEnd();
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

    private void UpdateRemainingCardsText() {
        this.remainingCardsTxt.text = string.Format("Cartas restantes\n{0}", this.deck.Count);
    }

    private void UpdateMissionsAcomplishedText() {
        this.missionsAcomplishedTxt.text = string.Format("Misiones cumplidas\n{0}", this.AcomplishedMissions);
    }

    private void CheckGameEnd() {
        int cardsInHandCount = this.cardsInHandContainer.transform.childCount;
        for (int i = 0; i < cardsInHandCount; i++)
        {
            CardInHand card = this.cardsInHandContainer.transform.GetChild(i).GetComponent<CardInHand>();
            if (card == null) {
                continue;
            }

            foreach (PileDropZone pile in this.pilesCards)
            {
                if (pile.CardIsValid(card)) {
                    return;
                }
            }

        }

        // TODO: Notificar al usuario cuando se quede sin cartas.
        Debug.Log("Fin del juego");
        if (this.EndOfGameDialog != null)
        {
            this.EndOfGameScore.text = "" + this.AcomplishedMissions;
            this.EndOfGameDialog.SetActive(true);
        }
    }

    private void OnDisable()
    {
        PileDropZone.cardDroppedIntoPile -= CardDroppedIntoPile;
        MissionUserInputController.missionClicked -= CheckMissionAcomplished;
    }
}
