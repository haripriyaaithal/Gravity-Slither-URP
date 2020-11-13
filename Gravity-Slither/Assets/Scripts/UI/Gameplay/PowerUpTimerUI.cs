using System;
using GS.Audio;
using GS.Common;
using GS.UI.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GS.UI {
    public class PowerUpTimerUI : PanelBase {
        [SerializeField] private Image _fillImage;
        [SerializeField] private TextMeshProUGUI _messageText;

        private int _tweenId;

        public void Initialize(int seconds) {
            LeanTween.cancel(_tweenId);
            _fillImage.fillAmount = 1f;
            UpdateText(seconds);
            LeanTween.value(gameObject, (time) => {
                    _fillImage.fillAmount = time / seconds;
                    UpdateText((int) time);
                }, seconds, 0, seconds)
                .setOnComplete(() => PanelStacker.RemovePanel(this));
        }

        private void UpdateText(int seconds) {
            _messageText.text = $"Invisibility ends in {seconds} seconds.";
        }
    }
}