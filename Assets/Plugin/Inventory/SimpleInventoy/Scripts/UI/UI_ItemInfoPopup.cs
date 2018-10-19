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


    internal void ShowDescription(Item item) {
        Show(true);
        titel.text = item.name;
        description.text = item.GetDescription();
    }

    public void Show(bool v) {
        gameObject.SetActive(v);
    }
}
