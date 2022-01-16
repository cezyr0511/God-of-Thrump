using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ButtonClickEvent : MonoBehaviour
{
    public void Stand()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Button, "push");
        //GameManager.Instance.MovePoint();
        GameManager.Instance.StandButton();
    }

    public void Hit()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Button, "push");
        GameManager.Instance.HitButton();
    }

    public void Max()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Button, "push");
        GameManager.Instance.MaxButton();
    }

    public void Min()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Button, "push");
        GameManager.Instance.MinButton();
    }

    public void Bet()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Button, "bet");

        UIManager.Instance.ShowDialog(2);

        GameManager.Instance.GamePlay(); //베팅했으니 게임 시작
    }

    public void Result()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Button, "push");

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
