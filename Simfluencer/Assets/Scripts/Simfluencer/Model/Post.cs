using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

namespace Simfluencer.Model {
    [CreateAssetMenu(menuName = "Simfluencer/Post", fileName = "Post")]
    public class Post : ScriptableObject {
        // Scenario should *not* be serialized as it is supposed to be set at runtime, making it more flexible.
        [NonSerialized] public Scenario scenario;

        [SerializeField] private int postId;

        [SerializeField] private string tagline;
        [SerializeField, TextArea] private string postContent;

        [SerializeField] private ScenarioEnding setting;
        [SerializeField, Range(0, 1)] private float impact = 1;
        [SerializeField] private bool overrideSettings;
        [SerializeField, Range(0, 1)] private float positivity;
        [SerializeField, Range(-1, 1)] private float credibility;

        public int PostID => postId;
        public string Tagline => tagline;
        public string Content => postContent;
        public float Impact => impact;

        public float Positivity {
            get {
                if (overrideSettings) {
                    return positivity;
                }
                else {
                    return GameManager.Instance.GameSettings.scenarioSettings[(int) setting].PositivityImpact;
                }
            }
        }

        public float Credibility {
            get {
                if (overrideSettings) {
                    return credibility;
                }
                else {
                    return GameManager.Instance.GameSettings.scenarioSettings[(int) setting].CredibilityImpact;
                }
            }
        }

#if UNITY_EDITOR
        private void OnEnable() {
            if (postId == 0) {
                Debug.LogWarning($"Generating new post id for post {name}");
                EditorUtility.SetDirty(this);
                postId = GenerateID();
                AssetDatabase.SaveAssets();
                EditorUtility.ClearDirty(this);
            }
        }

        private static int GenerateID() {
            var posts = new List<Post>();
            foreach (var guid in AssetDatabase.FindAssets("t:Post")) {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                posts.Add(AssetDatabase.LoadAssetAtPath<Post>(assetPath));
            }

            int randomID;
            do {
                randomID = new Random().Next(1, int.MaxValue);
            } while (posts.Any(post => post.PostID == randomID));

            return randomID;
        }
#endif
    }
}
