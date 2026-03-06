using System;
using System.Collections.Generic;

[Serializable]
public class LineSaveData
{
    public string oreType; // store as string
    public bool unlocked;
    public bool autoPurchased;
    public int upgradeLevel;
}

[Serializable]
public class SaveData
{
    public double money;
    public long lastSaveUnix; // optional
    public List<LineSaveData> lines = new List<LineSaveData>();
}