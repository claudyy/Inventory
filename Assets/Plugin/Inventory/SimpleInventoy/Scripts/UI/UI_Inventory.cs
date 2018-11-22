using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClaudeFehlen.ItemSystem.Simple {

    public class UI_Inventory<T> : UI_WindoWithGridSlot<T>, I_UI_Inventory where T : UI_ItemSlot{
        List<int> displayIndexs;
        public UI_ItemInfo info;
        public UI_ItemInfoPopup popUpinfo;
        public bool popUpNextToSlot;
        public Vector2 popUpOffest;
        private Inventory inventory;

        public UI_InventorySort sort;
        public UI_FilterText filterText;
        string curFitlerText = "";

        public UI_DragIcon dragIcon;


        // Use this for initialization
        protected override void Awake () {
            base.Awake();
            //slots = content.GetComponentsInChildren<UI_ItemSlot>().ToList();
            
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
        public List<string> GetSortingList()
        {
            return new List<string>() { "names", "descending names", "type", "descending type" };
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
            FillGrid(inventory.size);
            for (int i = 0; i < slots.Count; i++)
            {
                displayIndexs.Add(i);
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
        protected override void EnterSlot(UI_BaseSlot slot) {
            var index = displayIndexs[slots.IndexOf(slot as T)];
            if (index == -1) {
                return;
            }
            if (info != null) {
                if(inventory.GetItem(index) != Item.Empty)
                    info.ShowDescription(inventory.GetItem(index));
            }
            if (popUpinfo != null && inventory.GetItem(index) != Item.Empty) {
                if (popUpNextToSlot) {
                    popUpinfo.GetComponent<RectTransform>().SetPositionAndRotation(slot.GetComponent<RectTransform>().position + (Vector3)popUpOffest, Quaternion.identity);
                }
                var item = inventory.GetItem(index);
                popUpinfo.ShowDescription(item.name,item.GetDescription(),slot);
            }
            UI_DragIcon.dragTo = index;

        }
        protected override void ExitSlot(UI_BaseSlot slot) {
            if (popUpinfo != null)
                popUpinfo.Show(false);
        }
        protected override void ClickSlot(UI_BaseSlot slot) {
            var index = displayIndexs[slots.IndexOf(slot as T)];
            if (index == -1)
                return;

            if (UI_DragIcon.dragFrom != -1) {
                inventory.SwitchPositions(UI_DragIcon.dragFrom, UI_DragIcon.dragTo);
                UI_DragIcon.dragFrom = -1;
                UI_DragIcon.dragTo = -1;
                dragIcon.Show(false);
            } else {
                if (inventory.GetItem(index) == Item.Empty)
                    return;
                UI_DragIcon.dragFrom = displayIndexs[slots.IndexOf(slot as T)];
                dragIcon.SetItem(inventory.GetItem(index));
            }
        }
        protected override void Drag(UI_BaseSlot slot) {

        }
        protected override void ClickUp(UI_BaseSlot slot) {
            if (UI_DragIcon.dragFrom == -1)
                return;
        }


    }
}
