using System;
using System.Collections;
using EasingCore;
using UnityEngine;
using UnityEngine.Assertions;

namespace FancyCarouselView.Runtime.Scripts
{
    internal static class AnimationUtils
    {
        public static IEnumerator CreateFloatAnimationRoutine(float from, float to, float duration, Ease easeType,
            Action<float> onValueChanged, Action onComplete)
        {
            Assert.IsTrue(duration > 0);

            var time = 0.0f;

            while (true)
            {
                time = Mathf.Clamp(time, 0.0f, duration);
                var progress = time / duration;
                progress = Easing.Get(easeType).Invoke(progress);
                var value = Mathf.Lerp(from, to, progress);
                onValueChanged?.Invoke(value);
                if (time >= duration)
                {
                    break;
                }

                yield return null;
                time += Time.deltaTime;
            }

            onComplete?.Invoke();
        }
    }
}