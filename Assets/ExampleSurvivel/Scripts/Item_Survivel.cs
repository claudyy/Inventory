using ClaudeFehlen.ItemSystem.Simple;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Survivel : Item
{
    ItemData_Survivel data;
    public float PercentageOfDurability {
        get {
            return .5f;
        }
    }

    public Item_Survivel(ItemData_Survivel data,int count) : base(data.name, count)
    {
        this.data = data;
        stackCount = data.stack;
    }

}
