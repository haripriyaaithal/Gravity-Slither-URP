using GS.Gameplay.Spawner;

namespace GS.Common {
    public static class EventManager {
        public delegate void EatFoodEventHandler(Food food);

        public static event EatFoodEventHandler onEatFood;

        public static void OnEatFood(Food food) {
            onEatFood?.Invoke(food);
        }

        public delegate void GameStart();

        public static event GameStart onGameStart;

        public static void OnGameStart() {
            onGameStart?.Invoke();
        }
        
        public delegate void GameOver();

        public static event GameOver onGameOver;

        public static void OnGameOver() {
            onGameOver?.Invoke();
        }
        
    }
}