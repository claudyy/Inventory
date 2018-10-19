using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClaudeFehlen.ItemSystem.Simple {

    public class UI_Inventory : MonoBehaviour {
        public Transform content;
        public List<UI_ItemSlot> slots;
        List<int> displayIndexs;
        public UI_ItemSlot slotPrefab;
        public UI_ItemInfo info;
        public UI_ItemInfoPopup popUpinfo;
        public bool popUpNextToSlot;
        public Vector2 popUpOffest;
        private Inventory inventory;

        public UI_InventorySort sort;
        public UI_FilterText filterText;
        string curFitlerText = "";

        public UI_DragIcon dragIcon;

        public static int dragFrom;
        public static int dragTo;
        // Use this for initialization
        private void Awake () {
            slots = content.GetComponentsInChildren<UI_ItemSlot>().ToList();
            
        }
        private void Start() {
            if(sort != null) {
                sort.SetInvenory(this);
                sort.onClick+= Sort;
            }
            if(filterText != null) {
                filterText.onChangedInput += SortByName;
            }
        }
        internal List<string> GetSortingList() {
            return new List<string>() { "names","descending names","type","descending type" };
        }
        internal void Sort(int i) {
            switch (i) {
                case 0:
                    inventory.SortByName();
                    break;
                case 1:
                    inventory.SortByNameDescending();
                    break;
                case 2:
                    inventory.SortByType();
                    break;
                case 3:
                    inventory.SortByTypeDescending();
                    break;
                default:
                    break;
            }

        }
        internal void SortByName(string name) {
            curFitlerText = name;
            UpdateView(inventory);
        }
        public void SetInventory(Inventory inventory) {
            this.inventory = inventory;
            this.inventory.onUpdateView += UpdateView;
            displayIndexs = new List<int>();
            for (int i = 0; i < inventory.size; i++) {
                if (slots.Count <= i)
                    slots.Add(Instantiate(slotPrefab, content));
                displayIndexs.Add(-1);
                slots[i].UpdateView(inventory.GetItem(i));
                slots[i].onEnter += EnterSlot;
                slots[i].onExit += ExitSlot;
                slots[i].onClick += ClickSlot;
                slots[i].onDrag += Drag;
                slots[i].onUp += ClickUp;
            }

        }
        public void UpdateView(Inventory inventory) {
            var list = new List<Item>();
            if(curFitlerText != "") {
                displayIndexs = inventory.GetItemsThatContainInNameIndex(curFitlerText);
            } else {
                displayIndexs = inventory.GetAllItemsIndex();
            }
            for (int i = 0; i < displayIndexs.Count; i++) {
                if(displayIndexs[i] == -1)
                    slots[i].UpdateView(Item.Empty);
                else
                    slots[i].UpdateView(inventory.GetItem(displayIndexs[i]));
            }
        }
        void EnterSlot(UI_ItemSlot slot) {
            var index = displayIndexs[slots.IndexOf(slot)];
            if (index == -1)
                return;
            if (info != null) {
                if(inventory.GetItem(index) != Item.Empty)
                    info.ShowDescription(inventory.GetItem(index));
            }
            if (popUpinfo != null && inventory.GetItem(index) != Item.Empty) {
                if (popUpNextToSlot) {
                    popUpinfo.GetComponent<RectTransform>().SetPositionAndRotation(slot.GetComponent<RectTransform>().position + (Vector3)popUpOffest, Quaternion.identity);
                }
                popUpinfo.ShowDescription(inventory.GetItem(index));
            }
            dragTo = index;

        }
        void ExitSlot(UI_ItemSlot slot) {
            if (popUpinfo != null)
                popUpinfo.Show(false);
        }
        void ClickSlot(UI_ItemSlot slot) {
            var index = displayIndexs[slots.IndexOf(slot)];
            if (index == -1)
                return;
            if (inventory.GetItem(index) == Item.Empty)
                return;
            if (UI_Inventory.dragFrom != -1)
                return;
            dragFrom = displayIndexs[slots.IndexOf(slot)];
            dragIcon.SetItem(inventory.GetItem(index));
        }
        void Drag(UI_ItemSlot slot) {

        }
        void ClickUp(UI_ItemSlot slot) {
            if (UI_Inventory.dragFrom == -1)
                return;
            inventory.SwitchPositions(UI_Inventory.dragFrom, dragTo);
            UI_Inventory.dragFrom = -1;
            UI_Inventory.dragTo = -1;
            dragIcon.Show(false);
        }

    }
}
