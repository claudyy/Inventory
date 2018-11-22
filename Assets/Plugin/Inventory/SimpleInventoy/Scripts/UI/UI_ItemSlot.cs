using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace ClaudeFehlen.ItemSystem.Simple{
    public class UI_ItemSlot : UI_BaseSlot {
        public Image display;
        public Text count;

        public virtual void UpdateView(Item item) {
            count.text = item.count == 0 ? "" : item.count.ToString();
            if(item == Item.Empty) {
                display.gameObject.SetActive(false);
                return;
            }
            display.gameObject.SetActive(true);

            var sprite = item.GetUIDisplay();
            if (sprite != null)
                display.sprite = sprite;
            else
                Debug.LogWarning(item.name + " has no sprite");
        }
    }
}