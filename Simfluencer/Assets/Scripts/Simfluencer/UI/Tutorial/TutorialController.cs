using System;
using System.Collections.Generic;
using Simfluencer.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI {
    [RequireComponent(typeof(Image))]
    public class TutorialController : MonoBehaviour {
        [SerializeField] private Color backgroundColor = new Color(0, 0, 0, .3f);
        [SerializeField] private List<TutorialElement> elements;

        public bool IsRunning { get; private set; }
        public bool Completed { get; private set; }

        private Image background;
        private Image Background => background ? background : background = GetComponent<Image>();

        private int currentTutorialIndex;

        private TutorialElement currentElement;

        private TutorialElement CurrentElement {
            get => currentElement;
            set {
                if (currentElement) currentElement.gameObject.SetActive(false);
                currentElement = value;
                if (currentElement) currentElement.gameObject.SetActive(true);
            }
        }

        private void Awake() {
            Background.color = backgroundColor;
        }

        private void OnValidate() {
            Background.color = backgroundColor;
        }

        private void Start() {
            elements.ForEach(e => e.gameObject.SetActive(false));
            currentTutorialIndex = 0;
            IsRunning = true;
            ShowCurrentElement();
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.anyKeyDown) {
                NextTutorial();
            }
            else if (Input.touchCount > 0) {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    NextTutorial();
                }
            }
        }

        private void ShowCurrentElement() {
            CurrentElement = elements[currentTutorialIndex];
        }

        private void NextTutorial() {
            currentTutorialIndex++;
            // Check if tutorial is finished (all elements visited)
            if (currentTutorialIndex == elements.Count) {
                Deactivate();
                return;
            }

            ShowCurrentElement();
        }

        private void Deactivate() {
            IsRunning = false;
            Completed = true;
            CurrentElement = null;
            gameObject.SetActive(false);
        }
    }
}
