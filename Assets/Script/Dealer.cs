using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dealer : MonoBehaviour
{   
    [SerializeField]
    private GameObject CardDeck = null;

    public Text DealerScore = null;

    private string CardSharp = null;
    private int CardNum = 0;

    List<int> GetDealerCard = new List<int>();

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

        GetExtraCard();
    }

    public void GetCard()
    {
        CardDeck.GetComponent<CardDeck>().GetCard();

        CardSharp = CardDeck.GetComponent<CardDeck>().CardSharp;
        CardNum = CardDeck.GetComponent<CardDeck>().CardNumber;

        GetDealerCard.Add(CardNum);

        score = CalculateScore.Calculate(GetDealerCard, GameManager.who.Dealer);

        DealerScore.text = score.ToString();

        //GameManager.Instance.CalculateScore(CardNum, "Dealer");

        UIManager.Instance.SelectCard(CardSharp, CardNum, GameManager.who.Dealer);

        CardNum = 0;
        
        if (PlayerStand)
        {
            GetExtraCard();
        }
    }

    public void GetHideCard()
    {
        UIManager.Instance.HideCard();
    }
    
    private bool PlayerStand = false;
    public void GetExtraCard()
    {
        PlayerStand = true;

       if (score < 17)
       {
            //여기서 반복 시키기. (01.06)
            GetCard();
       }
       else if (score <= 21)
       {
            GameManager.Instance.WhoWinner();            
       }

        PlayerStand = false; //게임이 끝났으므로

    }

    //카드 갖고 있던 리스트 초기화
    public void CardReset()
    {
        GetDealerCard.Clear();
    }

}
