using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ClaudeFehlen.ItemSystem.Simple {
    public abstract class InventoryShop {
        List<ItemShop> itemShops;

        public InventoryShop(List<ItemShop> itemShops) {
            this.itemShops = itemShops;
        }

        internal int GetCost(int i) {
            return itemShops[i].Cost;
        }

        internal Sprite GetUIDisplay(int i) {
            return null;
        }

        public int Size {
            get {
                return itemShops.Count;
            }
        }
        public void Buy(int index, IInventoryOwner owner) {
            if (index >= itemShops.Count)
                return;
            var buyItem = itemShops[index];
            if (CanBuy(owner, buyItem))
                return;
            if (OwnerHasSpace(owner, buyItem.Item) == false)
                return;
            var inventories = owner.GetInventories();
            var count = buyItem.Item.count;
            for (int i = 0; i < inventories.Count; i++) {
                var space = inventories[i].CountOfSpaceForItem(buyItem.Item);
                var add = Mathf.Min(count, space);
                inventories[i].AddItem(buyItem.Item, add);
                count -= add;
            }
            owner.RemoveCurrency(buyItem.Cost);
        }

        internal string GetItemDescription(int selectItem) {
            return itemShops[selectItem].Item.GetDescription();
        }

        internal string GetItemName(int selectItem) {
            return itemShops[selectItem].Item.name;
        }
        public bool CanBuy(IInventoryOwner owner, int index) {
            return owner.GetCurrentCurency() < itemShops[index].Cost;
        }
        public bool CanBuy(IInventoryOwner owner, ItemShop buyItem) {
            return owner.GetCurrentCurency() < buyItem.Cost;
        }

        bool OwnerHasSpace(IInventoryOwner owner, Item item) {
            var inventories = owner.GetInventories();
            var freeSpace = 0;
            for (int i = 0; i < inventories.Count; i++) {
                if (inventories[i].HaveSpaceForItem(item)) {
                    freeSpace += inventories[i].CountOfSpaceForItem(item);
                }
            }
            return freeSpace >= item.count;
        }

        public void Sell(Inventory inv, int itemIndex, IInventoryOwner owner) {
            var item = inv.GetItem(itemIndex);
            if (item == Item.Empty)
                return;
            var count = inv.GetItem(itemIndex).count;
            inv.RemoveItemAt(itemIndex);
            var gain = GetSellValueForItem(item) * count;
            owner.AddCurrency(gain);
        }
        public abstract int GetSellValueForItem(Item item);
        
        public void SellAll(IInventoryOwner owner) {
            var inventories = owner.GetInventories();
            for (int i = 0; i < inventories.Count; i++) {
                SellAll(owner, inventories[i]);
            }
        }

        public void SellAll(IInventoryOwner owner, Inventory inv) {
            for (int j = 0; j < inv.size; j++) {
                Sell(inv, j, owner);
            }
        }
    }

}
