using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;
using Random = cvanbattum.Tools.Random;

namespace Simfluencer.Model {
    public class PostPool {
        private readonly GameStateManager manager;
        private readonly HashSet<Post> posts = new HashSet<Post>();
        private readonly HashSet<Post> used = new HashSet<Post>();

        public PostPool(GameStateManager manager, List<Post> neutralPosts) {
            this.manager = manager;

            LoadPosts();
            foreach (var post in neutralPosts) {
                posts.Add(post);
            }
        }

        public List<Post> GetPosts(ScenarioEnding scenario) {
            var selectedPosts = new List<Post>(4);
            var neutralPosts = 2;
            
            // Load the two top scenarios
            var topScenarios = manager.TopScenarios(2);
            foreach (var scen in topScenarios) {
                // Load all posts registered to this scenario
                var scenarioPostList = posts.Where(post => post.scenario == scen).ToList();
                // If there are any, select one random one to return
                if (scenarioPostList.Any()) {
                    selectedPosts.Add(scenarioPostList[UnityEngine.Random.Range(0, scenarioPostList.Count - 1)]);
                }
                // If there are none, tell to load one extra post from neutral
                else {
                    neutralPosts++;
                }
            }
            
            // Select all neutral posts, i.e. the posts that do not belong to the top 2 scenarios
            var neutralPostList = posts.Where(post => !topScenarios.Contains(post.scenario)).ToList();
            
            // If there are not enough posts to pool from, just return as many as possible
            if (neutralPosts > neutralPostList.Count) {
                neutralPosts = neutralPostList.Count;
            }
            
            // If there are no posts left to pool from, simply skip the rest and return
            if (neutralPosts == 0) {
                return selectedPosts;
            }
            
            // Generate random indexes
            var randIndexes = Random.UniqueIntegers(0, neutralPostList.Count, neutralPosts);
            // Select posts to return based on the indexes
            selectedPosts.AddRange(randIndexes.Select(i => neutralPostList[i]));
            
            Assert.IsTrue(selectedPosts.Count == 4);
            
            return selectedPosts;
        }

        public void Consume(Post post) {
            if (!posts.Contains(post)) {
                throw new ArgumentException("Tried to remove post that was not in pool");
            }

            posts.Remove(post);
            used.Add(post);
        }

        private void LoadPosts() {
            foreach (var scenario in manager.Scenarios) {
                foreach (var post in scenario.Posts) {
                    posts.Add(post);
                }
            }
        }
    }
}
