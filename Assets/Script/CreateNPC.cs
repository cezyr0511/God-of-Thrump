using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNPC : MonoBehaviour
{
    public Text npcname = null;

    public void Create(int id, string name, string imgname)
    {
        gameObject.name = id.ToString();        

        npcname.text = name;       

        //char p = '.';

        //string[] str = imgname.Split(p); //.jpg ©���ֱ�

        //GetComponent<Image>().sprite = UIManager.Instance.FineNPCImg(str[0]);       

        GetComponent<Image>().sprite = UIManager.Instance.FineNPCImg(DataManager.GetData(GameManager.Instance.Npcdata, id, "image"));       

    }

}
