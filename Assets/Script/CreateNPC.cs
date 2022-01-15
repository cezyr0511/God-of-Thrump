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

        char p = '.';

        string[] str = imgname.Split(p); //.jpg Â©¶óÁÖ±â

        GetComponent<Image>().sprite = UIManager.Instance.FineNPCImg(str[0]);       
    }
    
}
