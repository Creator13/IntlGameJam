using Simfluencer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace cvanbattum.Audio {
    [RequireComponent(typeof(Button), typeof(EventTrigger))]
    public class ButtonSoundEffect : SoundEffectPlayer {
        private Button button;
        
        private void Awake() {
            button = GetComponent<Button>();
            var evtTrig = GetComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) => {
                // var ptrData = data as PointerEventData;
                SoundManager.Instance.PlayEffect(soundName);
            });
            evtTrig.triggers.Add(entry);
        }
    }
}
