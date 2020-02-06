﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Simfluencer.UI {
    public class UIManager : MonoBehaviour {
        [SerializeField] private string startScreenName;
        [SerializeField] private List<Screen> screens;

        private Screen activeScreen;

        private void Start() {
            // TODO check integrity of scene setup while fetching components
            screens = GetComponentsInChildren<Screen>(true).ToList();
            screens.ForEach(s => s.Active = false);

            TransitionToScreen(startScreenName);
        }

        public void TransitionToScreen(string name) {
            if (activeScreen != null) activeScreen.Active = false;
            activeScreen = GetScreen(name);
            activeScreen.Active = true;
        }

        private Screen GetScreen(string name) {
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
    }
}