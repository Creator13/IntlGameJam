using UnityEditor;
using UnityEngine;

namespace Simfluencer.Editor {
    [CustomEditor(typeof(PostCategory))]
    public class PostCategoryEditor : UnityEditor.Editor {
        private SerializedProperty postOptions;

        private SerializedProperty sciPos;
        private SerializedProperty sciNeg;
        private SerializedProperty consPos;
        private SerializedProperty consNeg;

        private void OnEnable() {
            postOptions = serializedObject.FindProperty("postOptions");
        }

        public override void OnInspectorGUI() {
            if (postOptions.arraySize != 4) {
                postOptions.arraySize = 4;
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("name"));
            
            sciPos = postOptions.GetArrayElementAtIndex(0);
            sciNeg = postOptions.GetArrayElementAtIndex(1);
            consPos = postOptions.GetArrayElementAtIndex(2);
            consNeg = postOptions.GetArrayElementAtIndex(3);
            
            EditorGUILayout.PrefixLabel("Scientific positive");
            sciPos.stringValue = EditorGUILayout.TextArea(sciPos.stringValue, GUILayout.Height(60));

            EditorGUILayout.PrefixLabel("Scientific negative");
            sciNeg.stringValue = EditorGUILayout.TextArea(sciNeg.stringValue, GUILayout.Height(60));

            EditorGUILayout.PrefixLabel("Conspiracy positive");
            consPos.stringValue = EditorGUILayout.TextArea(consPos.stringValue, GUILayout.Height(60));

            EditorGUILayout.PrefixLabel("Conspiracy negative");
            consNeg.stringValue = EditorGUILayout.TextArea(consNeg.stringValue, GUILayout.Height(60));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
