using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManagerButton : MonoBehaviour {
    public void Save (int slot) {
        SaveManager.Save (slot);
    }

    public void Load (int slot) {
        SaveManager.Load (slot);
        SceneLoadManager.Instance.Reload ();
    }
}