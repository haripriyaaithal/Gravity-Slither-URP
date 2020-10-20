using UnityEngine;
using UnityEngine.SceneManagement;

namespace GS.Common.Debug {
    public class DebugManager : MonoBehaviour {
        public void Restart() {
            ManagerFactory.Reset();
            SceneManager.LoadScene(GlobalConstants.GameplayScene);
        }
    }
}