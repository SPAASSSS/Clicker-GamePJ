using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Money")]
    [SerializeField] private double money = 0;
    public double Money => money;

    public event Action OnMoneyChanged;

    [Header("UI")]
    public TMP_Text moneyText;

    [Header("Lines")]
    public FactoryLine[] lines;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadGameOrNew();
        RefreshMoneyUI();
    }

    public void AddMoney(double amount)
    {
        money += amount;
        RefreshMoneyUI();
        OnMoneyChanged?.Invoke();
    }

    public bool TrySpendMoney(double cost)
    {
        if (money < cost) return false;
        money -= cost;
        RefreshMoneyUI();
        OnMoneyChanged?.Invoke();
        return true;
    }

    public void SaveGame()
    {
        var data = new SaveData();
        data.money = money;
        data.lastSaveUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        foreach (var l in lines)
        {
            data.lines.Add(new LineSaveData
            {
                oreType = l.config.oreType.ToString(),
                unlocked = l.unlocked,
                autoPurchased = l.autoPurchased,
                upgradeLevel = l.upgradeLevel
            });
        }

        SaveSystem.Save(data);
    }

    private void LoadGameOrNew()
    {
        if (!SaveSystem.TryLoad(out var data))
        {
            // New game
            foreach (var l in lines) l.InitNewGameState();
            money = 0;
            SaveGame();
            return;
        }

        money = data.money;

        // Apply to lines by matching oreType
        foreach (var l in lines)
        {
            var match = data.lines.FirstOrDefault(x => x.oreType == l.config.oreType.ToString());
            if (match == null)
                l.InitNewGameState();
            else
                l.ApplyLoadedState(match.unlocked, match.autoPurchased, match.upgradeLevel);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void RefreshMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = $"$: {FormatMoney(money)}";
    }

    public static string FormatMoney(double v)
    {
        // simple formatter: 1,234 / 12.3K / 4.56M / 7.89B
        if (v < 1000) return v.ToString("0");
        if (v < 1_000_000) return (v / 1000d).ToString("0.0") + "K";
        if (v < 1_000_000_000) return (v / 1_000_000d).ToString("0.00") + "M";
        return (v / 1_000_000_000d).ToString("0.00") + "B";
    }
}