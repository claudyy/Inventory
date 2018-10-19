using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ClaudeFehlen.ItemSystem.Simple {

    public class InventoryOwner : MonoBehaviour,IInventoryOwner {
        List<Inventory> inventories = new List<Inventory>();
        public int currency;
        private void Awake() {
            inventories.Add(new Inventory(100));
            inventories.Add(new InventoryEquipement(12));
        }

        public List<Inventory> GetInventories() {
            return inventories;
        }

        public int GetCurrentCurency() {
            return currency;
        }
    }
}
