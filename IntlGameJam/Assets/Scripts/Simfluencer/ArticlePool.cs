using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Simfluencer {
    public class ArticlePool {
        private static ArticlePool instance;
        public static ArticlePool Instance => instance ?? (instance = new ArticlePool());

        private Dictionary<Article, bool> articles;

        public ArticlePool() {
            articles = new Dictionary<Article, bool>();
            LoadArticles();

            var res = GetRandomArticles(4);
            foreach (var item in res) {
                Debug.Log(item);
            }
        }

        public Article[] GetRandomArticles(int count) {
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

            return randIndexes.Select(i => pool[i].Key).ToArray();
        }

        public void Reset() {
            foreach (var pair in articles) {
                articles[pair.Key] = false;
            }
        }

        private void LoadArticles() {
            var articles = Resources.LoadAll<Article>("Articles");
            foreach (var article in articles) {
                this.articles.Add(article, false);
            }
        }
    }
}
