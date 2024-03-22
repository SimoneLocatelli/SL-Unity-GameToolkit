using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;
using Object = UnityEngine.Object;

namespace EditorScripts
{
    /// <summary>
    /// Makes the Animation preview in Unity autoplay the currently selected animation (a feature that should already be part of Unity).
    /// The code was taken from here: https://github.com/KuroiRoy/UnityAnimationPreviewAutoplay/tree/main
    /// See also https://forum.unity.com/threads/add-autoplay-option-on-animation-preview.617071/#post-6818996.
    /// </summary>
    [InitializeOnLoad]
    public static class AnimationPreviewPlayer
    {
        /// <summary>
        /// Set to false to disable it. Note that the Update method will still be called, but it will exit immediately.
        /// </summary>
        public static bool Enabled = true;

        private const BindingFlags PRIVATE_FIELD_BINDING_FLAGS = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField;
        private const BindingFlags PUBLIC_FIELD_BINDING_FLAGS = BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField;
        private const BindingFlags PUBLIC_PROPERTY_BINDING_FLAGS = BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty;

        private static readonly Type animationClipEditorType;
        private static readonly Type avatarPreviewType;
        private static readonly Type timeControlType;

        private static Object selectedObject;
        private static bool shouldFindTimeControl;

        static AnimationPreviewPlayer()
        {
            animationClipEditorType = Type.GetType("UnityEditor.AnimationClipEditor,UnityEditor");
            avatarPreviewType = Type.GetType("UnityEditor.AvatarPreview,UnityEditor");
            timeControlType = Type.GetType("UnityEditor.TimeControl,UnityEditor");
        }

        private static void Update()
        {
            if (!Enabled)
                return;

            if (HasActiveObjectChanged())
            {
                UpdateSelectedObject();
                return;
            }

            if (shouldFindTimeControl)
            {
                object timeControl = null;
                var playingProperty = GetPlayingProperty(ref timeControl);

                if (timeControl == null || playingProperty == null)
                    return;

                playingProperty.SetValue(timeControl, true);
            }
        }

        private static PropertyInfo GetPlayingProperty(ref object timeControl)
        {
            timeControl = null;
            var animationClipEditor = Resources.FindObjectsOfTypeAll(animationClipEditorType).FirstOrDefault();
            if (animationClipEditor == null)
                return null;

            var avatarPreview = animationClipEditorType.GetField("m_AvatarPreview", PRIVATE_FIELD_BINDING_FLAGS)?.GetValue(animationClipEditor);
            if (avatarPreview == null)
                return null;

            timeControl = avatarPreviewType.GetField("timeControl", PUBLIC_FIELD_BINDING_FLAGS)?.GetValue(avatarPreview);
            if (timeControl == null)
                return null;

            shouldFindTimeControl = false;

            var playingProperty = timeControlType.GetProperty("playing", PUBLIC_PROPERTY_BINDING_FLAGS);

            return playingProperty;
        }

        private static bool HasActiveObjectChanged()
            => Selection.activeObject != selectedObject;

        private static void UpdateSelectedObject()
        {
            selectedObject = Selection.activeObject;

            if (selectedObject is AnimationClip)
            {
                shouldFindTimeControl = true;
                return;
            }

            if (selectedObject is GameObject)
            {
                var assetPath = AssetDatabase.GetAssetPath(selectedObject);

                if (string.IsNullOrWhiteSpace(assetPath))
                    return;

                if (AssetDatabase.LoadAllAssetsAtPath(assetPath).Any(c => c is AnimationClip))
                    shouldFindTimeControl = true;
            }
        }

        [InitializeOnLoadMethod, UsedImplicitly]
        private static void SubscribeToUpdate()
            => EditorApplication.update += Update;
    }
}