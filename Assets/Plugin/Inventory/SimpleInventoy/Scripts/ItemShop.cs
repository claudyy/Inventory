using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ClaudeFehlen.ItemSystem.Simple {

    public class ItemShop {

        private Item _item;
        private int _count;
        private int _cost;

        public Item Item {
            get {
                return _item;
            }

        }

        public int Cost {
            get {
                return _cost;
            }
        }

        public ItemShop(Item item, int cost) {
            this._item = item;
            this._cost = cost;
        }
    }
}
