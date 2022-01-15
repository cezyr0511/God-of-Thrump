using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestBlackJack : MonoBehaviour
{
    // ī�� ����� 4����
    // ī�� ���ڴ� 13����
    // �׷��� ��Ż 52
    // ����� ���� ���ڵ� ������ �Ѵ�
    // �����Ͱ� ������
    // Map�� ����? �� ���� �Ǵ� STL�� ������
    // ����ü ����ϳ�???
    // A = 1 , J = 11 , Q = 12 , K = 13 �� Number�� ����
    //spade, club, dia, heart
    //"��""��""��""��"

    struct Card
    {
        public string Sharp;
        public int Number;
    };

    Card[] card = new Card[52]; //ī��� �� 52��

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
    //����׿�
    private void DebugCardDeck()
    {
       foreach(Card i in card)
        {
            if(i.Sharp == "Spade")
            {
                if (i.Number == 1)
                {
                    Debug.Log("��A");
                }
                else if(i.Number == 11)
                {
                    Debug.Log("��J");
                }
                else if(i.Number == 12)
                {
                    Debug.Log("��Q");
                }
                else if(i.Number == 13)
                {
                    Debug.Log("��K");
                }
                else
                {
                    Debug.Log("��" + i.Number);
                }
            }
            else if(i.Sharp == "Club")
            {
                if (i.Number == 1)
                {
                    Debug.Log("��A");
                }
                else if (i.Number == 11)
                {
                    Debug.Log("��J");
                }
                else if (i.Number == 12)
                {
                    Debug.Log("��Q");
                }
                else if (i.Number == 13)
                {
                    Debug.Log("��K");
                }
                else
                {
                    Debug.Log("��" + i.Number);
                }
            }
            else if(i.Sharp == "Dia")
            {
                if (i.Number == 1)
                {
                    Debug.Log("��A");
                }
                else if (i.Number == 11)
                {
                    Debug.Log("��J");
                }
                else if (i.Number == 12)
                {
                    Debug.Log("��Q");
                }
                else if (i.Number == 13)
                {
                    Debug.Log("��K");
                }
                else
                {
                    Debug.Log("��" + i.Number);
                }
            }
            else
            {
                if (i.Number == 1)
                {
                    Debug.Log("��A");
                }
                else if (i.Number == 11)
                {
                    Debug.Log("��J");
                }
                else if (i.Number == 12)
                {
                    Debug.Log("��Q");
                }
                else if (i.Number == 13)
                {
                    Debug.Log("��K");
                }
                else
                {
                    Debug.Log("��" + i.Number);
                }
            }

          
            
        }
    }
    #endregion //�����
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

            Card Temp = card[Randomindex]; //�������� ���� �� ������� �ű��

            card[Randomindex] = card[Randomindex2]; // �ι� ���� ����

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


