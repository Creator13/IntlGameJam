using System.Collections.Generic;
using System.Linq;
using Simfluencer.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Screen = Simfluencer.UI.Screens.Screen;

namespace Simfluencer.Editor {
    public class ScreenOverview : EditorWindow {
        private VisualElement screenOverviewList;
        private UIManager uiManager;
        private List<Screen> screens;

        private bool dontReload;

        [MenuItem("UI/Screen overview")]
        public static ScreenOverview ShowWindow() {
            var window = GetWindow<ScreenOverview>();

            window.titleContent = new GUIContent("UI screen overview");
            return window;
        }

        private void OnEnable() {
            screenOverviewList = new VisualElement();
            rootVisualElement.Add(screenOverviewList);

            LoadScreens();
        }

        private void OnHierarchyChange() {
            LoadScreens();
        }

        private void LoadScreens() {
            if (!uiManager) uiManager = FindObjectOfType<UIManager>();

            if (!uiManager) {
                screenOverviewList.Add(new Label("No UI manager found"));
                return;
            }

            // Reload screens
            var screensUpdated = uiManager.GetScreens();
            // Only refresh the UI if the list of screens actually changed
            if (ScreenListsEqual(screens, screensUpdated)) return;
            
            // Refresh the reference list if the hierarchy actually changed
            screens = screensUpdated;

            screenOverviewList.Clear();
            foreach (var screen in screens) {
                screenOverviewList.Add(CreateScreenButton(screen));
            }
        }

        private Button CreateScreenButton(Screen screen) {
            var button = new Button {text = screen.Name};

            button.clickable.clicked += () => {
                // Don't use the Active property of the screen object for editing, because it's meant to be used in
                // play mode. Using it outside play mode will result in NullRef errors. Set the active property of the
                // gameObject directly instead.
                uiManager.GetScreens().ForEach(s => s.gameObject.SetActive(false));
                screen.gameObject.SetActive(true);
            };

            return button;
        }

        private static bool ScreenListsEqual(List<Screen> a, List<Screen> b) {
            if (a == null || b == null) return false;

            var aNames = a.Select(s => s.Name).ToList();
            var bNames = b.Select(s => s.Name).ToList();

            var firstNotSecond = aNames.Except(bNames).ToList();
            var secondNotFirst = bNames.Except(aNames).ToList();

            return !firstNotSecond.Any() && !secondNotFirst.Any();
        }
    }
}
