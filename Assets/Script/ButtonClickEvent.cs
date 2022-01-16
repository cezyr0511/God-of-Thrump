using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class ButtonClickEvent : MonoBehaviour
{
    //타입별로 분리 해주는게 확장성에서 좋다. (01.06) - 완료 partial class로 분류
    public void SceneChange()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Button, "push");

        SceneManager.LoadScene("Main_Scene");
    }
    
}
