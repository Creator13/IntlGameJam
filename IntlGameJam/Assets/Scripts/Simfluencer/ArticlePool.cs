using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Simfluencer {
    public class ArticlePool {
        // private static ArticlePool instance;
        // public static ArticlePool Instance => instance ?? (instance = new ArticlePool());

        private Dictionary<Question, bool> articles;

        private ArticlePool() {
            LoadArticles();
        }

        public Question[] GetRandomArticles(int count) {
            var pool = articles.Where(pair => !pair.Value).ToList();
            var length = pool.Count();

            var randIndexes = new List<int>();
            // Generate 'count' number of random indices, or if there aren't enough items, select as many as there are
            for (var i = 0; i < (length < count ? length : count); i++) {
                int index;
                do {
                    index = Random.Range(0, length);
                } while (randIndexes.Contains(index));

                randIndexes.Add(index);
            }

            var returnList = randIndexes.Select(i => pool[i].Key).ToArray();
            // Mark the items as used
            foreach (var article in returnList) {
                articles[article] = true;
            }
            
            return returnList;
        }

        public void Reset() {
            foreach (var pair in articles) {
                articles[pair.Key] = false;
            }
        }

        public void LoadArticles() {
            this.articles = new Dictionary<Question, bool>();
            
            var articles = Resources.LoadAll<Question>("Articles");
            foreach (var article in articles) {
                this.articles.Add(article, false);
            }
        }
    }
}
