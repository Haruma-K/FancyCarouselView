using System;
using FancyCarouselView.Runtime.Scripts;
using FancyScrollView;
using UnityEditor;
using UnityEngine;

namespace FancyCarouselView.Editor.Scripts
{
    /// <summary>
    ///     Custom editor for the CarouselView.
    /// </summary>
    [CustomEditor(typeof(CarouselView<,>), true)]
    public class CarouselViewEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var targetBehaviour = (MonoBehaviour)target;
            var carouselScroller = targetBehaviour.gameObject.GetComponent<CarouselScroller>();
            serializedObject.Update();
            var carouselScrollerSo = new SerializedObject(carouselScroller);

            var offsetProp = serializedObject.FindProperty("scrollOffset");
            var loopProp = serializedObject.FindProperty("loop");
            var cellContainerProp = serializedObject.FindProperty("cellContainer");
            var scrollerProp = serializedObject.FindProperty("_scroller");
            var cellPrefabProp = serializedObject.FindProperty("_cellPrefab");
            var cellSizeProp = serializedObject.FindProperty("_cellSize");
            var cellSpacingProp = serializedObject.FindProperty("_cellSpacing");
            var snapAnimationDurationProp = serializedObject.FindProperty("_snapAnimationDuration");
            var snapAnimationTypeProp = serializedObject.FindProperty("_snapAnimationType");
            var autoScrollingProp = serializedObject.FindProperty("_autoScrollingEnabled");
            var autoScrollingIntervalSecProp = serializedObject.FindProperty("_autoScrollingIntervalSec");
            var inverseAutoScrollingDirectionProp = serializedObject.FindProperty("_inverseAutoScrollingDirection");
            var progressView = serializedObject.FindProperty("_progressView");
            var progressViewInteraction = serializedObject.FindProperty("_progressViewInteraction");
            var csMovementTypeProp = carouselScrollerSo.FindProperty("movementType");
            var csScrollDirectionProp = carouselScrollerSo.FindProperty("scrollDirection");
            var csDraggableProp = carouselScrollerSo.FindProperty("draggable");

            // NOTE: CellInterval is calculated at runtime, so hide it from the Inspector.

            offsetProp.floatValue = 0.5f;
            scrollerProp.objectReferenceValue = carouselScroller;

            EditorGUILayout.PropertyField(cellContainerProp);
            EditorGUILayout.PropertyField(cellPrefabProp);
            EditorGUILayout.PropertyField(cellSizeProp);
            EditorGUILayout.PropertyField(cellSpacingProp);
            EditorGUILayout.PropertyField(snapAnimationDurationProp);
            EditorGUILayout.PropertyField(snapAnimationTypeProp);
            EditorGUILayout.PropertyField(autoScrollingProp, new GUIContent("Auto Scrolling"));
            if (autoScrollingProp.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(autoScrollingIntervalSecProp, new GUIContent("Interval"));
                EditorGUILayout.PropertyField(inverseAutoScrollingDirectionProp, new GUIContent("Inverse Direction"));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(csScrollDirectionProp);

            EditorGUILayout.PropertyField(loopProp);
            var movementType = (MovementType)csMovementTypeProp.intValue;
            if (loopProp.boolValue)
            {
                // If loop is enabled, force the movementType to Unrestricted. 
                csMovementTypeProp.intValue = (int)MovementType.Unrestricted;
            }
            else
            {
                if (movementType == MovementType.Unrestricted)
                {
                    // If loop is disabled and movementType is Unrestricted, force the movementType to Elastic for now.
                    csMovementTypeProp.intValue = (int)MovementType.Elastic;
                    movementType = (MovementType)csMovementTypeProp.intValue;
                }

                using (new EditorGUI.IndentLevelScope())
                {
                    var endMovementType = (EndMovementType)movementType;
                    csMovementTypeProp.intValue =
                        Convert.ToInt32(EditorGUILayout.EnumPopup("Movement Type", endMovementType));
                }
            }

            EditorGUILayout.PropertyField(csDraggableProp);
            EditorGUILayout.PropertyField(progressView);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(progressViewInteraction, new GUIContent("Clickable"));
            }

            carouselScrollerSo.ApplyModifiedProperties();
            carouselScrollerSo.Dispose();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
