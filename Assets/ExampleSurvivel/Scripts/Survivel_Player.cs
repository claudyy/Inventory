using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivel_Player : MonoBehaviour {
    public UI_Inventory_Survivel Inventory_Survivel;
    Inventory_Survivel playerInventory;
    public List<ItemData_Survivel> startItems;
	// Use this for initialization
	void Start () {
        playerInventory = new Inventory_Survivel(12);
        for (int i = 0; i < startItems.Count; i++)
        {
            playerInventory.AddItem(new Item_Survivel(startItems[i], 12));
        }
        Inventory_Survivel.SetInventory(playerInventory);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
