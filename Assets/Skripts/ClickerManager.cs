using UnityEngine;
using UnityEngine.UI;
using YG;

public class ClickerManager : MonoBehaviour
{
    [SerializeField] GameObject _Achive01Close;
    [SerializeField] GameObject _Achive01Open;
    [SerializeField] GameObject _Achive02Close;
    [SerializeField] GameObject _Achive02Open;
    [SerializeField] GameObject _Achive03Close;
    [SerializeField] GameObject _Achive03Open;
    [SerializeField] GameObject _Achive04Close;
    [SerializeField] GameObject _Achive04Open;
    [SerializeField] GameObject _Achive05Close;
    [SerializeField] GameObject _Achive05Open;
    [SerializeField] private Button clickButton;
    [SerializeField] private Text textMoney;
    [SerializeField] private Text clickCountText;
    private int currentMoney;
    private int amountOfMoneyPerClick = 1;

    public int GetCurrentMoney => currentMoney;
    private void Start()
    {

        amountOfMoneyPerClick = PlayerPrefs.GetInt("Click", 1);
        currentMoney = PlayerPrefs.GetInt("Money", 0);
        clickButton.onClick.AddListener(Click);
        UpdateText();
        UnlockingAchievements();
        YandexGame.NewLeaderboardScores("LiderBordClicker", currentMoney);
    }

    private void UpdateText()
    {
        textMoney.text = currentMoney.ToString();
        clickCountText.text = amountOfMoneyPerClick.ToString();
    }
    private void Click()
    {
        AddMoney(amountOfMoneyPerClick);
    }

    public void AddClick(int bonusClick)
    {
        amountOfMoneyPerClick += bonusClick;
        PlayerPrefs.SetInt("Click", amountOfMoneyPerClick);
        UpdateText();
    }
    public void AddMoney(int count)
    {
        currentMoney += count;
        PlayerPrefs.SetInt("Money", currentMoney);
        UpdateText();
        MySave();
        UnlockingAchievements();
    }
    private void Update()
    {
        if (currentMoney == 100 || currentMoney == 1000 || currentMoney == 5000 || currentMoney == 15000 || currentMoney == 50000)
        {
            UnlockingAchievements();
        }
    }
    private void UnlockingAchievements()
    {
        if (currentMoney >= 100)
        {
            _Achive01Close.SetActive(false);
            _Achive01Open.SetActive(true);
        }
        if (currentMoney >= 1000)
        {
            _Achive02Close.SetActive(false);
            _Achive02Open.SetActive(true);
        }
        if (currentMoney >= 5000)
        {
            _Achive03Close.SetActive(false);
            _Achive03Open.SetActive(true);
        }
        if (currentMoney >= 15000)
        {
            _Achive04Close.SetActive(false);
            _Achive04Open.SetActive(true);
        }
        if (currentMoney >= 50000)
        {
            _Achive05Close.SetActive(false);
            _Achive05Open.SetActive(true);
        }
    }
    public void ExidGame()
    {
        Application.Quit();
    }
    public void MySave()
    {
        YandexGame.savesData.money = currentMoney;
        YandexGame.SaveProgress();
    }
    public void ExampleOpenRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
        //YandexGame.GetDataEvent += GetData;
    }
    void Rewarded(int id)
    {
        AddMoney(500);
    }
}
