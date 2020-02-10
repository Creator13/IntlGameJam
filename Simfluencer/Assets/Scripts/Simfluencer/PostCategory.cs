using System;
using UnityEngine;

namespace Simfluencer {
    [CreateAssetMenu(menuName = "Simfluencer/Post Category", fileName = "NewCategory")]
    public class PostCategory : ScriptableObject {
        [SerializeField] private string name;
        public string Name => name;

        [SerializeField] private string[] postOptions;
        public string[] PostOptions => postOptions;
        
        [field: NonSerialized] public bool Used { get; set; }
    }
}
