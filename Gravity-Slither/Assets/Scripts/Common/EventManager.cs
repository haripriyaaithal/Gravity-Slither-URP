using GS.Gameplay.Spawner;

namespace GS.Common {
    public static class EventManager {
        public delegate void EatFoodEventHandler(Food food);

        public static event EatFoodEventHandler onEatFood;

        public static void OnEatFood(Food food) {
            onEatFood?.Invoke(food);
        }
    }
}