using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateScore
{
    // A = 1 , J = 11 , Q = 12 , K = 13 �� Number�� ����
    // A�� 1�̳� 11�� �ɼ� �ִ�
    public static int Calculate(List<int> Card, GameManager.who Who)
    {
        int Score = 0;
        bool GetAce = false;

        //Aó�� ���� (01.06) - �Ϸ�
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
        // A�� ���� �̻��϶� ��� �ٽ�.


        if (Score > 21)
        {
            GameManager.Instance.BustLose(Who);
        }
        else if (Score == 21 && Who == GameManager.who.Player)
        {
            //���� ���º� ����
            //who�� string ���� () (01.06) - �Ϸ�
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
