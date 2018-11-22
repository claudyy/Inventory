using System;
using System.Collections;
using System.Collections.Generic;
using ClaudeFehlen.ItemSystem.Simple;
using UnityEngine;
using UnityEngine.UI;
public class UI_ItemInfoPopup : MonoBehaviour {

    public Text titel;
    public Text description;
    // Use this for initialization
    void Start() {
        gameObject.SetActive(false);

    }


    internal void ShowDescription(string title,string desc,UI_BaseSlot slot) {
        Show(true);
        titel.text = title;
        description.text = desc;
    }

    public void Show(bool v) {
        gameObject.SetActive(v);
    }
}
