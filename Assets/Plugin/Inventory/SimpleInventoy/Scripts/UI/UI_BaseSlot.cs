using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UI_BaseSlot : MonoBehaviour, IPointerClickHandler,IPointerDownHandler,IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler {


    public Action<UI_BaseSlot> onEnter;
    public Action<UI_BaseSlot> onExit;
    public Action<UI_BaseSlot> onClick;
    public Action<UI_BaseSlot> onUp;
    public Action<UI_BaseSlot> onDrag;
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
}
