using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Simfluencer.UI.Editor {
    public class ScreenOverview : EditorWindow {
        private VisualElement screenOverviewList;
        private UIManager uiManager;
        private List<Screen.Screen> screens;

        private bool dontReload;

        [MenuItem("UI/Screen overview")]
        public static void ShowWindow() {
            var window = GetWindow<ScreenOverview>();

            window.titleContent = new GUIContent("UI screen overview");
        }

        private void OnEnable() {
            screenOverviewList = new VisualElement();
            rootVisualElement.Add(screenOverviewList);

            LoadScreens();
        }

        // private void OnHierarchyChange() {
        //     LoadScreens();
        // }
        //
        // private void OnInspectorUpdate() {
        //     LoadScreens();
        // }
        
        private void LoadScreens() {
            if (!uiManager) uiManager = FindObjectOfType<UIManager>();

            if (!uiManager) {
                screenOverviewList.Add(new Label("No UI manager found"));
                return;
            }

            var screens = uiManager.GetScreens();
            if (screens == this.screens) {
                return;
            }

            screenOverviewList.Clear();
            foreach (var screen in screens) {
                screenOverviewList.Add(CreateScreenButton(screen));
            }
        }

        private Button CreateScreenButton(Screen.Screen screen) {
            var button = new Button {text = screen.Name};

            button.clickable.clicked += () => {
                uiManager.GetScreens().ForEach(s => s.gameObject.SetActive(false));
                screen.gameObject.SetActive(true);
            };

            return button;
        }
    }
}
