using ClaudeFehlen.ItemSystem.Simple;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTest : MonoBehaviour {

	public InventoryOwner player;
	public InventoryShop shop;
	public Dummy_UI_Inventory playerUI;
	public UI_Shop shop_UI;
	// Use this for initialization
	void Start() {
		playerUI.SetInventory(player.GetInventories()[0]);
		player.GetInventories()[0].AddItem(new Item("test", 1), 5);
		player.GetInventories()[0].AddItem(new Item("test1", 1), 12);
		player.GetInventories()[0].AddItem(new Item("test2", 1), 12);
		var shopItems = new List<ItemShop>() { new ItemShop(new Item("test", 1), 10), new ItemShop(new Item("test1", 1), 10) , new ItemShop(new Item("test2", 1), 10) };
		shop = new Dummy_Shop(shopItems);
		shop_UI.SetShop(shop,player);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
