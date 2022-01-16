using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlayer : MonoBehaviour
{
    public Image PlayerName = null;

    public void Create(int id)
    {
        gameObject.name = id.ToString();

        PlayerName.sprite = UIManager.Instance.FinePlayerImg(id.ToString());
    }

}
