using FancyCarouselView.Runtime.Scripts;
using UnityEditor;
using UnityEngine;

namespace FancyCarouselView.Editor.Scripts
{
    /// <summary>
    ///     Custom editor for CarouselScroller
    /// </summary>
    [CustomEditor(typeof(CarouselScroller))]
    public class CarouselScrollerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var carouselScroller = (CarouselScroller)target;
            serializedObject.Update();

            var viewportProp = serializedObject.FindProperty("viewport");
            var scrollSensitivityProp = serializedObject.FindProperty("scrollSensitivity");
            var inertiaProp = serializedObject.FindProperty("inertia");
            var scrollbarProp = serializedObject.FindProperty("scrollbar");
            var snapEnableProp = serializedObject.FindProperty("snap.Enable");

            // NOTE: The following parameters are controlled by CarouselViewEditor, so not do anything here.
            // scrollDirection, movementType, elasticity, draggable

            if (viewportProp.objectReferenceValue == null)
            {
                viewportProp.objectReferenceValue = (RectTransform)carouselScroller.transform;
            }

            inertiaProp.boolValue = false;
            snapEnableProp.boolValue = false;
            scrollSensitivityProp.floatValue = 1.0f;
            scrollbarProp.objectReferenceValue = null;

            serializedObject.ApplyModifiedProperties();
        }
    }
}