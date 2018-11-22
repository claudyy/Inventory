using ClaudeFehlen.ItemSystem.Simple;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : UI_WindoWithGridSlot<UI_ShopItem> {
    private InventoryShop shop;
    public UI_GenericButton buy;
    int selectItem = -1;
    public UI_ItemInfo info;
    public UI_ItemInfoPopup popUpInfo;
    IInventoryOwner customer;
    protected override void Awake() {
        base.Awake();
        buy.onClick += Buy;
    }
    public void SetShop(InventoryShop shop,IInventoryOwner customer) {
        this.shop = shop;
        this.customer = customer;
        FillGrid(shop.Size);
        UpdateView();
    }
    public void UpdateView() {
        for (int i = 0; i < shop.Size; i++) {
            slots[i].UpdateView(customer, shop, i);
        }
    }
    protected override void ClickSlot(UI_BaseSlot slot) {
        selectItem = GetIndex(slot);
        info.ShowDescription(shop.GetItemName(selectItem), shop.GetItemDescription(selectItem));
    }

    protected override void ClickUp(UI_BaseSlot slot) {
    }

    protected override void Drag(UI_BaseSlot slot) {
    }

    protected override void EnterSlot(UI_BaseSlot slot) {
        var index = GetIndex(slot);
        popUpInfo.ShowDescription(shop.GetItemName(index), shop.GetItemDescription(index),slot);
    }

    protected override void ExitSlot(UI_BaseSlot slot) {
        popUpInfo.Show(false);

    }
    public void Buy(UI_GenericButton b) {
        if (selectItem == -1)
            return;
        shop.Buy(selectItem,customer);
        UpdateView();
    }
}
