using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    private GameObject CardDeck = null;
    private GameObject Player = null;
    private GameObject Dealer = null;

    List<Dictionary<string, object>> npcdata;
    List<Dictionary<string, object>> pcdata;
    List<Dictionary<string, object>> settingdata;

    private int npcid;
    public int Npcid
    {
        get => npcid;
        set => npcid = value;
    }

    private int playerid = 1;
    public int Playerid
    {
        get => playerid;
        set => playerid = value;
    }

    public List<Dictionary<string, object>> Npcdata
    {
        get => npcdata;
        set => npcdata = value;
    }
    public List<Dictionary<string, object>> Pcdata
    {
        get => pcdata;
        set => pcdata = value;
    }

    private float npcmoney;
    public float Npcmoney
    {
        get => npcmoney;
        set => npcmoney = value;
    }

    private float playermoney;
    public float Playermoney
    {
        get => playermoney;
        set => playermoney = value;
    }

    public enum GameState
    {
        Betting,
        Playing,
        Resulting
    }

    public GameState GS
    {
        get;
        set;
    }

    public enum who
    {
        Player,
        Dealer,
        Draw
    }

    public who Who
    {
        get;
        set;
    }


    private int BettingMoney = 100;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this);
        }       

        //CSV �б�
        npcdata = CSVReader.Read("NPC_Table");
        pcdata = CSVReader.Read("PC_Table");
        settingdata = CSVReader.Read("Setting_Table");

        //���� ���� �� PlayerPrefs�� �� ����
        //ó������ �ƴ��� �Ǵ�
        if (!PlayerPrefs.HasKey("First"))
        {
            var basicmoney = float.Parse(settingdata[0]["basicmoney"].ToString());

            PlayerPrefs.SetFloat("Money", basicmoney);

            PlayerPrefs.SetInt("First", 0);

            GameObject.Find("Canvas").transform.Find("MY_Money").GetComponent<Text>().text = PlayerPrefs.GetFloat("Money").ToString();
        }
        else
        {
            GameObject.Find("Canvas").transform.Find("MY_Money").GetComponent<Text>().text = PlayerPrefs.GetFloat("Money").ToString();

            playerid = PlayerPrefs.GetInt("PlayerId");
        }

    }

    //���ӸŴ��� ������Ƽ
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }

    /*
    public IEnumerator GamePlay()
    {       
        StartBlackJack();

        // �⺻ ���徿 ��� BlackJack �̳� Bust ���� ����� �����Ұųİ� ����       
        Debug.Log("HIT or STAY ?");
       
        //������ �÷��̾ ī�� �ޱ� ������ ���
        yield return StartCoroutine(WaitPoint());

        if (Playing)
        {
            WhoWinner();
        }      

    }
    */

    Coroutine Co;
    public void GamePlay()
    {
        if (CardDeck == null)
        {
            CardDeck = GameObject.Find("CardDeck");
            Player = GameObject.Find("Player");
            Dealer = GameObject.Find("Dealer");
        }

        MoneyBetting();

        UIManager.Instance.ShowBalance();

        Co = StartCoroutine(StartBlackJack());

        //if (MoneyBetting())
        //{
        //    UIManager.Instance.ShowBalance();

        //    Co = StartCoroutine(StartBlackJack());

        //    //StartBlackJack();
        //}
        //else
        //{
        //    Debug.Log("�ٽú���");
        //}

    }

    private bool bWaitPoint = false;
    IEnumerator WaitPoint()
    {
        bWaitPoint = true;

        while (bWaitPoint)
        {
            yield return null;
        }
    }

    public void MovePoint()
    {
        bWaitPoint = false;
    }

    public bool cardset = false;
    public bool playerblackjack = false;

    float waittime = 0.9f;
    public IEnumerator StartBlackJack()
    {
        cardset = false;

        playerblackjack = false;

        GS = GameState.Playing;

        UIManager.Instance.ButtonControl();

        CardDeck.GetComponent<CardDeck>().CardDeckStart();

        Player.GetComponent<Player>().GetCard();        

        yield return new WaitForSeconds(waittime);

        Dealer.GetComponent<Dealer>().GetCard();

        yield return new WaitForSeconds(waittime);

        Player.GetComponent<Player>().GetCard();

        yield return new WaitForSeconds(waittime);

        Dealer.GetComponent<Dealer>().GetHideCard();

        SoundManager.Instance.PlaySound(SoundManager.SoundType.Button, "card_dealing");

        //yield return StartCoroutine(WaitPoint());

        //if (GS == GameState.Playing)
        //{
        //    GameManager.Instance.StandButton();
        //}

    }

    //private void StartBlackJack() //�⺻ ����
    //{
    //    GS = GameState.Playing;

    //    UIManager.Instance.ButtonControl();

    //    CardDeck.GetComponent<CardDeck>().CardDeckStart();

    //    Player.GetComponent<Player>().GetCard();

    //    Dealer.GetComponent<Dealer>().GetCard();

    //    Player.GetComponent<Player>().GetCard();

    //    Dealer.GetComponent<Dealer>().GetCard();

    //    //for (int i = 0; i < 2; i++)
    //    //{
    //    //    //�÷��̾����� ���� �������Ѵ�.
    //    //    Player.GetComponent<Player>().GetCard();

    //    //    //�������� ���� �������Ѵ�.
    //    //    Dealer.GetComponent<Dealer>().GetCard();
    //    //}
    //}

    //black (01.06)

    public void BustLose(GameManager.who Who)
    {
        GS = GameState.Resulting;

        UIManager.Instance.ButtonControl();

        if (Who == who.Player)
        {
            UIManager.Instance.ResultUI("LOSE");

            UIManager.Instance.ShowDialog(3);

            ResultMoney(who.Dealer);
        }
        else
        {
            UIManager.Instance.ResultUI("WIN");

            UIManager.Instance.ShowDialog(4);

            ResultMoney(who.Player);
        }

        GameManager.Instance.MovePoint();
    }

    public void HitButton()
    {
        //Player.GetComponent<Player>().GetCard();

        Player.GetComponent<Player>().StartCo();
    }

    public void StandButton()
    {
        //Dealer.GetComponent<Dealer>().GetExtraCard();

        Dealer.GetComponent<Dealer>().StartCo();
    }

    public void MaxButton()
    {
        UIManager.Instance.BetInput.text = DataManager.GetData(npcdata, npcid, "maxbet");

        UIManager.Instance.BetInput.GetComponent<InputCheck>().endedit();
    }

    public void MinButton()
    {
        UIManager.Instance.BetInput.text = DataManager.GetData(npcdata, npcid, "minbet");

        UIManager.Instance.BetInput.GetComponent<InputCheck>().endedit();
    }

    private int BlackJackNum = 21;

    public void WhoWinner()
    {
        GS = GameState.Resulting;

        UIManager.Instance.ButtonControl();

        int playerscore = Player.GetComponent<Player>().Score;
        int dealerscore = Dealer.GetComponent<Dealer>().Score;

        //�񱳸� ���� �ӽ�
        //etc) ���� ���� ����ϴ� ����� ����(01.06) - �Ϸ�
        int Temp = BlackJackNum - playerscore;
        int Dest = BlackJackNum - dealerscore;

        #region ��� ù��°
        /*
        if (Dest < 0)
        {
            Dest *= -1;
        }

        if (Temp < Dest)
        {
            UIManager.Instance.ResultUI("WIN!");

            UIManager.Instance.ShowDialog(4);

            ResultMoney(who.Player);
        }
        else if (Temp > Dest)
        {
            UIManager.Instance.ResultUI("LOSE!");

            UIManager.Instance.ShowDialog(3);

            ResultMoney(who.Dealer);
        }
        else //�����
        {
            UIManager.Instance.ResultUI("DRAW!");

            playermoney += BettingMoney;

            PlayerPrefs.SetFloat("Money", playermoney);

            //ResultMoney("Draw");
        }
        */
        #endregion

        #region ��� �ι�°

        int result = playerscore - dealerscore;

        //����� �÷��̾� win
        //������ ���� win
        //0 �̸� draw

        if (result > 0)
        {
            UIManager.Instance.ResultUI("WIN");

            UIManager.Instance.ShowDialog(4);

            ResultMoney(who.Player);
        }
        else if (result < 0)
        {
            UIManager.Instance.ResultUI("LOSE");

            UIManager.Instance.ShowDialog(3);

            ResultMoney(who.Dealer);
        }
        else
        {
            UIManager.Instance.ResultUI("DRAW");

            playermoney += BettingMoney;

            PlayerPrefs.SetFloat("Money", playermoney);
        }

        #endregion
    }


    private void Update()
    {       

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void GameReset()
    {
        UIManager.Instance.ResetGameUI(); // UI �ʱ�ȭ

        //���� �ִ� ī�� ����Ʈ �ʱ�ȭ
        Player.GetComponent<Player>().CardReset();
        Dealer.GetComponent<Dealer>().CardReset();

        Player.GetComponent<Player>().Score = 0;
        Dealer.GetComponent<Dealer>().Score = 0; //���� ����       

        CardDeck.GetComponent<CardDeck>().ResetDeck(); //ī�嵦 ī��Ʈ ����

        GS = GameState.Betting;

        UIManager.Instance.ButtonControl();

    }

    public string GetDialog(int DialogNum)
    {
        int dataindex = npcid - 1;

        string Dialog = "dialog_" + DialogNum.ToString();

        return npcdata[dataindex][Dialog].ToString();
    }

    public void MoneySetting(int id)
    {
        int dataindex = id - 1;

        playermoney = PlayerPrefs.GetFloat("Money");

        npcmoney = (int)npcdata[dataindex]["balance"];

        UIManager.Instance.BetRange.text = "( " + npcdata[dataindex]["minbet"].ToString() + " - " + npcdata[dataindex]["maxbet"].ToString() + " )";
    }

    //����(01.06) - �Ϸ�
    public void MoneyBetting()
    {
        string str = UIManager.Instance.BetInput.text;

        //int Min = int.Parse(DataManager.GetData(npcdata, npcid, "minbet"));
        //int Max = int.Parse(DataManager.GetData(npcdata, npcid, "maxbet"));

        //���� �˻�
        //string p = "^[" + Min.ToString() + "-" + Max.ToString() + "]*$";

        //���øӴ� �־��ֱ�
        BettingMoney = int.Parse(str);

        playermoney -= BettingMoney;

        PlayerPrefs.SetFloat("Money", playermoney);

        //if (Regex.IsMatch(str, p))
        //if (Min <= BettingMoney && BettingMoney <= Max)
        //{
        //    playermoney -= BettingMoney;

        //    return true;           
        //}
        //else
        //{
        //    Debug.Log("���� �ʰ�");

        //    return false;
        //}
    }

    public void ResultMoney(who who)
    {

        if (who == who.Player)
        {
            playermoney += (BettingMoney * 2);

            if (playermoney >= 9999999999999999999)
            {
                playermoney = 9999999999999999999;
            }

            PlayerPrefs.SetFloat("Money", playermoney);

            npcmoney -= BettingMoney;

        }
        else if (who == who.Dealer)
        {
            npcmoney += BettingMoney;
        }


        //NPC ����
        if (npcmoney <= 0)
        {
            UIManager.Instance.UI_Active(UIManager.UI_NAME.NPCSelect);

            //UIManager.Instance.UI_Active("Play", false);

            GameReset();

            UIManager.Instance.Result.SetActive(false);

            //UIManager.Instance.UI_Active("NPCSelect", true);
        }

        if (playermoney <= 0)
        {
            playermoney = 0;

            PlayerPrefs.SetFloat("Money", playermoney);

            UIManager.Instance.Result.SetActive(false);

            UIManager.Instance.Charge.SetActive(true);

            //UIManager.Instance.UI_Active("Charge", true);
        }

    }

    public void ChargeMoney()
    {
        var chargemoney = float.Parse(settingdata[0]["chargemoney"].ToString());

        playermoney += chargemoney;

        PlayerPrefs.SetFloat("Money", playermoney);

        GameReset();

        //UIManager.Instance.UI_Active("Play", false);

        //UIManager.Instance.UI_Active("Charge", false);

        //UIManager.Instance.UI_Active("NPCSelect", true);

        UIManager.Instance.Charge.SetActive(false);

        UIManager.Instance.UI_Active(UIManager.UI_NAME.NPCSelect);

    }


}
