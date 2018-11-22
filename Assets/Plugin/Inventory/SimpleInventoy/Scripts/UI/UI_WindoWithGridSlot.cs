using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class UI_WindoWithGridSlot<T> : MonoBehaviour where T : UI_BaseSlot{

    public List<T> slots;
    public T slotPrefab;
    public Transform content;
    protected virtual void Awake() {
        slots = content.GetComponentsInChildren<T>().ToList();

    }
    protected void FillGrid(int size) {
        for (int i = 0; i < size; i++) {
            if (slots.Count <= i)
                slots.Add(Instantiate(slotPrefab, content));
            slots[i].onEnter += EnterSlot;
            slots[i].onExit += ExitSlot;
            slots[i].onClick += ClickSlot;
            slots[i].onDrag += Drag;
            slots[i].onUp += ClickUp;
        }
    }
    protected abstract void EnterSlot(UI_BaseSlot slot);
    protected abstract void ExitSlot(UI_BaseSlot slot);
    protected abstract void ClickSlot(UI_BaseSlot slot);
    protected abstract void Drag(UI_BaseSlot slot);
    protected abstract void ClickUp(UI_BaseSlot slot);
    protected int GetIndex(UI_BaseSlot slot) {
       return slots.IndexOf(slot as T);
    }
}
