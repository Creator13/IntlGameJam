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

        public Screen ActiveScreen {
            get => activeScreen;
            set {
                if (activeScreen != null) {
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

        public void TransitionToScreen(string name) {
            ActiveScreen = GetScreen(name);
        }

        private Screen GetScreen(string name) {
            //TODO cleanup
            if (screens == null) screens = GetScreens();
            
            var maintype = name.Split('.')[0];
            var screen = screens.First(s => s.Name == maintype);
            
            if (screen is PostScreen_OLD pScreen) {
                var subtype = name.Split('.')[1];
                pScreen.Category = subtype;
                
                return pScreen;
            }

            return screen;
        }

        public List<Screen> GetScreens() { 
            return GetComponentsInChildren<Screen>(true).ToList();
        }
    }
}
