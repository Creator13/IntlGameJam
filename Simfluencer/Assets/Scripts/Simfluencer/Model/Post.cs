using System;
using UnityEngine;

namespace Simfluencer.Model {
    [CreateAssetMenu(menuName = "Simfluencer/Post", fileName = "Post")]
    public class Post : ScriptableObject {
        [NonSerialized] public Scenario scenario;

        [SerializeField, TextArea] private string postContent;

        // [SerializeField] private ScenarioEnding setting;
        [SerializeField, Range(0, 1)] private float impact = 1;
        [SerializeField, Range(-1, 1)] private float positivity;
        [SerializeField, Range(-1, 1)] private float credibility;

        public string Content => postContent;
        public float Impact => impact;
        public float Positivity => positivity;
        public float Credibility => credibility;
    }
}
