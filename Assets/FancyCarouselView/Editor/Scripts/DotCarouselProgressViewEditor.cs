using FancyCarouselView.Runtime.Scripts;
using UnityEditor;
using UnityEngine.UI;

namespace FancyCarouselView.Editor.Scripts
{
    [CustomEditor(typeof(DotCarouselProgressView))]
    public class DotCarouselProgressViewEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var dotCarouselProgressView = (DotCarouselProgressView)target;
            base.OnInspectorGUI();

            if (dotCarouselProgressView.GetComponent<HorizontalLayoutGroup>() == null &&
                dotCarouselProgressView.GetComponent<VerticalLayoutGroup>() == null)
            {
                var message =
                    $"{nameof(DotCarouselProgressView)} requires {nameof(HorizontalLayoutGroup)} or {nameof(VerticalLayoutGroup)}.";
                EditorGUILayout.HelpBox(message, MessageType.Error);
            }
        }
    }
}