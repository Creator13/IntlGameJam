using System.Collections.Generic;
using System.Linq;
using Simfluencer.UI.Screen;
using UnityEngine;

namespace Simfluencer.UI {
    public class UIManager : MonoBehaviour {
        [SerializeField] private string startScreenName;
        [SerializeField] private List<Screen.Screen> screens;

        public Screen.Screen ActiveScreen { get; private set; }

        private void Start() {
            // TODO check integrity of scene setup while fetching components
            GetScreens();
            screens.ForEach(s => s.Active = false);

            TransitionToScreen(startScreenName);
        }

        public void TransitionToScreen(string name) {
            SetActiveScreen(GetScreen(name));
        }

        private Screen.Screen GetScreen(string name) {
            //TODO cleanup
            var maintype = name.Split('.')[0];
            var screen = screens.First(s => s.Name == maintype);
            
            if (screen is PostScreen pScreen) {
                var subtype = name.Split('.')[1];
                pScreen.Category = subtype;
                
                return pScreen;
            }

            return screen;
        }

        public List<Screen.Screen> GetScreens() {
            screens = GetComponentsInChildren<Screen.Screen>(true).ToList();
            return screens;
        }

        private void SetActiveScreen(Screen.Screen screen) {
            if (ActiveScreen != null) ActiveScreen.Active = false;
            ActiveScreen = screen;
            ActiveScreen.Active = true;
        }
    }
}
