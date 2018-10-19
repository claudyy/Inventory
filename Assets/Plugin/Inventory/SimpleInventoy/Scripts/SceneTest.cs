using ClaudeFehlen.ItemSystem.Simple;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTest : MonoBehaviour {

	public InventoryOwner player;
	public UI_Inventory playerUI;
	// Use this for initialization
	void Start () {
		playerUI.SetInventory(player.GetInventories()[0]);
        player.GetInventories()[0].AddItem(new Item("test", 1),12);
        player.GetInventories()[0].AddItem(new Item("test1", 1),12);
        player.GetInventories()[0].AddItem(new Item("test2", 1),12);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
