using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClaudeFehlen.ItemSystem.Simple{

    public interface IInventoryOwner {
    
        List<Inventory> GetInventories();
        int GetCurrentCurency();
        void AddCurrency(int count);
        void RemoveCurrency(int count);


    }
}