using Amazon.Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.API.Database
{
    public class Filebase
    {
        private string _root;
        private string _appointmentRoot;
        private string _todoRoot;
        private static Filebase _instance;


        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }
        public int NextItemId
        {
            get
            {
                if (!Items.Any())
                {
                    return 1;
                }
                return Items.Select(i => i.Id).Max() + 1;
            }
        }
        private Filebase()
        {
            _root = @"C:\temp\Items";
        }

        public Item AddOrUpdate(Item i)
        {
            //set up a new Id if one doesn't already exist
            if(i.Id <= 0)
            {
                i.Id = NextItemId;
            }

            //go to the right place]
            string path = $"{_root}\\{i.Id}.json";
           
            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(i));

            //return the item, which now has an id
            return i;
        }

        // this is where shopping cart will be implemented
        // public List<ToDo> ToDos
        //{ 
        //    get
        //    {
        //        var root = new DirectoryInfo(_todoRoot);
        //        var _todos = new List<ToDo>();
        //        foreach(var todoFile in root.GetFiles())
        //        {
        //            var todo = JsonConvert.DeserializeObject<ToDo>(File.ReadAllText(todoFile.FullName));
        //            _todos.Add(todo);
        //        }
        //        return _todos;
        //    }
        //}

        public List<Item> Items
        {
            get
            {
                var root = new DirectoryInfo(_root);
                var _items = new List<Item>();
                foreach (var appFile in root.GetFiles())
                {
                    var item = JsonConvert.DeserializeObject<Item>(File.ReadAllText(appFile.FullName));
                    if(item != null)
                    {
                        _items.Add(item);
                    }
                    
                }
                return _items;
            }
        }

        public Item Delete(int id)
        {
            //TODO: refer to AddOrUpdate for an idea of how you can implement this.
            var itemToReturn = Items.FirstOrDefault(i => i.Id == id);
            if (itemToReturn == null)
            {
                // Item not found
                throw new FileNotFoundException($"Item with ID {id} not found.");
            }
            string path = Path.Combine(_root, $"{id}.json");
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
                return itemToReturn;
            }
            else
                throw new FileNotFoundException($"File with ID {id} not found.");
        }
    }


    // ------------------- FAKE MODEL FILES, REPLACE THESE WITH A REFERENCE TO YOUR MODELS -------- //

}
