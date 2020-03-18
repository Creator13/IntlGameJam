using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simfluencer.Model {
    public enum GameStateEnum {
        Stage0, Stage1, Stage2
    }

    [Serializable]
    public class PostReplyTransition {
        [SerializeField] private Scenario scenario;
        [SerializeField] private GameStateEnum fromState;
        [SerializeField] private GameStateEnum toState;
        [SerializeField] private string[] content;
    }

    public class PostAI {
        private GameStateManager stateManager;
        private List<PostReplyTransition> transitions;

        public PostAI(GameStateManager manager, List<PostReplyTransition> transitions) {
            stateManager = manager;
            stateManager.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(GameStateChangeEvent evt) {
            
        }
    }
}
