using UnityEngine;

namespace GS.Common {
    public static class Vibration {
#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject _currentActivity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject _vibrator = _currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
        private static AndroidJavaObject _vibrator;
#endif

        public static void Vibrate() {
            if (isAndroid()) {
                _vibrator.Call("vibrate");
            } else {
#if !UNITY_WEBGL
                Handheld.Vibrate();
#endif
            }
        }


        public static void Vibrate(long milliseconds) {
            if (isAndroid()) {
                _vibrator.Call("vibrate", milliseconds);
            } else {
#if !UNITY_WEBGL
                Handheld.Vibrate();
#endif
            }
        }

        public static void Vibrate(long[] pattern, int repeat) {
            if (isAndroid()) {
                _vibrator.Call("vibrate", pattern, repeat);
            } else {
#if !UNITY_WEBGL
                Handheld.Vibrate();
#endif
            }
        }

        public static bool HasVibrator() {
            return isAndroid();
        }

        public static void Cancel() {
            if (isAndroid()) {
                _vibrator.Call("cancel");
            }
        }

        private static bool isAndroid() {
#if UNITY_ANDROID && !UNITY_EDITOR
	        return true;
#else
            return false;
#endif
        }
    }
}