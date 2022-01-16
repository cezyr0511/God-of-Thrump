using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class ButtonClickEvent : MonoBehaviour
{   
    public void SelectPlayer()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Button, "push");

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

        PlayerPrefs.SetInt("PlayerId", GameManager.Instance.Playerid);

        //UIManager.Instance.UI_Active("NPCSelect", true);

        //UIManager.Instance.UI_Active("PlayerSelect", false);

        //UIManager.Instance.UI_Active("NPCSelect");

        UIManager.Instance.UI_Active(UIManager.UI_NAME.NPCSelect);

    }

    public void SelectNPC()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Button, "push");

        GameManager.Instance.Npcid = int.Parse(name);

        //UIManager.Instance.ResetGameUI();       

        //UIManager.Instance.UI_Active("Play", true);

        //UIManager.Instance.UI_Active("NPCSelect", false);

        //UIManager.Instance.UI_Active("Play");

        UIManager.Instance.UI_Active(UIManager.UI_NAME.Play);
    }
}
