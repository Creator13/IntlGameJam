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
            root.Add(new PropertyField(serializedObject.FindProperty("icon")));
            
            root.Add(UITools.CreateScenarioEndingList(serializedObject.FindProperty("midwayBackgrounds"), "Scenario midway backgrounds"));
            root.Add(UITools.CreateScenarioEndingList(serializedObject.FindProperty("endBackgrounds"), "Scenario ending backgrounds"));
            
            var audioFoldout = new Foldout {text = "Music"};
            audioFoldout.Add(new PropertyField(serializedObject.FindProperty("neutralMusic")));
            audioFoldout.Add(new PropertyField(serializedObject.FindProperty("negativeMusic")));
            audioFoldout.Add(new PropertyField(serializedObject.FindProperty("positiveMusic")));
            root.Add(audioFoldout);

            root.Add(new PostList(serializedObject.FindProperty("posts"), serializedObject));
            
            return root;
        }
    }
}
