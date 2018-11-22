using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace ClaudeFehlen.ItemSystem.Simple {
    public class Test_Shop {
        [UnityTest]
        public IEnumerator BuyItem() {
            var inv = new Dummy_Inventory(9);
            var owner = new Test_InventoryOwner(new List<Inventory>() { inv }, 10);
            var item = new Item("Test", 1);
            var shopItem = new ItemShop(item, 10);
            var shop = new Dummy_Shop(new List<ItemShop>() { shopItem});
            shop.Buy(0, owner);
            Assert.AreEqual(item,owner.GetInventories()[0].GetItem(0));
            Assert.AreEqual(0,owner.GetCurrentCurency());

            yield return null;
        }
        [UnityTest]
        public IEnumerator BuyItem_ButNotEnoughMoney() {
            var inv = new Dummy_Inventory(9);
            var owner = new Test_InventoryOwner(new List<Inventory>() { inv }, 9);
            var item = new Item("Test", 1);
            var shopItem = new ItemShop(item, 10);
            var shop = new Dummy_Shop(new List<ItemShop>() { shopItem });
            shop.Buy(0, owner);
            Assert.AreEqual(Item.Empty, owner.GetInventories()[0].GetItem(0));
            Assert.AreEqual(9, owner.GetCurrentCurency());
            yield return null;
        }
        [UnityTest]
        public IEnumerator BuyItem_ButNotEnoughSpace() {
            var inv = new Dummy_Inventory(2);
            var itemA = new Item("Test", 1);
            inv.AddItem(itemA, 4);
            inv.AddItem(itemA, 4);
            var owner = new Test_InventoryOwner(new List<Inventory>() { inv }, 10);
            var item = new Item("Test1", 1);
            var shopItem = new ItemShop(item, 10);
            var shop = new Dummy_Shop(new List<ItemShop>() { shopItem });
            shop.Buy(0, owner);

            Assert.AreEqual(itemA, owner.GetInventories()[0].GetItem(0));
            Assert.AreEqual(itemA, owner.GetInventories()[0].GetItem(1));
            Assert.AreEqual(10, owner.GetCurrentCurency());

            yield return null;
        }
        [UnityTest]
        public IEnumerator BuyItem_ThatInvnentoryAlreadyHas() {
            var inv = new Dummy_Inventory(2);
            var itemA = new Item("Test", 3);
            inv.AddItem(itemA, 3);
            var owner = new Test_InventoryOwner(new List<Inventory>() { inv }, 10);
            var item = new Item("Test", 1);
            var shopItem = new ItemShop(item, 10);
            var shop = new Dummy_Shop(new List<ItemShop>() { shopItem });
            shop.Buy(0, owner);

            Assert.AreEqual(itemA, owner.GetInventories()[0].GetItem(0));
            Assert.AreEqual(Item.Empty, owner.GetInventories()[0].GetItem(1));
            Assert.AreEqual(0, owner.GetCurrentCurency());

            yield return null;
        }
        [UnityTest]
        public IEnumerator BuyItem_ThatInvnentoryAlreadyHas_AndMoreAsStack() {
            var inv = new Dummy_Inventory(2);
            var itemA = new Item("Test", 3);
            inv.AddItem(itemA, 3);
            var owner = new Test_InventoryOwner(new List<Inventory>() { inv }, 10);
            var item = new Item("Test", 2);
            var shopItem = new ItemShop(item, 10);
            var shop = new Dummy_Shop(new List<ItemShop>() { shopItem });
            shop.Buy(0, owner);

            Assert.AreEqual(itemA, owner.GetInventories()[0].GetItem(0));
            Assert.AreEqual(4, owner.GetInventories()[0].GetItem(0).count);
            Assert.AreEqual(itemA, owner.GetInventories()[0].GetItem(1));
            Assert.AreEqual(1, owner.GetInventories()[0].GetItem(1).count);

            Assert.AreEqual(0, owner.GetCurrentCurency());

            yield return null;
        }
        [UnityTest]
        public IEnumerator BuyItem_ButInventoryAlreadyFull_AndPutInSecondeInventory() {
            var inv = new Dummy_Inventory(2);
            var inv2 = new Dummy_Inventory(2);
            var itemA = new Item("TestA", 3);
            inv.AddItem(itemA, 4);
            inv.AddItem(itemA, 4);
            var owner = new Test_InventoryOwner(new List<Inventory>() { inv,inv2 }, 10);
            var item = new Item("Test", 1);
            var shopItem = new ItemShop(item, 10);
            var shop = new Dummy_Shop(new List<ItemShop>() { shopItem });
            shop.Buy(0, owner);

            Assert.AreEqual(itemA, owner.GetInventories()[0].GetItem(0));
            Assert.AreEqual(itemA, owner.GetInventories()[0].GetItem(1));
            Assert.AreEqual(item, owner.GetInventories()[1].GetItem(0));
            Assert.AreEqual(1, owner.GetInventories()[1].GetItem(0).count);
            Assert.AreEqual(Item.Empty, owner.GetInventories()[1].GetItem(1));

            Assert.AreEqual(0, owner.GetCurrentCurency());

            yield return null;
        }
        [UnityTest]
        public IEnumerator SellItem() {
            var inv = new Dummy_Inventory(9);
            var item = new Item("Test", 1);
            inv.AddItem(item, 1);
            var owner = new Test_InventoryOwner(new List<Inventory>() { inv }, 10);
            var shopItem = new ItemShop(item, 10);
            var shop = new Dummy_Shop(new List<ItemShop>() { shopItem });

            shop.Sell(inv,0,owner);

            Assert.AreEqual(Item.Empty, owner.GetInventories()[0].GetItem(0));
            Assert.AreEqual(11, owner.GetCurrentCurency());

            yield return null;
        }
        [UnityTest]
        public IEnumerator SellItemCountOfTWo() {
            var inv = new Dummy_Inventory(9);
            var item = new Item("Test", 1);
            inv.AddItem(item, 2);
            var owner = new Test_InventoryOwner(new List<Inventory>() { inv }, 10);
            var shopItem = new ItemShop(item, 10);
            var shop = new Dummy_Shop(new List<ItemShop>() { shopItem });

            shop.Sell(inv, 0, owner);

            Assert.AreEqual(Item.Empty, owner.GetInventories()[0].GetItem(0));
            Assert.AreEqual(12, owner.GetCurrentCurency());

            yield return null;
        }
        [UnityTest]
        public IEnumerator SellAllItem() {
            var inv = new Dummy_Inventory(9);
            var inv2 = new Dummy_Inventory(9);
            var item = new Item("Test", 1);
            inv.AddItem(item, 2);
            inv.AddItem(item, 2);
            inv2.AddItem(item, 2);
            var owner = new Test_InventoryOwner(new List<Inventory>() { inv, inv2 }, 10);
            var shopItem = new ItemShop(item, 10);
            var shop = new Dummy_Shop(new List<ItemShop>() { shopItem });

            shop.SellAll(owner,inv);

            Assert.AreEqual(Item.Empty, owner.GetInventories()[0].GetItem(0));
            Assert.AreEqual(Item.Empty, owner.GetInventories()[0].GetItem(1));
            Assert.AreEqual(item, owner.GetInventories()[1].GetItem(0));
            Assert.AreEqual(14, owner.GetCurrentCurency());

            yield return null;
        }
        [UnityTest]
        public IEnumerator SellAllItemOfAllInventories() {
            var inv = new Dummy_Inventory(9);
            var inv2 = new Dummy_Inventory(9);
            var item = new Item("Test", 1);
            inv.AddItem(item, 2);
            inv.AddItem(item, 2);
            inv2.AddItem(item, 2);
            var owner = new Test_InventoryOwner(new List<Inventory>() { inv, inv2 }, 10);
            var shopItem = new ItemShop(item, 10);
            var shop = new Dummy_Shop(new List<ItemShop>() { shopItem });

            shop.SellAll(owner);

            Assert.AreEqual(Item.Empty, owner.GetInventories()[0].GetItem(0));
            Assert.AreEqual(Item.Empty, owner.GetInventories()[0].GetItem(1));
            Assert.AreEqual(Item.Empty, owner.GetInventories()[1].GetItem(0));
            Assert.AreEqual(16, owner.GetCurrentCurency());

            yield return null;
        }
    }
}