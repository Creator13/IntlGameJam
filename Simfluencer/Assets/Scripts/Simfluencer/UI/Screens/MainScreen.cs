using cvanbattum.Audio;

namespace Simfluencer.UI.Screens {
    public class MainScreen : Screen {
        protected override void Show() {
            SoundManager.Instance.PlayMusic();
        }
    }
}
