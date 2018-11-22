using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ClaudeFehlen.ItemSystem.Simple {
    public class Crafting {
        private List<ItemCrafting> list;

        public Crafting(List<ItemCrafting> list) {
            this.list = list;
        }

        public void Craft(IInventoryOwner owner, int index) {
            if (index >= list.Count)
                return;
            var select = SelectRecipe(owner, index);
            if (select == -1)
                return;
            Craft(owner, index, select);
        }
        public void Craft(IInventoryOwner owner, int index,int selectRecipe) {
            if (index >= list.Count)
                return;
            if (CanCraft(owner, index, selectRecipe) == false)
                return;
            var itemCraft = list[index];
            var recipes = itemCraft.GetRecipes();
            RemoveItems(owner, recipes[selectRecipe]);
            AddItems(itemCraft, owner);
        }
        private static void AddItems(ItemCrafting itemCraft, IInventoryOwner owner) {
            var inventories = owner.GetInventories();
            var result = itemCraft.GetItems();
            for (int inv = 0; inv < inventories.Count; inv++) {
                for (int i = 0; i < result.Count; i++) {
                    if (inventories[inv].HaveSpaceForItem(result[i]))
                        inventories[inv].AddItem(result[i], result[i].count);
                }
            }
        }

        private static void RemoveItems(IInventoryOwner owner, CraftingRecipe selectedRecipe) {
            var inventories = owner.GetInventories();

            for (int i = 0; i < selectedRecipe.items.Count; i++) {
                var count = selectedRecipe.counts[i];
                for (int inv = 0; inv < inventories.Count; inv++) {
                    var remove = Mathf.Min(count, inventories[inv].CountOfItem(selectedRecipe.items[i]));
                    inventories[inv].RemoveItem(selectedRecipe.items[i], remove);
                    count -= remove;
                }
            }
        }

        public bool CanCraft(IInventoryOwner owner, int index) {
            int selectRecipe = SelectRecipe(owner, index);
            if (selectRecipe == -1)
                return false;
            return true;
        }
        public bool CanCraft(IInventoryOwner owner, int index,int recipeIndex) {
            var itemCraft = list[index];
            var inventories = owner.GetInventories();
            var recipes = itemCraft.GetRecipes();

            var selectedRecipe = recipes[recipeIndex];
            bool result = OwnerHasEnoughItemsForRecipe(inventories, selectedRecipe);

            return result;
        }
        private static bool OwnerHasEnoughItemsForRecipe(List<Inventory> inventories, CraftingRecipe selectedRecipe) {
            for (int i = 0; i < selectedRecipe.items.Count; i++) {
                var count = 0;
                for (int inv = 0; inv < inventories.Count; inv++) {
                    count += inventories[inv].CountOfItem(selectedRecipe.items[i]);
                }
                var neededCount = selectedRecipe.counts[i];
                if (neededCount > count)
                    return false;
            }

            return true;
        }

        private int SelectRecipe(IInventoryOwner owner, int index) {
            var itemCraft = list[index];
            var recipes = itemCraft.GetRecipes();
            for (int r = 0; r < recipes.Count; r++) {
                if (CanCraft(owner, index, r) == true) {
                    return r;
                }
            }

            return -1;
        }
    }
}