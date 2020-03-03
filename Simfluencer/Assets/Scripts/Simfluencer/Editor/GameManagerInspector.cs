using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Simfluencer.Editor {
    [CustomEditor(typeof(GameManager))]
    public class GameManagerInspector : UnityEditor.Editor {
        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();

            root.Add(new PropertyField(serializedObject.FindProperty("gameSettings")) {style = {marginBottom = 10}});

            root.Add(CreateScenarioList());

            return root;
        }

        private VisualElement CreateScenarioList() {
            var list = new VisualElement {name = "scenarioList"};

            var scenariosProperty = serializedObject.FindProperty("scenarios");
            var availableScenarios = Resources.LoadAll<Model.Scenario>("Scenarios");
            
            var activeScenarios = new List<Model.Scenario>();
            for (var i = 0; i < scenariosProperty.arraySize; i++) {
                activeScenarios.Add((Model.Scenario) scenariosProperty.GetArrayElementAtIndex(i).objectReferenceValue);
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

        private void UpdateScenariosProperty(List<Model.Scenario> scenarioList) {
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
