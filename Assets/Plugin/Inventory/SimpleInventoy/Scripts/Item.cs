using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ClaudeFehlen.ItemSystem.Simple {
    public class Item {
        private int _count;
        readonly int stackCount = 4;
        private string _name;
        private ItemType _type;
        public int count {
            get {
                return _count;
            }
        }

        public Sprite GetUIDisplay() {
            return null;
        }

        public bool isFull {
            get {
                return _count == stackCount;
            }
        }

        internal string GetDescription() {
            return "Test";
        }
        public Item(string name, int count) {
            this._count = count;
            this._name = name;
        }
        public Item(string name, ItemType type, int count){
            this._count = count;
            this._name = name;
            this._type = type;
        }

        public Item(Item item, int count) {
            this._name = item.name;
            this._count = count;
            this._type = item.Type;


        }

        internal int Add(int a) {
            int addCount = Mathf.Min(a, stackCount - _count);
            this._count += addCount;
            return addCount;
        }

        internal int Remove(int r) {
            int removeCount = Mathf.Min(r,_count);
            this._count -= removeCount;
            return removeCount;
        }
        private static Item _empty;
        public string name {
            get {
                return _name;
            }
        }

        public static Item Empty {
            get {
                if(_empty == null)
                    _empty = new Item("Empty",0);
                return _empty;
            }
        }

        public bool StackFull { get {
                return _count == stackCount;
            }

        }

        public ItemType Type {
            get {
                return _type;
            }
        }

        public static bool operator ==(Item a , Item b) {
            if (object.ReferenceEquals(a, null)) {
                return object.ReferenceEquals(b, null);
            }
            if (object.ReferenceEquals(b, null)) {
                return object.ReferenceEquals(a, null);
            }
            return (a._name == b._name);

        }
        public static bool operator !=(Item a, Item b) {
            if (object.ReferenceEquals(a, null)) {
                return !object.ReferenceEquals(b, null);
            }
            if (object.ReferenceEquals(b, null)) {
                return !object.ReferenceEquals(a, null);
            }
            return (a._name != b._name);

        }
        public override bool Equals(object obj) {
            if (!object.ReferenceEquals(obj, null) && obj is Item) {
                return _name == (obj as Item).name;
            }
            return base.Equals(obj);
        }
        public override string ToString() {
            return _name;
        }
    }
}