using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace ClaudeFehlen.ItemSystem.Simple {
    public class SwitchTests  {

        [UnityTest]
        public IEnumerator SwitchItem() {
            var i = new Inventory(9);
            var itemData = new Item("Test1",1);
            var itemData2 = new Item("Test2", 1);
            i.AddItem(itemData, 1);
            i.AddItem(itemData2, 1);
            i.SwitchPositions(0, 1);
            Assert.AreEqual(itemData, i.GetItem(1));
            Assert.AreEqual(itemData2, i.GetItem(0));
            yield return null;
        }
        [UnityTest]
        public IEnumerator SwitchItemFromTwoDifferentInventories() {
            var i = new Inventory(9);
            var i2 = new Inventory(9);
            var itemData = new Item("Test1", 1);
            var itemData2 = new Item("Test1", 1);
            i.AddItem(itemData, 1);
            i2.AddItem(itemData2, 1);
            i.SwitchPositions(0, 0,i2);
            Assert.AreEqual(itemData, i2.GetItem(0));
            Assert.AreEqual(itemData2, i.GetItem(0));
            yield return null;
        }
    }
}