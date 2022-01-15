using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClickEvent : MonoBehaviour
{
    //타입별로 분리 해주는게 확장성에서 좋다. (01.06)
    public void SceneChange()
    {
        SceneManager.LoadScene("Main_Scene");
    }

    public void SelectPlayer()
    {
        PlayerPrefs.SetInt("First", 1); //최초 접속시

        var a = this.transform.parent.Find("Grid_Layout_Group").gameObject;       

        for (int i = 0; i < a.transform.childCount; i++)
        {
            if (a.transform.GetChild(i).GetComponent<Toggle>().isOn)
            {
                GameManager.Instance.Playerid = int.Parse(a.transform.GetChild(i).name);

                break;
            }
        }

        UIManager.Instance.UI_Active("NPCSelect", true);

        UIManager.Instance.UI_Active("PlayerSelect", false);

    }

    public void SelectNPC()
    {
        GameManager.Instance.Npcid = int.Parse(name);

        //UIManager.Instance.ResetGameUI();       

        UIManager.Instance.UI_Active("Play", true);

        UIManager.Instance.UI_Active("NPCSelect", false);
    }

    // 각각 함수 로 처리보단 하나 함수로 처리. (01.06)

    public void Stand()
    {
        //GameManager.Instance.MovePoint();
        GameManager.Instance.StandButton();
    }

    public void Hit()
    {
        GameManager.Instance.HitButton();
    }

    public void Max()
    {
        GameManager.Instance.MaxButton();
    }

    public void Min()
    {
        GameManager.Instance.MinButton();
    }

    public void Bet()
    {
        UIManager.Instance.ShowDialog(2);

        GameManager.Instance.GamePlay(); //베팅했으니 게임 시작
    }

    public void Result()
    {
        UIManager.Instance.Result.SetActive(false);

        GameManager.Instance.GameReset();        
    }

    public void InputMoney()
    {
        UIManager.Instance.InputMoney();
    }

    public void Charge()
    {      
        GameManager.Instance.ChargeMoney();
    }

}
