using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldDisplay : MonoBehaviour {
    private InputField field { get { return GameObject.FindObjectOfType<InputField> (); } }
    private Text text;

    private SaveTarget displayText;

    void Start () {
        text = GetComponentInChildren<Text> ();
        displayText = new SaveTarget (this, "displayText", string.Empty);
        text.text = displayText.Value;
    }

    public void UpdateText () {
        //テキストにinputFieldの内容を反映
        text.text = field.text;
        displayText.Value = field.text;

    }
}