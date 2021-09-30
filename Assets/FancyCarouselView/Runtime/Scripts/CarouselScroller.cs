using FancyScrollView;
using UnityEditor;
using UnityEngine;

namespace FancyCarouselView.Runtime.Scripts
{
    [DisallowMultipleComponent]
    public sealed class CarouselScroller : Scroller
    {
#if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();

            // Set the initial values when this component is attached or reset.
            SetInitialValues();
        }

        private void SetInitialValues()
        {
            var so = new SerializedObject(this);
            so.Update();

            so.FindProperty("scrollDirection").intValue = (int)ScrollDirection.Horizontal;
            Draggable = true;

            so.ApplyModifiedProperties();
            so.Dispose();
        }
#endif
    }
}