using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Screen = Simfluencer.UI.Screens.Screen;

namespace Simfluencer.UI {
    public class UIManager : MonoBehaviour {
        [SerializeField] private string startScreenName;
        [SerializeField] private List<Screen> screens;

        private Screen activeScreen;
        private readonly Stack<Screen> screenHistory = new Stack<Screen>();

        private void Awake() {
            // Load screens only once on game startup
            screens = GetScreens();

#if UNITY_EDITOR
            // Check for duplicate screens in children
            foreach (var screen in screens.Where(screen => screens.Count(s => s.name == screen.name) > 1)) {
                Debug.LogError($"Screen \"{screen.name}\" has more than one occurrences in the UIManager");
            }
#endif
        }

        private void Start() {
            // Deactivate all active screens at start of game
            screens.ForEach(s => { s.gameObject.SetActive(false); });

            TransitionToScreen(startScreenName);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (activeScreen.HasActiveTutorial) return;

                ReturnToLastScreen();
            }
        }

        /// <summary>
        /// Go the screen with the provided name based on string-based lookup of the screen name.
        /// </summary>
        /// <param name="name">Name of the screen to go to.</param>
        /// <param name="saveToHistory">Indicates if this transition should be save to the screen history.</param>
        public void TransitionToScreen(string name, bool saveToHistory = true) {
            SetActiveScreen(GetScreen(name), saveToHistory);
        }

        /// <summary>
        /// Transition to the provided screen.
        /// </summary>
        /// <param name="screen">Screen to transition to.</param>
        /// <param name="saveToHistory">Indicates if this transition should be save to the screen history.</param>
        public void TransitionToScreen(Screen screen, bool saveToHistory = true) {
            if (!screens.Contains(screen))
                throw new InvalidOperationException(
                    $"Tried to transition to screen {screen.Name}, but this screen is not registered to this uiManager ({this})"
                );

            SetActiveScreen(screen, saveToHistory);
        }

        /// <summary>
        /// Activates the previously visited screen or quits the game if there are no more active screens.
        /// </summary>
        public void ReturnToLastScreen() {
            if (screenHistory.Count == 0) {
                // TODO give warning prompt
                GameManager.QuitGame();
            }
            else {
                SetActiveScreen(screenHistory.Pop(), false);
            }
        }

        /// <summary>
        /// Obtains all screens that are children of this UI Manager.
        /// </summary>
        /// <returns>A list of child screens of this UI Manager.</returns>
        public List<Screen> GetScreens() {
            return GetComponentsInChildren<Screen>(true).ToList();
        }

        public void ClearHistory() {
            screenHistory.Clear();
        }

        private void SetActiveScreen(Screen screen, bool saveToHistory = true) {
            if (activeScreen != null) {
                if (saveToHistory) screenHistory.Push(activeScreen);
                activeScreen.Active = false;
            }

            activeScreen = screen;
            activeScreen.Active = true;
        }

        private Screen GetScreen(string name) {
            //TODO cleanup
            if (screens == null) screens = GetScreens();

            return screens.First(s => s.Name == name);
        }
    }
}
