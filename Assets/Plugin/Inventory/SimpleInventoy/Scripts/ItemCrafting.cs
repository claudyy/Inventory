using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ClaudeFehlen.ItemSystem.Simple {
    [System.Serializable]
    public class CraftingRecipe {
        public List<Item> items;
        public List<int> counts;

        public CraftingRecipe(List<Item> items, List<int> count) {
            this.items = items;
            this.counts = count;
        }
    }
    public class ItemCrafting {
        
        List<Item> craftResult;
        List<CraftingRecipe> recipes;
        public ItemCrafting(List<Item> craftResult, CraftingRecipe recipes) {
            this.craftResult = craftResult;
            this.recipes = new List<CraftingRecipe>() { recipes };
        }
        public ItemCrafting(List<Item> craftResult, List<CraftingRecipe> recipes) {
            this.craftResult = craftResult;
            this.recipes = recipes;
        }
        public List<CraftingRecipe> GetRecipes() {
            return new List<CraftingRecipe>(recipes);
        }
        public List<Item> GetItems() {
            return new List<Item>(craftResult);
        }
    }
}