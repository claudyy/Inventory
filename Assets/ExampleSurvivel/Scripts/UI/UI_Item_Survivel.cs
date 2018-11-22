using ClaudeFehlen.ItemSystem.Simple;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Item_Survivel : UI_ItemSlot {

    public Image barDisplay;
    public override void UpdateView(Item item)
    {
        base.UpdateView(item);
        barDisplay.fillAmount = (item as Item_Survivel).PercentageOfDurability;
    }
}
