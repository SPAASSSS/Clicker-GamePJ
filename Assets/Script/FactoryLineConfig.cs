using UnityEngine;

[CreateAssetMenu(menuName = "IdleFactory/Factory Line Config")]
public class FactoryLineConfig : ScriptableObject
{
    public OreType oreType;

    [Header("Visual")]
    public Sprite oreSprite;

    [Header("Economy")]
    public bool startUnlocked = false;
    public double baseValue = 1;         // เงินต่อ 1 ครั้ง (ก่อนอัปเกรด)
    public double unlockCost = 10;
    public double autoCost = 50;
    public float autoIntervalSeconds = 1.0f;

    [Header("Upgrade (Value +10% each level)")]
    public double upgradeBaseCost = 25;
    public double upgradeCostMultiplier = 1.20; // ราคาค่าอัปเกรดโตขึ้นเรื่อยๆ
}