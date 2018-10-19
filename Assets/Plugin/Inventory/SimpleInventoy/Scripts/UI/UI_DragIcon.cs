using System;
using System.Collections;
using System.Collections.Generic;
using ClaudeFehlen.ItemSystem.Simple;
using UnityEngine;
using UnityEngine.UI;

public class UI_DragIcon : MonoBehaviour {
    public Image display;
	// Use this for initialization
	void Start () {
        Show(false);
    }

    // Update is called once per frame
    void Update () {
        var mousePos = Input.mousePosition;
        GetComponent<RectTransform>().SetPositionAndRotation(mousePos, Quaternion.identity);
	}

    public void SetItem(Item item) {
        display.sprite = item.GetUIDisplay();
        Show(true);
    }
    public void Show(bool s) {
        gameObject.SetActive(s);
    }
}
