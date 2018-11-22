using System;
using System.Collections;
using System.Collections.Generic;
using ClaudeFehlen.ItemSystem.Simple;
using UnityEngine;
using UnityEngine.UI;
public class UI_ItemInfo : MonoBehaviour {
	public Text titel;
	public Text description;


	internal void ShowDescription(Item item) {
		titel.text = item.name;
		description.text = item.GetDescription();
	}

    internal void ShowDescription(string title,string desc) {
        titel.text = title;
        description.text = desc;
    }
}
