using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    const int TRUE = 0;
    const int FLASE = 1;

    public static UIManager Instance;

    private Sprite[] CardImage;
    private Sprite[] NPCImage;
    private Sprite[] PlayerImage;

    public Image[] PlayerCardUI;
    public Image[] DealerCardUI;

    [Header("UI")]
    public GameObject PlayerSelect = null;
    public GameObject NPCSelect = null;
    public GameObject Play = null;
    public GameObject Charge = null;

    [Header("Result")]
    public GameObject Result = null;
    public Text ResultText = null;

    [Header("Button")]
    public Button HitButtons = null;
    public Button StandButtons = null;
    public Button MinButtons = null;
    public Button MaxButtons = null;
    public Button BetButtons = null;

    [Header("Bet")]
    public InputField BetInput = null;
    public Text BetRange = null;
    public Text BetMoneyText = null;

    [Header("Img")]   
    public Image Player_Img = null;
    public Image NPC_Img = null;
    public Text NPC_Dialog = null;

    [Header("Money")]
    public Text My_Money = null;
    public Text NPC_Money = null;

    List<Image> Npcimages;

    GameManager GM = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CardImage = Resources.LoadAll<Sprite>("UI/cards/");
        NPCImage = Resources.LoadAll<Sprite>("UI/stage/");
        PlayerImage = Resources.LoadAll<Sprite>("UI/player/");

        if (GM == null)
        {
            GM = GameManager.Instance;
        }

        if (PlayerPrefs.GetInt("First") == TRUE)
        {
            UI_Active("PlayerSelect", true);
        }
        else
        {
            UI_Active("NPCSelect", true);
        }
    }

    //어떤 UI 활성화 할지?
    public void UI_Active(string UI_Name, bool active)
    {
        if (UI_Name == "PlayerSelect")
        {
            PlayerSelect.SetActive(active);

            CreatePlayerSelect();
        }
        else if (UI_Name == "NPCSelect")
        {
            NPCSelect.SetActive(active);

            if (GameObject.Find("Canvas").transform.Find("NPC_Scroll_View").Find("Viewport").Find("Content").gameObject.transform.childCount == 0)
            {
                CreateNPCSelect();
            }

            //StageCheck();
        }
        else if (UI_Name == "Play")
        {
            GM.MoneySetting(GM.Npcid);

            ResetGameUI();

            Play.SetActive(active);

            ShowPlayerImg();
            //ShowBalance();

            GM.GS = GameManager.GameState.Betting;

            ButtonControl();
        }
        else if (UI_Name == "Charge")
        {
            Charge.SetActive(active);
        }
    }

    //public void UI_Active<T>()
    //{
        
    //}

    private int PlayerCardCount = 0;
    private int DealerCardCount = 0;

    public void SelectCard(string CS, int CardNum, GameManager.who Who)
    {
        if (Who == GameManager.who.Player)
        {
            PlayerCardUI[PlayerCardCount].sprite = FindImage(CS, CardNum);

            PlayerCardUI[PlayerCardCount].gameObject.SetActive(true);

            PlayerCardCount++;
        }
        else if (Who == GameManager.who.Dealer)
        {
            DealerCardUI[DealerCardCount].sprite = FindImage(CS, CardNum);

            DealerCardUI[DealerCardCount].gameObject.SetActive(true);

            DealerCardCount++;
        }

    }

    //카드 UI 이미지
    Sprite FindImage(string CS, int CardNum)
    {
        string CardName = "";

        if (CardNum == 11)
        {
            CardName = "J" + CS;
        }
        else if (CardNum == 12)
        {
            CardName = "Q" + CS;
        }
        else if (CardNum == 12)
        {
            CardName = "K" + CS;
        }
        else
        {
            CardName = CardNum.ToString() + CS;
        }

        int Number = 0;

        for (int i = 0; i < CardImage.Length; i++)
        {
            if (CardImage[i].name == CardName)
            {
                Number = i;

                break;
            }
        }

        return CardImage[Number];
    }

    public void ResetGameUI()
    {
        for (int i = 0; i < PlayerCardUI.Length; i++)
        {
            PlayerCardUI[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < DealerCardUI.Length; i++)
        {
            DealerCardUI[i].gameObject.SetActive(false);
        }

        PlayerCardCount = DealerCardCount = 0;

        GameObject.Find("Player").transform.GetComponent<Player>().MyScore.text = "";
        GameObject.Find("Dealer").transform.GetComponent<Dealer>().DealerScore.text = "";

        BetInput.text = "";

        ShowBalance();

        ShowDialog(1);
    }

    public void ResultUI(string GameResult)
    {
        Result.SetActive(true);

        ResultText.text = GameResult;
    }

    //GMGR이동 (01.06) - 완료
    //for은 할 일 끝나면 return (01.06) - 완료
    public Sprite FineNPCImg(string ImgName)
    {
        int num = 0;

        for (int i = 0; i < NPCImage.Length; i++)
        {
            if (NPCImage[i].name == ImgName)
            {
                num = i;       

                break;
            }
        }

        return NPCImage[num];
    }

    public Sprite FinePlayerImg(string id)
    {
        int Num = 0;

        for (int i = 0; i < PlayerImage.Length; i++)
        {
            if (PlayerImage[i].name == id)
            {
                Num = int.Parse(id);

                break;
            }          
        }

        return PlayerImage[Num];
    }

    public void ShowBalance()
    {
        My_Money.text = GM.Playermoney.ToString();

        NPC_Money.text = GM.Npcmoney.ToString();
    }

    public void ShowPlayerImg()
    {
        Player_Img.sprite = FinePlayerImg(GM.Playerid.ToString());
        
        //NPC_Img.sprite = FineNPCImg(GM.Npcid.ToString());
        NPC_Img.sprite = FineNPCImg(DataManager.GetData(GM.Npcdata, GM.Npcid, "image"));
    }

    public void ShowDialog(int DialogNum)
    {
        NPC_Dialog.text = GM.GetDialog(DialogNum);
    }

    //버튼이름 명시적으로 변경 (01.06) - 완료
    public void ButtonControl()
    {
        if (GM.GS == GameManager.GameState.Betting)
        {
            HitButtons.interactable = false;
            StandButtons.interactable = false;
            MinButtons.interactable = true;
            MaxButtons.interactable = true;
            BetButtons.interactable = false;

        }
        else if (GM.GS == GameManager.GameState.Playing)
        {
            HitButtons.interactable = true;
            StandButtons.interactable = true;
            MinButtons.interactable = false;
            MaxButtons.interactable = false;
            BetButtons.interactable = false;
        }
        else if (GM.GS == GameManager.GameState.Resulting)
        {
            HitButtons.interactable = false;
            StandButtons.interactable = false;
            MinButtons.interactable = false;
            MaxButtons.interactable = false;
            BetButtons.interactable = false;
        }
    }

    public void InputMoney()
    {
        BetButtons.interactable = true;
    }

    public void CreatePlayerSelect()
    {
        int DataCount = GM.Pcdata.Count;

        var ToggleGroup = GameObject.Find("Canvas").transform.Find("PlayerSelect").Find("Grid_Layout_Group").gameObject.GetComponent<ToggleGroup>();

        GameObject obj = Resources.Load<GameObject>("Prefab/Player_Toggle");       

        for (int i = 0; i < DataCount; i++)
        {
            GameObject PlayerSelect = Instantiate(obj);

            PlayerSelect.GetComponent<Toggle>().group = ToggleGroup;

            PlayerSelect.GetComponent<CreatePlayer>().Create((int)GM.Pcdata[i]["id"]);

            PlayerSelect.transform.SetParent(ToggleGroup.gameObject.transform);

            ReScale(PlayerSelect, 1.2f);
        }

    }

    public void CreateNPCSelect()
    {
        int DataCount = GM.Npcdata.Count;

        GameObject obj = Resources.Load<GameObject>("Prefab/NPCButton");

        GameObject Content = GameObject.Find("Canvas").transform.Find("NPC_Scroll_View").Find("Viewport").Find("Content").gameObject;

        for (int i = 0; i < DataCount; i++)
        {
            GameObject NPCSelect = Instantiate(obj);

            NPCSelect.GetComponent<CreateNPC>().Create((int)GM.Npcdata[i]["id"], GM.Npcdata[i]["name"].ToString(), GM.Npcdata[i]["image"].ToString());

            NPCSelect.transform.SetParent(Content.transform);

            ReScale(NPCSelect, 1f);
        }     

    }

    public void ReScale(GameObject obj, float scale)
    {
        //obj.transform.localScale = new Vector3(scale, scale, 1);

        //obj.GetComponent<RectTransform>().sizeDelta = new Vector2(scale, scale);
        
        obj.GetComponent<RectTransform>().localScale = new Vector2(scale, scale);
    }

    public void HideCard()
    {
        var cardname = "Back";

        for (int i = 0; i < CardImage.Length; i++)
        {
            if (string.Compare(CardImage[i].name, cardname, true) == 0)
            {
                DealerCardUI[1].sprite = CardImage[i];

                break;
            }
        }         
        
        DealerCardUI[1].gameObject.SetActive(true);
        
        GM.cardset = true;

        if (GM.playerblackjack)
        {
            GameManager.Instance.StandButton();
        }      

    }

    public void StageCheck()
    {
        var havemoney = PlayerPrefs.GetFloat("Money");

        GameObject Content = GameObject.Find("Canvas").transform.Find("NPC_Scroll_View").Find("Viewport").Find("Content").gameObject;

        for (int i = 0; i < Content.transform.childCount; i++)
        {
            int min = int.Parse(DataManager.GetData(GameManager.Instance.Npcdata, i + 1, "minbet"));

            if (havemoney < min)
            {
                Content.transform.GetChild(i).GetComponent<Button>().interactable = false;               
            }
        }     
    
    }

}

