using System;
using System.Collections;
using System.Collections.Generic;
using ClaudeFehlen.ItemSystem.Simple;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopItem : UI_BaseSlot {
    public Text text;
    public Image display;
    internal void UpdateView(IInventoryOwner customer,InventoryShop shop, int i) {
        text.text = shop.GetCost(i).ToString();
        display.sprite = shop.GetUIDisplay(i);
        display.color = shop.CanBuy(customer, i) ? Color.white : Color.gray;
    }
}
