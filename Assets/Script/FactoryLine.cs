using System;
using UnityEngine;

public class FactoryLine : MonoBehaviour
{
    public FactoryLineConfig config;

    [Header("Scene Refs")]
    public GameObject conveyorObject;
    public GameObject machineObject;
    public Transform spawnPoint;
    public OreItem orePrefab;

    [Header("Runtime State")]
    public bool unlocked;
    public bool autoPurchased;
    public int upgradeLevel;

    private float _autoTimer;

    public event Action OnStateChanged;

    private void Start()
    {
        ApplyVisualState();
    }

    private void Update()
    {
        if (!unlocked || !autoPurchased) return;

        _autoTimer += Time.deltaTime;
        if (_autoTimer >= config.autoIntervalSeconds)
        {
            _autoTimer = 0f;
            SpawnOreVisual();
            GameManager.Instance.AddMoney(GetCurrentValue());
        }
    }

    public void InitNewGameState()
    {
        unlocked = config.startUnlocked;
        autoPurchased = false;
        upgradeLevel = 0;
        ApplyVisualState();
    }

    public void ApplyLoadedState(bool u, bool a, int lvl)
    {
        unlocked = u;
        autoPurchased = a;
        upgradeLevel = lvl;
        ApplyVisualState();
    }

    public void ManualClick()
    {
        if (!unlocked) return;

        SpawnOreVisual();
        GameManager.Instance.AddMoney(GetCurrentValue());
    }

    private void SpawnOreVisual()
    {
        if (orePrefab == null || spawnPoint == null) return;

        var ore = Instantiate(orePrefab, spawnPoint.position, Quaternion.identity);
        ore.SetSprite(config.oreSprite);
    }

    public double GetCurrentValue()
    {
        // +10% value each upgrade level
        return config.baseValue * Math.Pow(1.10, upgradeLevel);
    }

    public double GetUnlockCost() => config.unlockCost;

    public double GetAutoCost() => config.autoCost;

    public double GetUpgradeCost()
    {
        return config.upgradeBaseCost * Math.Pow(config.upgradeCostMultiplier, upgradeLevel);
    }

    public void TryUnlock()
    {
        if (unlocked) return;
        if (GameManager.Instance.TrySpendMoney(GetUnlockCost()))
        {
            unlocked = true;
            ApplyVisualState();
            OnStateChanged?.Invoke();
        }
    }

    public void TryBuyAuto()
    {
        if (!unlocked || autoPurchased) return;
        if (GameManager.Instance.TrySpendMoney(GetAutoCost()))
        {
            autoPurchased = true;
            ApplyVisualState();
            OnStateChanged?.Invoke();
        }
    }

    public void TryUpgradeValue()
    {
        if (!unlocked) return;
        if (GameManager.Instance.TrySpendMoney(GetUpgradeCost()))
        {
            upgradeLevel++;
            OnStateChanged?.Invoke();
        }
    }

    private void ApplyVisualState()
    {
        if (conveyorObject != null) conveyorObject.SetActive(unlocked);
        if (machineObject != null) machineObject.SetActive(autoPurchased);
    }

    private void Awake()
    {
        var sign = GetComponentInChildren<ClickableSign>(true);
        if (sign != null) sign.SetLine(this);
    }
}