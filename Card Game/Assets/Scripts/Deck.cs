using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Deck : MonoBehaviour
{
    public List<Card> cards;
    private int cardsLeft;
    public List<Card> Hand;
    private List<Vector3> cardPositions;
    private TextMeshProUGUI _actionText;

    void Start()
    {
        GameManager.RegisterDeck(this);
        cards = new List<Card>();
        Transform _cards = transform.Find("Cards");
        foreach (Transform card in _cards){
            cards.Add(card.GetComponent<Card>());
        }
        Hand = new List<Card>();
        cardPositions = new List<Vector3>(){
            new Vector3(-12,0,0),
            new Vector3(-10,0,0),
            new Vector3(-8,0,0),
            new Vector3(-6,0,0),
            new Vector3(-4,0,0)
        };
        _actionText = transform.Find("Canvas/ActionText").GetComponent<TextMeshProUGUI>();
    }
    public void DrawCard(){
        if(Hand.Count == 5 || cards.Count == 0) return; 
        Card card = GetRandomCard();
        card.gameObject.SetActive(true);
        Hand.Add(card);
        card.handIndex = Hand.Count-1;
        MoveCardsToPosition();        
    }
    void MoveCardsToPosition(){
        for (int i = 0; i < Hand.Count; i++)
        {
            Vector3 targetPosition = transform.TransformPoint(cardPositions[i]);
            Hand[i].MoveTowards(targetPosition);
        }
    }
    Card GetRandomCard(){
        int index = Random.Range(0,cards.Count);
        Card card = cards[index];
        cards.RemoveAt(index);
        return card;
    }
    public void ResetHandIndexes(){
        for (int i = 0; i < Hand.Count; i++)
        {
            Hand[i].handIndex = i;
        }
    }
    public void PlayCard(int index){
        Destroy(Hand[index].gameObject);
        Hand.RemoveAt(index);
        ResetHandIndexes();
        MoveCardsToPosition();
        SetActionPointText();
        SetCardsColor();
    }
    public void SetActionPointText(){
        _actionText.text = "Actions: "+GameManager.actions;
    }
    public void SetCardsColor(){
        Hand.ForEach(card=>{
            card.SetColor();
        });
    }
}