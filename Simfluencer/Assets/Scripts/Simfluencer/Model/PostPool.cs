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

        private List<Post> next;
        private Dictionary<ScenarioEnding, List<Post>> nextPosts;

        public PostPool(GameStateManager manager, List<Post> neutralPosts) {
            this.manager = manager;

            LoadPosts();
            foreach (var post in neutralPosts) {
                posts.Add(post);
            }

            GenerateAllPosts();
        }

        public List<Post> GetPosts(ScenarioEnding scenario) {
            return nextPosts[scenario];
        }

        private void GenerateAllPosts() {
            nextPosts = new Dictionary<ScenarioEnding, List<Post>> {
                {ScenarioEnding.ConspiracyNegative, GeneratePosts(ScenarioEnding.ConspiracyNegative)},
                {ScenarioEnding.ConspiracyPositive, GeneratePosts(ScenarioEnding.ConspiracyPositive)},
                {ScenarioEnding.ScienceNegative, GeneratePosts(ScenarioEnding.ScienceNegative)},
                {ScenarioEnding.SciencePositive, GeneratePosts(ScenarioEnding.SciencePositive)}
            };
        }

        private List<Post> GeneratePosts(ScenarioEnding type) {
            var selectedPosts = new List<Post>(4);
            var neutralPosts = 2;
            var topScenarioCount = 2;

            var typePosts = posts.Where(post => post.Type == type).ToList();

            // Load the two top scenarios
            var topScenarios = manager.TopScenarios(topScenarioCount);
            if (topScenarios.Count < topScenarioCount) {
                neutralPosts += topScenarioCount - topScenarios.Count;
            }

            foreach (var scenario in topScenarios) {
                // Load all posts registered to this scenario
                var scenarioPostList = typePosts.Where(post => post.scenario == scenario).ToList();
                // If there are any, select one random one to return
                if (scenarioPostList.Count > 0) {
                    selectedPosts.Add(scenarioPostList[UnityEngine.Random.Range(0, scenarioPostList.Count - 1)]);
                }
                // If there are none, tell to load one extra post from neutral
                else {
                    neutralPosts++;
                }
            }

            // Select all neutral posts, i.e. the posts that do not belong to the top 2 scenarios
            // var neutralPostList = posts.Where(post => !topScenarios.Contains(post.scenario)).ToList();
            // FIXME This messes up (might be fixed 2020-03-26)
            var neutralPostList = typePosts.Where(post => !selectedPosts.Contains(post)).ToList();

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

            return selectedPosts;
        }

        public void Consume(Post post) {
            if (!posts.Contains(post)) {
                throw new ArgumentException("Tried to remove post that was not in pool");
            }

            posts.Remove(post);
            used.Add(post);

            GenerateAllPosts();
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
