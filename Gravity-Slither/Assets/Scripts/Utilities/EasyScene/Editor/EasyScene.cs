using GS.Common;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EasyScene : MonoBehaviour {
    [MenuItem("", menuItem = "Scene/Init")]
    public static void InitScene() {
        EditorSceneManager.OpenScene($"Assets/Scenes/{GlobalConstants.InitScene}.unity");
    }

    [MenuItem("", menuItem = "Scene/Gameplay")]
    public static void MenuScene() {
        EditorSceneManager.OpenScene($"Assets/Scenes/{GlobalConstants.GameplayScene}.unity");
    }
}