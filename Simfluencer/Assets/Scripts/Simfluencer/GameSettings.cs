using UnityEngine;

namespace Simfluencer {
    [System.Serializable]
    public struct ScenarioSettings {
        [SerializeField, Range(-1, 1)] private float positivityImpact;
        [SerializeField, Range(-.5f, .5f)] private float credibilityImpact;
        [SerializeField, Range(-1, 1)] private float followerChangeMultiplier;

        public float PositivityImpact => positivityImpact;
        public float CredibilityImpact => credibilityImpact;
        public float FollowerChangeMultiplier => followerChangeMultiplier;
    }

    [System.Serializable]
    public class GameSettings {
        [SerializeField] internal int startFollowers = 48629;
        [SerializeField, Range(0, 1)] internal float startCredibility = .58f;
        [SerializeField, Range(-1, 1)] internal float startPositivity = 0f;
        [SerializeField, Range(0, 1)] internal float basePostImpact = .2f;

        [SerializeField] internal ScenarioSettings[] scenarioSettings;
    }
}
