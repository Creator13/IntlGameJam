using System;
using TMPro;
using UnityEngine;

namespace Simfluencer.Model {
    public class TutorialElement : MonoBehaviour {
        [SerializeField, TextArea] private string tutorialText;
        [SerializeField] private GameObject targetObject;

        private TextMeshProUGUI text;
        private TextMeshProUGUI Text => text ? text : text = GetComponentInChildren<TextMeshProUGUI>();

        private void Awake() {
            Text.text = tutorialText;
        }

        private void Start() {
            var targetPos = targetObject ? targetObject.transform.position : Vector3.zero;
            transform.position = targetPos;
        }

        private void OnValidate() {
            Text.text = tutorialText;
        }
    }
}
