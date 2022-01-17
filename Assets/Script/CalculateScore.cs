using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateScore
{
    // A = 1 , J = 11 , Q = 12 , K = 13 로 Number로 규정
    // A는 1이나 11이 될수 있다
    public static int Calculate(List<int> Card, GameManager.who Who)
    {
        int Score = 0;
        bool GetAce = false;

        //A처리 수정 (01.06) - 완료
        foreach (int CardNum in Card)
        {
            if (CardNum == 1)
            {
                GetAce = true;

                //if (Score < 11)
                //{
                //    Score += 11;
                //}
                //else
                //{
                //    Score += 1;
                //}

                Score += 11;

            }
            else if (CardNum == 11 || CardNum == 12 || CardNum == 13)
            {
                Score += 10;
            }
            else
            {
                Score += CardNum;
            }
        }     

        if (GetAce)
        {
            if (Score > 21)
            {
                Score -= 10;
            }
        }
        // A가 두장 이상일때 계산 다시.


        if (Score > 21)
        {
            GameManager.Instance.BustLose(Who);
        }
        else if (Score == 21 && Who == GameManager.who.Player)
        {
            //블랙잭 무승부 있음
            //who를 string ㄴㄴ () (01.06) - 완료
            //GameManager.Instance.BlackJackWin(Who);

            //GameManager.Instance.StandButton();
            if (!GameManager.Instance.cardset)
            {
                GameManager.Instance.playerblackjack = true;
            }
            else
            {
                GameManager.Instance.StandButton();
            }
            
        }

        return Score;
    }
}
