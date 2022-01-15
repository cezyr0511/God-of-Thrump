using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{    
    struct Card
    {
        private string sharp;
        private int number;

        public string Sharp 
        { 
            get => sharp; 
            set => sharp = value; 
        }
        public int Number 
        { 
            get => number; 
            set => number = value; 
        }
    }

    Card[] card = new Card[52]; //카드는 총 52개

    public void CardDeckStart()
    {
        CreateCardDeck();

        ShuffleDeck();
    }    

    private void CreateCardDeck()
    {
        int Num = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j <= 13; j++)
            {
                if (i == 0)
                {
                    card[Num].Sharp = "S";
                }
                else if (i == 1)
                {
                    card[Num].Sharp = "C";
                }
                else if (i == 2)
                {
                    card[Num].Sharp = "D";
                }
                else
                {
                    card[Num].Sharp = "H";
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

    public string CardSharp;
    public int CardNumber;
    private int CardDeckNum = 0;

    //구조체로 리턴?
    public void GetCard()
    {
        //매번 초기화?
        CardSharp = "";
        CardNumber = 0;

        CardSharp = card[CardDeckNum].Sharp;
        CardNumber = card[CardDeckNum].Number;

        CardDeckNum++;
    }

    public void ResetDeck()
    {
        CardDeckNum = 0;
    }

}
