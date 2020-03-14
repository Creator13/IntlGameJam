using Simfluencer.UI.Screens;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Simfluencer.UI.Editor {
    [CustomEditor(typeof(ScreenTransitionButton))]
    public class ScreenTransitionButtonInspector : UnityEditor.Editor {
        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();

            var button = (ScreenTransitionButton) target;
            var uiManager = button.GetComponentInParent<UIManager>();
            var screens = uiManager.GetScreens();

            var nextScreenProperty = serializedObject.FindProperty("nextScreen");
            
            // Obtain the currently selected screen
            var selectedIndex = 0;
            var selectedScreen = nextScreenProperty.objectReferenceValue as Screen;
            if (selectedScreen) {
                if (screens.Contains(selectedScreen)) {
                    selectedIndex = screens.IndexOf(selectedScreen);
                }
            }
            else {
                nextScreenProperty.objectReferenceValue = screens[0];
            }

            var popup = new PopupField<Screen>("Next screen", screens, selectedIndex) {
                // bindingPath = "nextScreen", 
                formatListItemCallback = screen => screen.Name,
                formatSelectedValueCallback = screen => screen.Name
            };
            popup.RegisterValueChangedCallback(evt => {
                nextScreenProperty.objectReferenceValue = evt.newValue;
                nextScreenProperty.serializedObject.ApplyModifiedProperties();
            });
            root.Add(popup);

            return root;
        }
    }
}
