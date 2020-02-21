using System;
using UnityEngine;

namespace Simfluencer {
    public class TimeManager {
        private readonly float timePerDay;
        private float timePassed;
        private float unityStartTime;

        private float currentDay;

        public bool Paused { get; set; } = false;

        private DateTime currentDate;

        public DateTime CurrentDate {
            get { throw new NotImplementedException(); }
        }

        public event Action DayChanged;

        public TimeManager(DateTime startDate, float timePerDay) {
            this.timePerDay = timePerDay;
            currentDate = startDate;
        }

        public void Start() {
            unityStartTime = Time.time;
            timePassed = 0;
            currentDay = 0;
        }

        public void Update() {
            if (Paused) return;

            timePassed += Time.deltaTime;
            currentDay += Time.deltaTime;

            if (currentDay > timePerDay) {
                DayChanged?.Invoke();
                currentDay = 0;
            }
        }
    }
}
