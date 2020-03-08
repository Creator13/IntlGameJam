using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace cvanbattum.Audio.Editor {
    [CustomEditor(typeof(SoundEffectPlayer))]
    public class SoundEffectPlayerInspector : UnityEditor.Editor {
        public override VisualElement CreateInspectorGUI() {
            var root = new VisualElement();

            var options = SoundManager.EffectClips.Select(clip => clip.name).ToList();
            
            var soundNameProperty = serializedObject.FindProperty("soundName");

            // Display a message that says there are no sounds if the source folder is empty
            if (options.Count == 0) {
                root.Add(new Label("No sounds found. Please add sounds to \"Resources/Sound/Effects\"")
                    {style = {color = Color.red, whiteSpace = new StyleEnum<WhiteSpace>(WhiteSpace.Normal)}});
                soundNameProperty.stringValue = "";
                return root;
            }

            // As the default value of the soundName field is unknown, binding the popup field to it will result in an
            // exception. To avoid, set the property to the first value in the list of items if it is a value that is
            // not in the list of items.
            if (!options.Contains(soundNameProperty.stringValue)) {
                soundNameProperty.stringValue = options[0];
                serializedObject.ApplyModifiedProperties();
            }

            root.Add(new PopupField<string>("Effect name", options, 0) {bindingPath = "soundName"});

            return root;
        }
    }

    [CustomEditor(typeof(ButtonSoundEffect))]
    public class ButtonSoundEffectInspector : SoundEffectPlayerInspector { }
}
