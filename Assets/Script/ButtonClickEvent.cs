using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class ButtonClickEvent : MonoBehaviour
{
    //Ÿ�Ժ��� �и� ���ִ°� Ȯ�强���� ����. (01.06) - �Ϸ� partial class�� �з�
    public void SceneChange()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Button, "push");

        SceneManager.LoadScene("Main_Scene");
    }
    
}
