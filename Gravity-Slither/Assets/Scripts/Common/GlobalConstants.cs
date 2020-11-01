using UnityEngine;

namespace GS.Common {
    public static class GlobalConstants {
        #region Scenes

        public static string InitScene = "InitScene";
        public static string MenuScene = "MainMenu";
        public static string GameplayScene = "Gameplay";
        public static int MinSceneLoadTime = 1;

        #endregion

        #region Tags

        public static string World = "World";
        public static string Food = "Food";
        public static string Player = "Player";
        public static string Tree = "Tree";
        public static string SnakeBody = "SnakeBody";

        #endregion

        #region Pool max capacity

        public static int FoodMaxCapacity = 5;
        public static int PlayerBodyMaxCapacity = 15;
        public static int RockMaxCapacity = 4;

        #endregion

        #region Shader properties

        public static readonly int Tint = Shader.PropertyToID("_Tint");

        #endregion

        #region Power Up

        public static int InvisiblePowerUpTime = 10;
        public static int RevivePowerUpTime = 5;
        public static int FoodCountForPowerUp = 10;

        #endregion

        #region Settings

        public enum ControlType {
            Touch = 1,
            OnScreen = 2
        }

        public static float DefaultMusicVolume = 4f;
        public static float DefaultSoundVolume = 10f;

        #endregion

        public static int ScoreMultiplier = 10;
        public static int GameOverVibrateDuration = 150;
        public static int FoodCountForSpeedIncrease = 3;
    }
}