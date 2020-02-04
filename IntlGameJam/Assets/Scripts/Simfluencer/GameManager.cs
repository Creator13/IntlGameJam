using System;
using UnityEngine;

namespace Simfluencer {
    public class GameManager : MonoBehaviour {
        private PlayerStats player;
        private void Awake() {
#if !UNITY_EDITOR
            SettingTools.FitTargetResolution();
#endif
        }

        private void Start() {
            player = new PlayerStats();

            new ArticlePool();
        }
    }
}
