using ClaudeFehlen.ItemSystem.Simple;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Shop : InventoryShop {
    public Dummy_Shop(List<ItemShop> itemShops) : base(itemShops) {
    }

    public override int GetSellValueForItem(Item item) {
        return 1;
    }
}
