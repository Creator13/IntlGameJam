using System;
using cvanbattum.Audio;
using Simfluencer.Model;

namespace Simfluencer {
    public class AudioController {
        private readonly GameStateManager manager;

        public AudioController(GameStateManager manager) {
            this.manager = manager;

            manager.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(GameState state) {
            var sound = SoundManager.Instance;
            if (state is ScenarioState sState) {
                sound.OverrideMusic = sState.scenario.NeutralMusic;
            }
            else if (state is ScenarioLockState slState) {
                switch (manager.CurrentScenarioEndingPath) {
                    case ScenarioEnding.ConspiracyNegative:
                    case ScenarioEnding.ScienceNegative:
                        sound.OverrideMusic = slState.scenario.NegativeMusic;
                        break;
                    case ScenarioEnding.ConspiracyPositive:
                    case ScenarioEnding.SciencePositive:
                        sound.OverrideMusic = slState.scenario.PositiveMusic;
                        break;
                }
            }
            else {
                sound.OverrideMusic = null;
            }
        }
    }
}
