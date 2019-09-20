using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformEx {
    /// <summary>
    /// Hierarchyのパスを取得
    /// </summary>
    /// <returns></returns>
    public static string GetPath (this Transform self, bool sceneName = true) {
        string path = self.gameObject.name;
        Transform parent = self.parent;
        while (parent != null) {
            path = parent.name + "/" + path;
            parent = parent.parent;
        }
        return (sceneName) ? self.gameObject.scene.name + "/" + path + "/" : path + "/";
    }
}