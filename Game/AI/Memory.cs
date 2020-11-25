using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ButtonOffice.AI
{
    public class Memory : PersistentObject
    {
        public IEnumerable<String> Keys
        {
            get
            {
                return _Items.Keys;
            }
        }
        
        private Dictionary<String, Object> _Items;
        
        public Memory()
        {
            _Items = new Dictionary<String, Object>();
        }
        
        public void Add(String Key, Object Data)
        {
            Debug.Assert(_Items.ContainsKey(Key) == false);
            _Items.Add(Key, Data);
        }
        
        public Object Get(String Key)
        {
            Debug.Assert(_Items.ContainsKey(Key) == true);
            
            return _Items[Key];
        }
        
        public T Get<T>(String Key) where T : class
        {
            Debug.Assert(_Items.ContainsKey(Key) == true);
            
            var Result = _Items[Key];
            
            Debug.Assert(Result.GetType() == typeof(T));
            
            return Result as T;
        }
        
        public void Remove(String Key)
        {
            Debug.Assert(_Items.ContainsKey(Key) == true);
            _Items.Remove(Key);
        }
        
        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            foreach(var Item in _Items)
            {
                var ItemObjectStore = ObjectStore.Save("item");
                
                ItemObjectStore.Save("key", Item.Key);
                if(Item.Value is PersistentObject)
                {
                    ItemObjectStore.SaveReference("value", Item.Value);
                }
                else
                {
                    ItemObjectStore.SaveContained("value", Item.Value);
                }
            }
        }
        
        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            ObjectStore.LoadForEach("item", delegate(LoadObjectStore ItemObjectStore)
                                            {
                                                var Key = ItemObjectStore.LoadStringProperty("key");
                                                var Value = ItemObjectStore.LoadProperty("value");
                                                
                                                _Items.Add(Key, Value);
                                            });
        }
    }
}
