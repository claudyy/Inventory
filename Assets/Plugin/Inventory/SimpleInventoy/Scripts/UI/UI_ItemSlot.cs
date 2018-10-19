using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace ClaudeFehlen.ItemSystem.Simple{
    public class UI_ItemSlot : MonoBehaviour,IDragHandler,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler,IPointerUpHandler,IPointerDownHandler {
        public Image display;
        public Text count;
        public Action<UI_ItemSlot> onEnter;
        public Action<UI_ItemSlot> onExit;
        public Action<UI_ItemSlot> onClick;
        public Action<UI_ItemSlot> onUp;
        public Action<UI_ItemSlot> onDrag;
        public void OnDrag(PointerEventData eventData) {
            if (eventData.pointerDrag) {
                if (onDrag != null)
                    onDrag(this);
            }
        }

        public void OnPointerClick(PointerEventData eventData) {

        }

        public void OnPointerDown(PointerEventData eventData) {
            if (onClick != null)
                onClick(this);
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if (onEnter != null)
                onEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData) {
            if (onExit != null)
                onExit(this);
        }

        public void OnPointerUp(PointerEventData eventData) {
            if (onUp != null)
                onUp(this);
        }

        public void UpdateView(Item item) {
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