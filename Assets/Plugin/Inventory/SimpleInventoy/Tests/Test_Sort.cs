using ClaudeFehlen.ItemSystem.Simple;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
namespace ClaudeFehlen.ItemSystem.Simple {
    public class Test_Sort : MonoBehaviour {

        [UnityTest]
        public IEnumerator SortByName() {
            var i = new Inventory(9);
            var itemDataA = new Item("TestA", 1);
            var itemDataB = new Item("TestB", 1);
            i.AddItem(itemDataA, 1);
            i.AddItem(itemDataB, 1);
            i.SortByName();
            Assert.AreEqual(itemDataA, i.GetItem(0));
            Assert.AreEqual(itemDataB, i.GetItem(1));
            yield return null;
        }
        [UnityTest]
        public IEnumerator SortByNameDescending() {
            var i = new Inventory(9);
            var itemDataA = new Item("TestA", 1);
            var itemDataB = new Item("TestB", 1);
            i.AddItem(itemDataA, 1);
            i.AddItem(itemDataB, 1);
            i.SortByNameDescending();
            Assert.AreEqual(itemDataB, i.GetItem(0));
            Assert.AreEqual(itemDataA, i.GetItem(1));
            yield return null;
        }
        [UnityTest]
        public IEnumerator SortByType() {
            var i = new Inventory(9);
            var itemDataA = new Item("A", ItemType.armor, 1);
            var itemDataB = new Item("B", ItemType.collectable, 1);
            i.AddItem(itemDataA, 1);
            i.AddItem(itemDataB, 1);
            i.SortByType();
            Assert.AreEqual(itemDataA.Type, i.GetItem(0).Type);
            Assert.AreEqual(itemDataB.Type, i.GetItem(1).Type);
            yield return null;
        }
        [UnityTest]
        public IEnumerator SortByTypeDescending() {
            var i = new Inventory(9);
            var itemDataA = new Item("A", ItemType.armor, 1);
            var itemDataB = new Item("B", ItemType.collectable, 1);
            i.AddItem(itemDataA, 1);
            i.AddItem(itemDataB, 1);
            i.SortByTypeDescending();

            Assert.AreEqual(itemDataB.Type, i.GetItem(0).Type);
            Assert.AreEqual(itemDataA.Type, i.GetItem(1).Type);



            yield return null;
        }
    }
}