using ClaudeFehlen.ItemSystem.Simple;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
namespace ClaudeFehlen.ItemSystem.Simple {
    public class Test_Filter : MonoBehaviour {
        [UnityTest]
        public IEnumerator FilterGetAll() {
            var i = new Dummy_Inventory(9);
            var itemDataA = new Item("A", ItemType.armor, 1);
            var itemDataB = new Item("B", ItemType.collectable, 1);
            i.AddItem(itemDataA, 1);
            i.AddItem(itemDataB, 1);
            var list = i.GetAllItems();
            Assert.AreEqual(itemDataA, list[0]);
            Assert.AreEqual(itemDataB, list[1]);
            yield return null;
        }
        [UnityTest]
        public IEnumerator FilterGetOfType() {
            var i = new Dummy_Inventory(9);
            var itemDataA = new Item("A", ItemType.armor, 1);
            var itemDataB = new Item("B", ItemType.collectable, 1);
            i.AddItem(itemDataA, 1);
            i.AddItem(itemDataB, 1);
            var list = i.GetAllItems(ItemType.armor);
            Assert.AreEqual(itemDataA, list[0]);
            Assert.AreEqual(list.Count, 1);
            yield return null;
        }
        [UnityTest]
        public IEnumerator FilterGetOfMultipleTypes() {
            var i = new Dummy_Inventory(9);
            var itemDataA = new Item("A", ItemType.armor, 1);
            i.AddItem(itemDataA, 1);
            i.AddItem(new Item("B", ItemType.collectable, 1), 1);
            i.AddItem(new Item("E", ItemType.equipement, 1), 1);
            i.AddItem(new Item("AS", ItemType.equipement, 1), 1);
            var list = i.GetAllItems(new List<ItemType>() { ItemType.armor, ItemType.collectable });
            Assert.AreEqual(itemDataA, list[0]);
            Assert.AreEqual(2, list.Count);
            yield return null;
        }
        [UnityTest]
        public IEnumerator FilterGetItemsThatContainString() {
            var i = new Dummy_Inventory(9);
            i.AddItem(new Item("Test", ItemType.armor, 1), 1);
            i.AddItem(new Item("Test3", ItemType.collectable, 1), 1);
            i.AddItem(new Item("Bla2", ItemType.equipement, 1), 1);
            i.AddItem(new Item("Bla1", ItemType.equipement, 1), 1);
            var list = i.GetItemsThatContainInName("Bla");
            Assert.AreEqual(2, list.Count);
            list = i.GetItemsThatContainInName("bla");
            Assert.AreEqual(2, list.Count);
            yield return null;
        }
    }
}