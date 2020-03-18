using System.Collections.Generic;
using Simfluencer.Model;
using UnityEngine;

namespace Simfluencer {
    [System.Serializable]
    public class GameSettings {
        [SerializeField] public int startFollowers = 48629;
        [SerializeField, Range(0, 1)] public float startCredibility = .58f;
        [SerializeField, Range(-1, 1)] public float startPositivity = 0f;
        [SerializeField, Range(0, 1)] public float basePostImpact = .2f;

        [SerializeField] public ScenarioSettings[] scenarioSettings;

        [SerializeField] public List<PostReplyTransition> postReplyTransitions;
    }
    
    [System.Serializable]
    public struct ScenarioSettings {
        [SerializeField, Range(-1, 1)] private float positivityImpact;
        [SerializeField, Range(-.5f, .5f)] private float credibilityImpact;
        [SerializeField, Range(-1, 1)] private float followerChangeMultiplier;

        public float PositivityImpact => positivityImpact;
        public float CredibilityImpact => credibilityImpact;
        public float FollowerChangeMultiplier => followerChangeMultiplier;
    }
}
