using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Generic_Repository
{
    public class GenericRepository<T> : IGenericrepository<T>
    {
        private string _path = string.Empty;
        public PropertyInfo _PropertyInfo { get; set; }  
        public GenericRepository(string path=null)
        {
            try
            {
                _PropertyInfo = typeof(T).GetProperty("Id");
            }
            catch (NullReferenceException)
            {
                
            }
            if(path is null)
            {
                if (!Directory.Exists("DataBase"))
                {
                    Directory.CreateDirectory("DataBase");
                }
                _path = $"DataBase/{typeof(T)}.json";
                if (!File.Exists(_path))
                {
                    File.Create(_path);
                }
            }
            else { _path = path; }
        }
        public void Add(T item)
        {
            string data;
            var objects = GetAll();         
            if(objects is null)
            {
                objects = new List<T>();
            }
            objects.Add(item);
            data = JsonConvert.SerializeObject(objects);
            File.WriteAllText(_path, data);
        }

        public void Delete(int id)
        {
            string data;
            var objects = GetAll();
            var item = Get(id);
            objects.Remove(item);
            data = JsonConvert.SerializeObject(objects);
            File.WriteAllText(_path, data);
        }

        public List<T> GetAll()
        {
            var data = File.ReadAllText(_path);
            var objects = JsonConvert.DeserializeObject<List<T>>(data);
            return objects;
        }

        public T Get(int id)
        {
            var objects = GetAll();
            if(objects is null)
            {
                throw new NullReferenceException();
            }
            var item = objects.FirstOrDefault(x => (int)_PropertyInfo.GetValue(x) == id);
            return item;
        }
    }
}
