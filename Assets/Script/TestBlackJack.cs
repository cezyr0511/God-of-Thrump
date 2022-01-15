using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestBlackJack : MonoBehaviour
{
    // 카드 모양은 4종류
    // 카드 숫자는 13종류
    // 그래서 토탈 52
    // 모양을 갖고 숫자도 가져야 한다
    // 데이터가 두종류
    // Map을 쓸까? 음 셔플 되는 STL이 좋은데
    // 구조체 써야하나???
    // A = 1 , J = 11 , Q = 12 , K = 13 로 Number로 규정
    //spade, club, dia, heart
    //"♠""♣""◆""♥"

    struct Card
    {
        public string Sharp;
        public int Number;
    };

    Card[] card = new Card[52]; //카드는 총 52개

    private void Start()
    {
        CreateCardDeck();

        ShuffleDeck();

        PlayGame();

        //DebugCardDeck();
    }

    private void PlayGame()
    {
       
    }

    #region
    //디버그용
    private void DebugCardDeck()
    {
       foreach(Card i in card)
        {
            if(i.Sharp == "Spade")
            {
                if (i.Number == 1)
                {
                    Debug.Log("♠A");
                }
                else if(i.Number == 11)
                {
                    Debug.Log("♠J");
                }
                else if(i.Number == 12)
                {
                    Debug.Log("♠Q");
                }
                else if(i.Number == 13)
                {
                    Debug.Log("♠K");
                }
                else
                {
                    Debug.Log("♠" + i.Number);
                }
            }
            else if(i.Sharp == "Club")
            {
                if (i.Number == 1)
                {
                    Debug.Log("♣A");
                }
                else if (i.Number == 11)
                {
                    Debug.Log("♣J");
                }
                else if (i.Number == 12)
                {
                    Debug.Log("♣Q");
                }
                else if (i.Number == 13)
                {
                    Debug.Log("♣K");
                }
                else
                {
                    Debug.Log("♣" + i.Number);
                }
            }
            else if(i.Sharp == "Dia")
            {
                if (i.Number == 1)
                {
                    Debug.Log("◆A");
                }
                else if (i.Number == 11)
                {
                    Debug.Log("◆J");
                }
                else if (i.Number == 12)
                {
                    Debug.Log("◆Q");
                }
                else if (i.Number == 13)
                {
                    Debug.Log("◆K");
                }
                else
                {
                    Debug.Log("◆" + i.Number);
                }
            }
            else
            {
                if (i.Number == 1)
                {
                    Debug.Log("♥A");
                }
                else if (i.Number == 11)
                {
                    Debug.Log("♥J");
                }
                else if (i.Number == 12)
                {
                    Debug.Log("♥Q");
                }
                else if (i.Number == 13)
                {
                    Debug.Log("♥K");
                }
                else
                {
                    Debug.Log("♥" + i.Number);
                }
            }

          
            
        }
    }
    #endregion //디버그
    private void CreateCardDeck()
    {
        int Num = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j <= 13; j++)
            {
                if (i == 0)
                {
                    card[Num].Sharp = "Spade";
                }
                else if (i == 1)
                {
                    card[Num].Sharp = "Club";
                }
                else if (i == 2)
                {
                    card[Num].Sharp = "Dia";
                }
                else
                {
                    card[Num].Sharp = "Heart";
                }

                card[Num].Number = j;

                Num++;
            }
        }
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < 52; i++)
        {
            int Randomindex = UnityEngine.Random.Range(0, 52);
            int Randomindex2 = UnityEngine.Random.Range(0, 52);

            Card Temp = card[Randomindex]; //랜덤으로 뽑은 놈 빈곳으로 옮기고

            card[Randomindex] = card[Randomindex2]; // 두번 섞기 위해

            card[Randomindex2] = Temp;

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

        }
    }
}


