using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GenericButton : MonoBehaviour,IPointerClickHandler {

    public Action<UI_GenericButton> onClick;
    public Text text;

    public void OnPointerClick(PointerEventData eventData) {
        if (onClick != null)
            onClick(this);
    }

    internal void SetText(string v) {
        text.text = v;
    }
}
