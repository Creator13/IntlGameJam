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
                nextScreenProperty.serializedObject.ApplyModifiedProperties();
            }

            string StringFormatCallback(Screen screen) => screen.Name == string.Empty ? "<unnamed>" : screen.Name;

            var popup = new PopupField<Screen>("Next screen", screens, selectedIndex) {
                // bindingPath = "nextScreen", 
                formatListItemCallback = StringFormatCallback,
                formatSelectedValueCallback = StringFormatCallback
            };
            popup.RegisterValueChangedCallback(evt => {
                nextScreenProperty.objectReferenceValue = evt.newValue;
                nextScreenProperty.serializedObject.ApplyModifiedProperties();
            });

            var backToggle = new Toggle("Back to previous") {bindingPath = "back"};
            backToggle.RegisterValueChangedCallback(evt => { popup.SetEnabled(!evt.newValue); });

            popup.SetEnabled(!serializedObject.FindProperty("back").boolValue);

            root.Add(backToggle);
            root.Add(popup);

            return root;
        }
    }
}
