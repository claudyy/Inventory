using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace ClaudeFehlen.ItemSystem.Simple {

    public class Inventory  {
        private List<Item> items;
        public Inventory(int size) {
            items = new List<Item>();
            for (int i = 0; i < size; i++) {
                items.Add(Item.Empty);
            }
        }

        public Item GetItem(int i) {
            return items[i];
        }



        public List<Item> GetAllItems() {
            return new List<Item>(items);
        }
        public List<Item> GetAllItems(ItemType type) {
            var list = new List<Item>();
            for (int i = 0; i < items.Count; i++) {
                if (items[i].Type == type)
                    list.Add(items[i]);
            }
            return list;
        }
        public List<Item> GetAllItems(List<ItemType> types) {
            var list = new List<Item>();
            for (int i = 0; i < items.Count; i++) {
                if (items[i] == Item.Empty)
                    continue;
                for (int t = 0; t < types.Count; t++) {
                    if(items[i].Type == types[t]) {
                        list.Add(items[i]);
                        break;
                    }
                }
            }
            return list;
        }
        public List<Item> GetItemsThatContainInName(string v) {
            var list = new List<Item>();
            for (int i = 0; i < items.Count; i++) {
                if (items[i].name.IndexOf(v, StringComparison.OrdinalIgnoreCase) >=0)
                    list.Add(items[i]);
            }
            return list;
            }
        public void SortByName() {
            items = items.OrderBy(x => x == Item.Empty ? "z" : x.name).ToList();
            UpdateView();
        }
        public void SortByNameDescending() {
            items = items.OrderByDescending(x => x == Item.Empty ? "a" : x.name).ToList();
            UpdateView();

        }
        public void SortByType() {
            items = items.OrderBy(x => x == Item.Empty ? "z" : x.Type.ToString()).ToList();
            UpdateView();

        }
        public void SortByTypeDescending() {
            items = items.OrderByDescending(x => x == Item.Empty ? "a" : x.Type.ToString()).ToList();
            UpdateView();

        }
        #region CallBacks
        //public Action<Item> onAddItem;
        //public Action<Item,int> onAddItemInSlot;
        //
        public Action<Item> onThrowItemAway;
        public Action<Inventory> onUpdateView;
        #endregion
        #region Public Functions
        public void AddItem(Item data, int count) {
            // no space remove spawn pickup
            int addCount = count;
            //Find already with the same type
            for (int i = 0; i < items.Count; i++) {
                if (addCount != 0 && items[i] == data)
                    addCount -= items[i].Add(addCount);
            }
            //Find new Free Slot
            for (int i = 0; i < items.Count; i++) {
                if (addCount != 0 && items[i] == Item.Empty) {
                    var space = Mathf.Min(4, addCount);
                    items[i] = new Item(data, space);
                    addCount -= space;
                }
            }
            if (onThrowItemAway != null && addCount != 0)
                onThrowItemAway(new Item(data, addCount));
            UpdateView();
        }

        internal List<int> GetItemsThatContainInNameIndex(string filter) {
            var list = new List<int>();

            for (int i = 0; i < size; i++) {
                if (items[i].name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0)
                    list.Add(i);
            }
            for (int i = list.Count; i < size; i++) {
                list.Add(-1);
            }
            return list;
        }

        internal List<int> GetAllItemsIndex() {
            var list = new List<int>();
            for (int i = 0; i < size; i++) {
                list.Add(i);
            }
            return list;
        }

        private void UpdateView() {
            if (onUpdateView != null)
                onUpdateView(this);
        }

        public bool RemoveItem(Item data, int count) {
            int removeCount = count;
            if (CountOfItem(data) < count)
                return false;
            //Find already with the same type
            for (int i = 0; i < items.Count; i++) {
                if (removeCount != 0 && items[i] == data) {
                    removeCount -= items[i].Remove(removeCount);
                    if (items[i].count == 0)
                        items[i] = Item.Empty;
                }
                
            }
            UpdateView();

            return true;
        }
        public void SwitchPositions(int fromIndex, int toIndex) {
            SwitchPositions(fromIndex, toIndex, this);
        }
        public void SwitchPositions(int fromIndex, int toIndex,Inventory fromInventory) {
            if (fromIndex >= items.Count || toIndex >= items.Count)
                return;
            var itemFrom = fromInventory.items[fromIndex];
            fromInventory.items[fromIndex] = items[toIndex];
            items[toIndex] = itemFrom;
            UpdateView();

        }
        #endregion
        #region Public Utility
        public int size {
            get {
                return items.Count;
            }
        }
        public int CountOfItem(Item data) {
            var c = 0;
            for (int i = 0; i < items.Count; i++) {
                if (items[i] == data)
                    c += items[i].count;
            }
            return c;
        }

        public bool IsFull() {
            for (int i = 0; i < items.Count; i++) {
                if (items[i] ==Item.Empty || items[i].isFull == false)
                    return false;
            }
            return true;
        }
        #endregion


    }
}