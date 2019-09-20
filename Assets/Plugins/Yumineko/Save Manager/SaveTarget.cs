using UniRx;
using UnityEngine;

public class SaveTarget {
    public string Key;
    public dynamic Value;
    public SaveTarget (MonoBehaviour mono, string key, dynamic defaultValue) {
        Key = key;
        Value = SaveManager.TempLoad<dynamic> (mono, Key) ?? defaultValue;

        mono.ObserveEveryValueChanged (_ => Value)
            .Subscribe (v => {
                SaveManager.TempSave (mono.transform.GetPath () + Key, Value);
            });
    }
}