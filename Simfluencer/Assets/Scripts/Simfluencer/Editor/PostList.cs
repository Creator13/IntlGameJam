using System.Collections.Generic;
using System.Linq;
using Simfluencer.Model;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Simfluencer.Editor {
    public class PostList : PropertyField {
        private readonly SerializedObject serializedObject;
        private readonly SerializedProperty property;
        
        public PostList(SerializedProperty property, SerializedObject serializedObject) : base(property) {
            this.serializedObject = serializedObject;
            this.property = property;
            
            RegisterCallback<DragUpdatedEvent>(ValidateDrag);
            
            RegisterCallback<DragPerformEvent>(HandlePostDrop);
        }

        private static void ValidateDrag(DragUpdatedEvent evt) {
            if (DragAndDrop.objectReferences.Any(o => !(o is Post))) {
                DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
            }
            else {
                DragAndDrop.visualMode =  DragAndDropVisualMode.Generic;
            }
        }

        private void HandlePostDrop(DragPerformEvent evt) {
            var posts = new List<Post>(property.arraySize);

            for (var i = 0; i < property.arraySize; i++) {
                posts.Insert(i, (Post) property.GetArrayElementAtIndex(i).objectReferenceValue);
            }
                
            var dropped = DragAndDrop.objectReferences.Select(o => (Post) o).ToList();
            posts.AddRange(dropped);
            posts = posts.Distinct().ToList();
                
            property.ClearArray();
            property.arraySize = posts.Count;
            for (var i = 0; i < posts.Count; i++) {
                property.GetArrayElementAtIndex(i).objectReferenceValue = posts[i];
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
