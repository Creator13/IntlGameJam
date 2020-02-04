using UnityEngine;

namespace Simfluencer {
    [CreateAssetMenu(fileName = "NewArticle", menuName = "Simfluencer/Article")]
    public class Article : ScriptableObject {
        [TextArea, SerializeField] public string headline; 
        [Range(0, 1), SerializeField] public float credibility;
        [Range(0, 1), SerializeField] public float influence;

        public override string ToString() {
            return $"Article: {(headline == "" ? "[no headline]" : headline)} ({name})";
        }
    }
}
