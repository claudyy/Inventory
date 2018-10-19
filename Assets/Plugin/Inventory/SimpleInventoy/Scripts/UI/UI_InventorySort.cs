using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ClaudeFehlen.ItemSystem.Simple {

public class UI_InventorySort : MonoBehaviour {
	public List<UI_GenericButton> buttons;
    public Action<int> onClick;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < buttons.Count; i++) {
			buttons[i].onClick += Click;
		}
	}
	public void Click(UI_GenericButton b) {
            if (onClick != null)
                onClick(buttons.IndexOf(b));

    }
	public void SetInvenory(UI_Inventory inv) {
		List<string> list = inv.GetSortingList();
		for (int i = 0; i < list.Count; i++) {
				buttons[i].SetText(list[i]);
		}
	}
}
}
