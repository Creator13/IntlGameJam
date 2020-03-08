using System;
using System.Collections.Generic;
using System.Linq;
using Simfluencer.UI.Screens;
using UnityEngine;
using Screen = Simfluencer.UI.Screens.Screen;

namespace Simfluencer.UI {
    public class UIManager : MonoBehaviour {
        [SerializeField] private string startScreenName;
        [SerializeField] private List<Screen> screens;

        private Screen activeScreen;
        private readonly Stack<Screen> screenHistory = new Stack<Screen>();

        public Screen ActiveScreen {
            get => activeScreen;
            set {
                if (activeScreen != null) {
                    screenHistory.Push(activeScreen);
                    activeScreen.Active = false;
                }

                activeScreen = value;
                activeScreen.Active = true;
            }
        }

        private void Start() {
            // TODO check integrity of scene setup while fetching components
            screens = GetScreens();
            screens.ForEach(s => s.Active = false);

            TransitionToScreen(startScreenName);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (screenHistory.Count == 0) {
                    // TODO give warning prompt
                    GameManager.QuitGame();
                }
                else {
                    ReturnToLastScreen();
                }
            }
        }

        public void TransitionToScreen(string name) {
            ActiveScreen = GetScreen(name);
        }

        public void ReturnToLastScreen() {
            activeScreen.Active = false;
            activeScreen = screenHistory.Pop();
            activeScreen.Active = true;
        }

        private Screen GetScreen(string name) {
            //TODO cleanup
            if (screens == null) screens = GetScreens();
            
            return screens.First(s => s.Name == name);;
        }

        public List<Screen> GetScreens() { 
            return GetComponentsInChildren<Screen>(true).ToList();
        }
    }
}
