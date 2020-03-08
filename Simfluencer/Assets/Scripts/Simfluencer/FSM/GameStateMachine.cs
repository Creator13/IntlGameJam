using System;
using System.Collections.Generic;

namespace Simfluencer.FSM {
    public delegate void StateEvent(IState newState);
    
    [Serializable]
    public class GameStateMachine {
        public event StateEvent StateChanged;
        
        private GameManager gameManager;

        private IState currentState;

        private IState startState;
        private Dictionary<IState, IState> transitions = new Dictionary<IState, IState>();
        
        public GameStateMachine(GameManager gameManager, IState startState) {
            this.gameManager = gameManager;
            this.startState = startState;
        }
        
        public void Start() {
            LoadState(startState);
        }

        private void LoadState(IState state) {
            currentState.Complete();
            
            currentState = state;
            
            StateChanged?.Invoke(currentState);
            
            currentState.Start();
        }

        public void TriggerTransition() {
            
        }
    }
}
