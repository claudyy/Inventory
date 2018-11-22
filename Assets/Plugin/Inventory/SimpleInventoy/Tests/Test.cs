using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
namespace ClaudeFehlen.ItemSystem.Simple {
    public class Test {

        [UnityTest]
        public IEnumerator AddItem() {
            var i = new Dummy_Inventory(9);
            var itemData = new Item("Test1", 1);
            i.AddItem(itemData,1);
            Assert.AreEqual(itemData,i.GetItem(0));
            Assert.AreEqual(1, i.GetItem(0).count);
            yield return null;
        }
        [UnityTest]
        public IEnumerator AddItemTwice() {
            var i = new Dummy_Inventory(9);
            var itemData = new Item("Test1", 1);
            i.AddItem(itemData, 1);
            i.AddItem(itemData, 1);
            Assert.AreEqual(i.GetItem(0), itemData);
            Assert.AreEqual(i.GetItem(0).count, 2);
            yield return null;
        }
        [UnityTest]
        public IEnumerator AddItemFillStack() {
            var i = new Dummy_Inventory(9);
            var itemData = new Item("Test1", 1);
            i.AddItem(itemData, 4);
            Assert.AreEqual(i.GetItem(0), itemData);
            Assert.AreEqual(i.GetItem(0).StackFull, true);
            Assert.AreEqual(i.GetItem(1), Item.Empty);

            yield return null;
        }
        [UnityTest]
        public IEnumerator AddItemFillStackTwice() {
            var i = new Dummy_Inventory(9);
            var itemData = new Item("Test1", 1);
            i.AddItem(itemData, 4);
            i.AddItem(itemData, 4);
            Assert.AreEqual(i.GetItem(0), itemData);
            Assert.AreEqual(i.GetItem(0).StackFull, true);
            Assert.AreEqual(i.GetItem(1), itemData);
            Assert.AreEqual(i.GetItem(1).StackFull, true);
            Assert.AreEqual(i.GetItem(2), Item.Empty);

            yield return null;
        }

        [UnityTest]
        public IEnumerator AddItemRemoveItem() {
            var i = new Dummy_Inventory(9);
            var itemData = new Item("Test1", 1);

            i.AddItem(itemData, 4);
            Assert.AreEqual(i.RemoveItem(itemData, 4), true);

            Assert.AreEqual(i.GetItem(0), Item.Empty);

            yield return null;
        }
        [UnityTest]
        public IEnumerator AddItemRemoveMoreThanPossible() {
            var i = new Dummy_Inventory(9);
            var itemData = new Item("Test1", 1);

            i.AddItem(itemData, 4);
            Assert.AreEqual(i.RemoveItem(itemData, 6), false);
            Assert.AreEqual(i.GetItem(0).count, 4);

            yield return null;
        }
        [UnityTest]
        public IEnumerator AddItemCheckCount() {
            var i = new Dummy_Inventory(9);
            var itemData = new Item("Test1", 1);

            i.AddItem(itemData, 4);
            Assert.AreEqual(i.CountOfItem(itemData) == 4, true);
            Assert.AreEqual(i.CountOfItem(itemData) == 6, false);
            i.AddItem(itemData, 6);
            Assert.AreEqual(i.CountOfItem(itemData) == 10, true);
            Assert.AreEqual(i.CountOfItem(itemData) == 12, false);
            yield return null;
        }
        [UnityTest]
        public IEnumerator IsFull() {
            var i = new Dummy_Inventory(9);
            var itemData = new Item("Test1", 1);

            i.AddItem(itemData, 4*9);
            Assert.AreEqual(i.IsFull(), true);
            yield return null;
        }
        [UnityTest]
        public IEnumerator IsNotFull() {
            var i = new Dummy_Inventory(9);
            var itemData = new Item("Test1", 1);

            i.AddItem(itemData, 4 * 8);
            Assert.AreEqual(i.IsFull(), false);
            yield return null;
        }

    }
}