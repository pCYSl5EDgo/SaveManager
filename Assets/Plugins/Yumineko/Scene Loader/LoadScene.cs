using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour {
    public float FadeOutTime { get; set; }
    public float FadeInTime { get; set; }
    public void FadeLoad (string sceneName) {
        if (FadeOutTime == 0.0f) FadeOutTime = 0.5f;
        if (FadeInTime == 0.0f) FadeInTime = 0.5f;
        SceneLoadManager.Instance.FadeLoad (sceneName, FadeOutTime, FadeInTime);
    }

    public void Load (string sceneName) {
        SceneLoadManager.Instance.Load (sceneName);
    }
}