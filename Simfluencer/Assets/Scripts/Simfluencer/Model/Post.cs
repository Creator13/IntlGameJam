using System;
using UnityEngine;

namespace Simfluencer.Model {
    [CreateAssetMenu(menuName = "Simfluencer/Post", fileName = "Post")]
    public class Post : ScriptableObject {
        // Scenario should *not* be serialized as it is supposed to be set at runtime, making it more flexible.
        [NonSerialized] public Scenario scenario;

        [SerializeField] private string tagline;
        [SerializeField, TextArea] private string postContent;

        //todo // [SerializeField] private ScenarioEnding setting; 
        [SerializeField, Range(0, 1)] private float impact = 1;
        [SerializeField, Range(-1, 1)] private float positivity;
        [SerializeField, Range(-1, 1)] private float credibility;

        public string Tagline => tagline;
        public string Content => postContent;
        public float Impact => impact;
        public float Positivity => positivity;
        public float Credibility => credibility;
    }
}
