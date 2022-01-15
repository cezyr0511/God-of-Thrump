using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{   
    [SerializeField]
    private GameObject CardDeck = null;

    public Text MyScore = null;

    private string CardSharp = null;
    private int CardNum = 0;

    List<int> GetMyCard  = new List<int>();

    private int score;
    public int Score 
    {
        get => score;

        set => score = value;
    }

    public void StartCo()
    {
        StartCoroutine("Co_GetCard");
    }

    public IEnumerator Co_GetCard()
    {
        yield return new WaitForSeconds(0.3f);

        GetCard();
    }

    public void GetCard()
    {
        CardDeck.GetComponent<CardDeck>().GetCard();

        CardSharp = CardDeck.GetComponent<CardDeck>().CardSharp;
        CardNum = CardDeck.GetComponent<CardDeck>().CardNumber;

        GetMyCard.Add(CardNum);

        score = CalculateScore.Calculate(GetMyCard, GameManager.who.Player);

        MyScore.text = score.ToString();

        UIManager.Instance.SelectCard(CardSharp, CardNum, GameManager.who.Player);

        CardNum = 0;
    }          

    //카드 갖고 있던 리스트 초기화
    public void CardReset()
    {
        GetMyCard.Clear();
    }
   
}
