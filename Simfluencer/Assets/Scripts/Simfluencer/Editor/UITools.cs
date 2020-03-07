using System;
using System.Linq;
using System.Text.RegularExpressions;
using Simfluencer.Model;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Simfluencer.Editor {
    public static class UITools {
        public static VisualElement CreateScenarioEndingList(SerializedProperty listProperty, string label, bool useHeader = false) {
            var list = new Foldout {text = label};

            var endingList = ((ScenarioEnding[]) Enum.GetValues(typeof(ScenarioEnding))).ToList();

            listProperty.arraySize = endingList.Count;
            listProperty.serializedObject.ApplyModifiedProperties();

            for (var i = 0; i < endingList.Count; i++) {
                VisualElement listElem;
                if (useHeader) {
                    list.Add(new Label(endingList[i].ToDisplayName()) {
                        style = {unityFontStyleAndWeight = new StyleEnum<FontStyle>(FontStyle.Bold)}
                    });
                    listElem = new PropertyField(listProperty.GetArrayElementAtIndex(i));
                }
                else {
                    listElem = new PropertyField(listProperty.GetArrayElementAtIndex(i), endingList[i].ToDisplayName());
                }

                list.Add(listElem);
            }

            return list;
        }

        private static string ToDisplayName(this ScenarioEnding s) {
            return Regex.Replace(s.ToString(), @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
        }
    }
}
