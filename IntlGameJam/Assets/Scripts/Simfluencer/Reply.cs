using System;
using UnityEngine;

namespace Simfluencer {
    public enum ReplyType {
        Neutral, Conspiracy, Credible
    }
    
    [Serializable]
    public class Reply {
        [SerializeField] private string content;
        public string Content => content;

        [SerializeField] private ReplyType type;
        public ReplyType Type => type;
    }
}
