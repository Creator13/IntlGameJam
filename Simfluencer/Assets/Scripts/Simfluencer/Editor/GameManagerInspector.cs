using System.Collections.Generic;
using Simfluencer.Model;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Simfluencer.Editor {
    [CustomEditor(typeof(GameManager))]
    public class GameManagerInspector : UnityEditor.Editor {
        private VisualElement scenarioScoreView;

        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();

            root.Add(new PropertyField(serializedObject.FindProperty("settings")));

            root.Add(CreateScenarioList());

            root.Add(new PostList(serializedObject.FindProperty("neutralPosts"), serializedObject));

            scenarioScoreView = new VisualElement();
            var scoreViewFoldout = new Foldout {name = "scenarioScores", text = "Scores"};
            scoreViewFoldout.Add(scenarioScoreView);
            root.Add(scoreViewFoldout);

            EditorApplication.update = RefreshScenarioScoreView;

            return root;
        }

        private void RefreshScenarioScoreView() {
            scenarioScoreView.Clear();

            if (!EditorApplication.isPlaying) {
                scenarioScoreView.Add(new Label("Please enter play mode"));
                return;
            }
            
            var stateManager = ((GameManager) target).GameStateManager;
            var values = stateManager.ScenarioScoreDict;

            foreach (var kvp in values) {
                var field = new FloatField(kvp.Key.ScenarioName) {value = kvp.Value};
                field.SetEnabled(false);

                if (kvp.Key == stateManager.TopScenario()) {
                    field.style.color = Color.red;
                }
                
                scenarioScoreView.Add(field);
            }
        }

        private VisualElement CreateScenarioList() {
            var list = new Foldout {name = "scenarioList", text = "Scenarios"};

            var scenariosProperty = serializedObject.FindProperty("scenarios");
            var availableScenarios = Resources.LoadAll<Scenario>("Scenarios");

            var activeScenarios = new List<Scenario>();
            for (var i = 0; i < scenariosProperty.arraySize; i++) {
                activeScenarios.Add((Scenario) scenariosProperty.GetArrayElementAtIndex(i).objectReferenceValue);
            }

            foreach (var scenario in availableScenarios) {
                var listItem = new VisualElement();

                var toggle = new Toggle(scenario.name) {value = activeScenarios.Contains(scenario)};
                toggle.RegisterCallback((ChangeEvent<bool> evt) => {
                    if (evt.newValue) {
                        // check for contains may not be necessary
                        if (!activeScenarios.Contains(scenario)) {
                            activeScenarios.Add(scenario);
                            UpdateScenariosProperty(activeScenarios);
                        }
                    }
                    else {
                        // check for contains may not be necessary
                        if (activeScenarios.Contains(scenario)) {
                            activeScenarios.Remove(scenario);
                            UpdateScenariosProperty(activeScenarios);
                        }
                    }
                });

                listItem.Add(toggle);
                list.Add(toggle);
            }

            return list;
        }

        private void UpdateScenariosProperty(List<Scenario> scenarioList) {
            var scenariosProperty = serializedObject.FindProperty("scenarios");
            scenariosProperty.arraySize = scenarioList.Count;
            scenariosProperty.ClearArray();

            for (var i = 0; i < scenarioList.Count; i++) {
                scenariosProperty.InsertArrayElementAtIndex(i);
                scenariosProperty.GetArrayElementAtIndex(i).objectReferenceValue = scenarioList[i];
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
