using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveManager {
    public static int SlotID { get; set; }

    private const string saveName = "SaveData";
    public static string SaveFileName { get { return saveName + SlotID.ToString () + ".es3"; } }

    private static Dictionary<string, dynamic> TempData = new Dictionary<string, dynamic> ();

    private static ES3Settings _saveset = null;
    public static ES3Settings SaveSettings {
        get {
            if (_saveset == null) {
                _saveset = new ES3Settings ();
                _saveset.location = ES3.Location.File;
                _saveset.path = SaveFileName;
                _saveset.encryptionPassword = "MomiG - Save";
                _saveset.encryptionType = ES3.EncryptionType.AES;
            }
            return _saveset;
        }
    }

    /// <summary>
    /// 指定スロットへセーブ
    /// </summary>
    public static void Save () {
        Save (SlotID);
    }

    /// <summary>
    /// 指定スロットへセーブ（スロットを切り替える）
    /// </summary>
    /// <param name="slotID">スロット番号</param>
    public static void Save (int slotID) {
        if (TempData.Count () == 0) return;
        SlotID = slotID;
        CreateSaveData ();
    }

    /// <summary>
    /// 指定スロットをロード
    /// </summary>
    public static void Load () {
        Load (SlotID);
    }

    /// <summary>
    /// 指定スロットをロード（切り替える）
    /// </summary>
    /// <param name="slotID">スロット番号</param>
    public static void Load (int slotID) {
        SlotID = slotID;
        if (ES3.FileExists (SaveSettings) == false) return;
        CreateTempData ();
    }

    /// <summary>
    /// 暗号化を介して、TempDataからSaveDataを作成。
    /// </summary>
    static void CreateSaveData () {
        foreach (var pair in TempData) {
            ES3.Save<dynamic> (pair.Key, pair.Value, SaveSettings);
        }
    }

    /// <summary>
    /// 復号化を介して、SaveDataからTempDataを作成。
    /// </summary>
    static void CreateTempData () {
        var keys = ES3.GetKeys (SaveSettings);
        TempData = keys.ToDictionary (key => key, key => ES3.Load<dynamic> (key, SaveSettings));
    }

    public static T TempLoad<T> (this MonoBehaviour self, string key) {
        var loadkey = self.transform.GetPath () + key;
        if (TempData.ContainsKey (loadkey)) {
            return TempData[loadkey];
        }
        return default (T);
    }

    public static void TempSave (this MonoBehaviour self, string key, dynamic value) {
        var saveKey = self.transform.GetPath () + key;
        TempSave (saveKey, value);
    }

    public static void TempSave (string key, dynamic value) {
        if (TempData.ContainsKey (key)) {
            TempData[key] = value;
        } else {
            TempData.Add (key, value);
        }
    }
}