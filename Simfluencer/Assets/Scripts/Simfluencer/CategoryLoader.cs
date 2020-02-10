using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Simfluencer {
    public class CategoryLoader {
        private static CategoryLoader instance;
        public static CategoryLoader Instance => instance ?? (instance = new CategoryLoader());

        public List<PostCategory> Categories { get; private set; }
        
        private CategoryLoader() {
            Load();
        }

        public PostCategory GetCategory(string name) {
            return Categories.Find(cat => cat.Name == name);
        }

        private void Load() {
            Categories = Resources.LoadAll<PostCategory>("Categories").ToList();
        }
    }
}
