using UnityEngine;

namespace Simfluencer {
    public static class SettingTools {
// #if !UNITY_EDITOR
//         static SettingTools() {
//             FitTargetResolution();
//         }
// #endif

        public static void FitTargetResolution() {
            var res = Screen.currentResolution;
            var targetRes = new Resolution {refreshRate = 60, width = 1080, height = 2200};

            var finalRes = targetRes;

            if (targetRes.height > res.height) {
                finalRes.height = res.height;
                finalRes.width = targetRes.width * finalRes.width / targetRes.height;
            }

            if (targetRes.width > res.width) {
                finalRes.width = res.width;
                finalRes.height = targetRes.height * finalRes.height / targetRes.width;
            }

            Screen.SetResolution(finalRes.width, finalRes.height, true);
        }
    }
}
