using GS.Gameplay.Spawner;

namespace GS.Common {
    public class EventManager {
        private static EventManager _instance;

        private EventManager() { }

        public static EventManager GetInstance() {
            if (_instance == null) {
                _instance = new EventManager();
            }

            return _instance;
        }

        public delegate void EatFoodEventHandler(Food food);

        public event EatFoodEventHandler onEatFood;

        public void OnEatFood(Food food) {
            onEatFood?.Invoke(food);
        }

        public delegate void HitRockEventHandler();

        public event HitRockEventHandler onHitRock;

        public void OnHitRock() {
            onHitRock?.Invoke();
        }

        public delegate void GameStart();

        public event GameStart onGameStart;

        public void OnGameStart() {
            onGameStart?.Invoke();
        }

        public delegate void GameOver();

        public event GameOver onGameOver;

        public void OnGameOver() {
            Vibration.Vibrate(GlobalConstants.GameOverVibrateDuration);
            onGameOver?.Invoke();
        }

        public delegate void ShakeCamera();

        public event ShakeCamera onShakeCamera;

        public void OnShakeCamera() {
            onShakeCamera?.Invoke();
        }
    }
}