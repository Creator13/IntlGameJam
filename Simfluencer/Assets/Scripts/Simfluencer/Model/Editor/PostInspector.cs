using Simfluencer.Model;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Simfluencer.Editor {
    [CustomEditor(typeof(Post))]
    [CanEditMultipleObjects]
    public class PostInspector : UnityEditor.Editor {
        private PropertyField scenarioSettingField;
        private Toggle overrideToggle;
        private VisualElement overrideContainer;

        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();

            root.Add(new PropertyField(serializedObject.FindProperty("tagline")));
            root.Add(new PropertyField(serializedObject.FindProperty("postContent")));
            root.Add(new PropertyField(serializedObject.FindProperty("impact")));

            scenarioSettingField = new PropertyField(serializedObject.FindProperty("setting"), "Scenario ending preset");
            root.Add(scenarioSettingField);

            var overrideProperty = serializedObject.FindProperty("overrideSettings");
            overrideToggle = new Toggle("Override") {value = overrideProperty.boolValue};
            root.Add(overrideToggle);

            overrideToggle.RegisterValueChangedCallback(evt => {
                overrideProperty.boolValue = evt.newValue;
                serializedObject.ApplyModifiedProperties();
                
                SetOverride(evt.newValue);
            });
            
            overrideContainer = new VisualElement();
            overrideContainer.Add(new PropertyField(serializedObject.FindProperty("positivity")));
            overrideContainer.Add(new PropertyField(serializedObject.FindProperty("credibility")));

            root.Add(overrideContainer);
            
            SetOverride(overrideProperty.boolValue);
            
            return root;
        }

        private void SetOverride(bool value) {
            scenarioSettingField.SetEnabled(!value);
            
            overrideContainer.SetEnabled(value);
        }
    }
}
