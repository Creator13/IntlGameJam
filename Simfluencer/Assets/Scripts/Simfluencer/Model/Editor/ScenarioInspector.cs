using Simfluencer.Model;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Simfluencer.Editor {
    [CustomEditor(typeof(Scenario))]
    public class ScenarioInspector : UnityEditor.Editor {
        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();
        
            root.Add(new PropertyField(serializedObject.FindProperty("scenarioName")));
            
            root.Add(UITools.CreateScenarioEndingList(serializedObject.FindProperty("midwayBackgrounds"), "Scenario midway backgrounds"));
            root.Add(UITools.CreateScenarioEndingList(serializedObject.FindProperty("endBackgrounds"), "Scenario ending backgrounds"));
            
            root.Add(new PostList(serializedObject.FindProperty("posts"), serializedObject));
            
            return root;
        }
    }
}
