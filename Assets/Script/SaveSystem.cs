using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string PathFile => System.IO.Path.Combine(Application.persistentDataPath, "save.json");

    public static void Save(SaveData data)
    {
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(PathFile, json);
    }

    public static bool TryLoad(out SaveData data)
    {
        data = null;
        if (!File.Exists(PathFile)) return false;

        var json = File.ReadAllText(PathFile);
        data = JsonUtility.FromJson<SaveData>(json);
        return data != null;
    }

    public static void DeleteSave()
    {
        if (File.Exists(PathFile))
            File.Delete(PathFile);
    }
}