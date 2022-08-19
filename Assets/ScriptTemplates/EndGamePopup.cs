using System;
using System.Collections;
using UnityEngine;

namespace JackSParrot.UI
{
    public class EndGamePopup : PopupView
    {
        public class Config : IPopupConfig
        {
            public string PrefabAddress => "EndGamePopup";
            public Action OnClosed;
        }

        [SerializeField]
        private GameObject _buttonNext = null;

        private Config _config;

        public override void Initialize(IPopupConfig config)
        {
            base.Initialize(config);
            _config = (Config)config;
        }

        public override void Show(bool animated = true, Action onFinish = null)
        {
            base.Show(animated, onFinish);
            StartCoroutine(AnimationCoroutine());
        }

        IEnumerator AnimationCoroutine()
        {
            _buttonNext.SetActive(false);
            yield return new WaitForSeconds(1f);
            _buttonNext.SetActive(true);
        }

        public void OnClicked()
        {
            Close(_config.OnClosed);
        }
    }
}