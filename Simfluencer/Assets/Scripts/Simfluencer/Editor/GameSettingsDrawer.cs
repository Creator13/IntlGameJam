using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Simfluencer.Editor {
    [CustomPropertyDrawer(typeof(GameSettings))]
    public class GameSettingsDrawer : PropertyDrawer {
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            var root = new Foldout {text = property.displayName};

            root.Add(new PropertyField(property.FindPropertyRelative("startFollowers")));
            root.Add(new PropertyField(property.FindPropertyRelative("startCredibility")));
            root.Add(new PropertyField(property.FindPropertyRelative("startPositivity")));
            root.Add(new PropertyField(property.FindPropertyRelative("basePostImpact")));
            
            root.Add(UITools.CreateScenarioEndingList(property.FindPropertyRelative("scenarioSettings"), "Scenario Settings", true));

            return root;
        }
    }

    [CustomPropertyDrawer(typeof(ScenarioSettings))]
    public class ScenarioSettingsDrawer : PropertyDrawer {
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            var root = new VisualElement();
        
            root.Add(new PropertyField(property.FindPropertyRelative("positivityImpact")));
            root.Add(new PropertyField(property.FindPropertyRelative("credibilityImpact")));
            root.Add(new PropertyField(property.FindPropertyRelative("followerChangeMultiplier")));
        
            return root;
        }
    }
}
