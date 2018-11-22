using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClaudeFehlen.ItemSystem.Simple;
public class Test_InventoryOwner : IInventoryOwner {
    List<Inventory> Inventories;
    int money;

    public Test_InventoryOwner(Inventory inventorie, int money) {
        Inventories = new List<Inventory>() {inventorie};
        this.money = money;
    }
    public Test_InventoryOwner(List<Inventory> inventories, int money) {
        Inventories = inventories;
        this.money = money;
    }

    public void AddCurrency(int count) {
        money += count;
    }

    public void AddItem(Item item) {
        Inventories[0].AddItem(item, item.count);

    }
    public bool SpaceForItem(Item item) {

        return true;
    }
    public int GetCurrentCurency() {
        return money;
    }


    public void RemoveCurrency(int count) {
        money -= count;
    }

    public List<Inventory> GetInventories() {
        return Inventories;
    }
}
