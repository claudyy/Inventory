using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace ClaudeFehlen.ItemSystem.Simple {
    public class Test_Crafting : MonoBehaviour {

        [UnityTest]
        public IEnumerator CraftItem() {
            var inv = new Dummy_Inventory(2);
            var item = new Item("Test",4);
            inv.AddItem(item, 4);
            inv.AddItem(item, 4);
            var owner = new Test_InventoryOwner(inv,10);
            var craftResult = new Item("TestC", 1);
            var itemCrafting = new ItemCrafting(new List<Item>() { craftResult }, new List<CraftingRecipe>() { new CraftingRecipe(new List<Item>(){ item },new List<int>() { 8 })});
            var crafting = new Crafting(new List<ItemCrafting>() { itemCrafting });
            crafting.Craft(owner, 0);

            Assert.AreEqual(craftResult, inv.GetItem(0));
            Assert.AreEqual(1, inv.GetItem(0).count);
            Assert.AreEqual(Item.Empty, inv.GetItem(1));

            yield return null;
        }
        [UnityTest]
        public IEnumerator CraftItem_ButNotReqeuredResources() {
            var inv = new Dummy_Inventory(2);
            var item = new Item("Test", 4);
            inv.AddItem(item, 4);
            inv.AddItem(item, 2);
            var owner = new Test_InventoryOwner(inv, 10);
            var craftResult = new Item("TestC", 1);
            var itemCrafting = new ItemCrafting(new List<Item>() { craftResult }, new List<CraftingRecipe>() { new CraftingRecipe(new List<Item>() { item }, new List<int>() { 8 }) });
            var crafting = new Crafting(new List<ItemCrafting>() { itemCrafting });
            crafting.Craft(owner, 0);

            Assert.AreEqual(item, inv.GetItem(0));
            Assert.AreEqual(4, inv.GetItem(0).count);
            Assert.AreEqual(item, inv.GetItem(1));
            Assert.AreEqual(2, inv.GetItem(1).count);

            yield return null;
        }
        [UnityTest]
        public IEnumerator CraftItem_UseSecondRecipe() {
            var inv = new Dummy_Inventory(2);
            var item = new Item("Test", 4);
            var item2 = new Item("Test2", 4);
            inv.AddItem(item2, 4);
            inv.AddItem(item, 4);
            var owner = new Test_InventoryOwner(inv, 10);
            var craftResult = new Item("TestC", 1);
            var itemCrafting = new ItemCrafting(new List<Item>() { craftResult }, new List<CraftingRecipe>() { new CraftingRecipe(new List<Item>() { item }, new List<int>() { 4 }), new CraftingRecipe(new List<Item>() { item2 }, new List<int>() { 4 }) });
            var crafting = new Crafting(new List<ItemCrafting>() { itemCrafting });
            crafting.Craft(owner, 0,1);

            Assert.AreEqual(craftResult, inv.GetItem(0));
            Assert.AreEqual(1, inv.GetItem(0).count);
            Assert.AreEqual(item, inv.GetItem(1));
            Assert.AreEqual(4, inv.GetItem(1).count);

            yield return null;
        }
        [UnityTest]
        public IEnumerator CanCraftItem() {
            var inv = new Dummy_Inventory(2);
            var item = new Item("Test", 4);
            var item2 = new Item("Test2", 4);
            inv.AddItem(item2, 4);
            inv.AddItem(item, 4);
            var owner = new Test_InventoryOwner(inv, 10);
            var craftResult = new Item("TestC", 1);
            var itemCrafting = new ItemCrafting(new List<Item>() { craftResult }, new List<CraftingRecipe>() { new CraftingRecipe(new List<Item>() { item }, new List<int>() { 4 }), new CraftingRecipe(new List<Item>() { item2 }, new List<int>() { 4 }) });
            var crafting = new Crafting(new List<ItemCrafting>() { itemCrafting });

            Assert.AreEqual(true, crafting.CanCraft(owner, 0));

            yield return null;
        }
        [UnityTest]
        public IEnumerator CanNotCraftItem() {
            var inv = new Dummy_Inventory(2);
            var item = new Item("Test", 4);
            var item2 = new Item("Test2", 4);
            inv.AddItem(item2, 1);
            inv.AddItem(item, 1);
            var owner = new Test_InventoryOwner(inv, 10);
            var craftResult = new Item("TestC", 1);
            var itemCrafting = new ItemCrafting(new List<Item>() { craftResult }, new List<CraftingRecipe>() { new CraftingRecipe(new List<Item>() { item }, new List<int>() { 4 }), new CraftingRecipe(new List<Item>() { item2 }, new List<int>() { 4 }) });
            var crafting = new Crafting(new List<ItemCrafting>() { itemCrafting });

            Assert.AreEqual(false, crafting.CanCraft(owner, 0));

            yield return null;
        }
    }
}