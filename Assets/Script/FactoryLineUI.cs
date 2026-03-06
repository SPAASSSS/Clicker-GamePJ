using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactoryLineUI : MonoBehaviour
{
    public FactoryLine line;

    [Header("Buttons")]
    public Button unlockButton;
    public Button autoButton;
    public Button upgradeButton;

    [Header("Texts")]
    public TMP_Text unlockPriceText;
    public TMP_Text autoPriceText;
    public TMP_Text upgradePriceText;
    public TMP_Text infoText;

    private void Start()
    {
        Refresh();

        if (line != null)
            line.OnStateChanged += Refresh;

        GameManager.Instance.OnMoneyChanged += Refresh;
    }

    public void OnClickUnlock() { line.TryUnlock(); Refresh(); }
    public void OnClickAuto() { line.TryBuyAuto(); Refresh(); }
    public void OnClickUpgrade() { line.TryUpgradeValue(); Refresh(); }

    public void Refresh()
    {
        if (line == null) return;

        bool unlocked = line.unlocked;

        if (unlockButton != null) unlockButton.gameObject.SetActive(!unlocked);
        if (autoButton != null) autoButton.gameObject.SetActive(unlocked && !line.autoPurchased);

        if (upgradeButton != null) upgradeButton.gameObject.SetActive(unlocked);

        if (unlockPriceText != null) unlockPriceText.text = $"Buy: {GameManager.FormatMoney(line.GetUnlockCost())}";
        if (autoPriceText != null) autoPriceText.text = $"Auto: {GameManager.FormatMoney(line.GetAutoCost())}";
        if (upgradePriceText != null) upgradePriceText.text = $"Upgrade: {GameManager.FormatMoney(line.GetUpgradeCost())}";

        if (infoText != null)
        {
            infoText.text =
                $"$: {GameManager.FormatMoney(line.GetCurrentValue())}\n" +
                $"Lv: {line.upgradeLevel}\n" +
                $"Auto: {(line.autoPurchased ? "ON" : "")}";
        }

        // interactable checks
        double money = GameManager.Instance.Money;
        if (unlockButton != null) unlockButton.interactable = money >= line.GetUnlockCost();
        if (autoButton != null) autoButton.interactable = money >= line.GetAutoCost();
        if (upgradeButton != null) upgradeButton.interactable = money >= line.GetUpgradeCost();
    }
}