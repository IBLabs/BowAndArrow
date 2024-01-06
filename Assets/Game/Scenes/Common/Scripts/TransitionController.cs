using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scenes.Common.Scripts
{
    public class TransitionController : MonoBehaviour
    {
        [SerializeField] private Image fadeImage;

        [SerializeField] private float transitionDuration = 0.3f;

        private bool isVisible = true;

        private void OnEnable()
        {
            StartEnterTransition();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isVisible)
                    StartEnterTransition();
                else
                    StartExitTransition();
            }
        }

        public void StartExitTransition()
        {
            fadeImage.DOFade(1, transitionDuration);
            isVisible = true;
        }

        public void StartEnterTransition()
        {
            fadeImage.DOFade(0, transitionDuration);
            isVisible = false;
        }
    }
}