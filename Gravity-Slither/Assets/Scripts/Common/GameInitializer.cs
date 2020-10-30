using UnityEngine;

namespace GS.Common {
    public class GameInitializer : MonoBehaviour {
        #region Unity event methods

        private void Awake() {
            Application.runInBackground = true;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void OnEnable() {
            EventManager.GetInstance().onSceneLoaded += OnSceneLoad;
        }

        #endregion

        private void OnSceneLoad(UnityEngine.SceneManagement.Scene scene) {
            if (scene.name.Equals(GlobalConstants.InitScene)) {
                DontDestroyOnLoad(this);
                var v_sceneLoadManager = ManagerFactory.Get<SceneLoadManager>();
                if (v_sceneLoadManager != null) {
                    v_sceneLoadManager.LoadSceneAsync(GlobalConstants.MenuScene);
                }

                EventManager.GetInstance().onSceneLoaded -= OnSceneLoad;
            }
        }
    }
}