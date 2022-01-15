using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCheck : MonoBehaviour
{  
    public void endedit()
    {       
        var havemoney = PlayerPrefs.GetFloat("Money");

        string str = UIManager.Instance.BetInput.text;

        int Min = int.Parse(DataManager.GetData(GameManager.Instance.Npcdata, GameManager.Instance.Npcid, "minbet"));
        int Max = int.Parse(DataManager.GetData(GameManager.Instance.Npcdata, GameManager.Instance.Npcid, "maxbet"));

        int inputBettingMoney = 0;
        inputBettingMoney = int.Parse(str);

        //���� ����
        if (havemoney < Min)
        {
            UIManager.Instance.BetInput.text = "";

            return;
        }

        //���ñ��� ������ ���� ������ ����������
        if (havemoney < inputBettingMoney)
        {
            inputBettingMoney = (int)havemoney;

            UIManager.Instance.BetInput.text = inputBettingMoney.ToString();
        }

        if (Min > inputBettingMoney)
        {
            UIManager.Instance.BetInput.text = Min.ToString();
        }
        else if (Max < inputBettingMoney)
        {
            UIManager.Instance.BetInput.text = Max.ToString();
        }

        UIManager.Instance.InputMoney();
    }


}
