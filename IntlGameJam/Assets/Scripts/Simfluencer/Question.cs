using UnityEngine;

namespace Simfluencer {
    [CreateAssetMenu(fileName = "NewQuestion", menuName = "Simfluencer/Question")]
    public class Question : ScriptableObject {
        [TextArea, SerializeField] private string prompt;
        [Range(0, 1), SerializeField] private float credibility;
        [Range(-1, 1), SerializeField] private float influence;
        [Range(0, 1), SerializeField] private float difficulty;
        
        [SerializeField] private Reply[] replies;
        
        public string Prompt => prompt;
        public float Credibility => credibility;
        public float Influence => influence;
        public float Difficulty => difficulty;
        public Reply[] Replies => replies;

        public override string ToString() {
            return $"Prompt: {(prompt == "" ? "[no headline]" : prompt)} ({name})";
        }
    }
}
