using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace cvanbattum.Audio {
    [RequireComponent(typeof(Button), typeof(EventTrigger))]
    public class ButtonSoundEffect : SoundEffectPlayer {
        private void Awake() {
            var evtTrig = GetComponent<EventTrigger>();
            var entry = new EventTrigger.Entry {eventID = EventTriggerType.PointerDown};
            entry.callback.AddListener((data) => {
                PlaySound();
            });
            evtTrig.triggers.Add(entry);
        }
    }
}
