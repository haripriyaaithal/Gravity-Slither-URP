using System.Collections;
using GS.UI.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GS.Common {
    public class SceneLoadManager : MonoBehaviour {

        private WaitForSeconds _waitTime = new WaitForSeconds(GlobalConstants.MinSceneLoadTime);

        private void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnLoaded;
        }

        private void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnLoaded; 
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            var v_loadingPanel = UIFactory.Get<LoadingUIPanel>();
            if (v_loadingPanel != null) {
                v_loadingPanel.UpdateProgress(0f);
                PanelStacker.RemovePanel(v_loadingPanel);
            }
            PanelStacker.Clear();
            UIFactory.Clear();
            ManagerFactory.Clear();
            EventManager.GetInstance().OnSceneLoaded(scene);
        }

        private void OnSceneUnLoaded(Scene scene) {
            EventManager.GetInstance().OnSceneUnLoaded(scene);
        }

        public void LoadSceneAsync(string sceneName) {
            PanelStacker.Clear();
            PanelStacker.AddPanel(UIFactory.Get<LoadingUIPanel>());
            StartCoroutine(AsyncLoadScene(sceneName));
        }

        private IEnumerator AsyncLoadScene(string sceneName) {
            yield return _waitTime;
            var v_asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            v_asyncOperation.allowSceneActivation = false;
            var v_loadingPanel = UIFactory.Get<LoadingUIPanel>();
            var v_progress = 0f;
            while (!v_asyncOperation.isDone && v_progress < 1f) {
                v_loadingPanel.UpdateProgress(v_progress);
                v_progress += Time.unscaledDeltaTime;
                if (v_progress >= 1f) {
                    v_asyncOperation.allowSceneActivation = true;
                }
                yield return null;
            }
        }

    }
}