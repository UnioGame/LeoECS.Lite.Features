namespace Game.Code.Configuration.Editor.Animation
{
    using Animations;
    using JetBrains.Annotations;
    using Sirenix.OdinInspector;
    using Sirenix.OdinInspector.Editor;
    using UniGame.Shared.Runtime.Timeline;
    using UniModules.Editor;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Playables;
    using UnityEngine.Serialization;
    using UnityEngine.Timeline;

    [UsedImplicitly]
    public sealed class AnimationLinkWindow : OdinEditorWindow
    {
        [InlineEditor]
        public AnimationLink animationLink;
        
        [PropertySpace(8)]
        public PlayableDirector playableDirector;

        #region static data

        [InitializeOnLoadMethod]
        public static void OnLoadInitialization()
        {
            AnimationEditorData.OnEditorOpen -= ShowWindow;
            AnimationEditorData.OnEditorOpen += ShowWindow;
        }
        
        [MenuItem("Tools/Animation/Animation Link Tool")]
        public static void ShowWindow()
        {
            var window = GetWindow<AnimationLinkWindow>();
            window.titleContent = new GUIContent("Animation Link Tool");
            window.Show();
        }
        
        public static void ShowWindow(AnimationLink link)
        {
            var window = GetWindow<AnimationLinkWindow>();
            window.titleContent = new GUIContent("Animation Link Tool");
            window.animationLink = link;
            window.Show();
        }
    
        #endregion
        
        public bool DataExists => playableDirector != null && animationLink != null;

        [Button(ButtonSizes.Large,Icon = SdfIconType.Building)]
        [EnableIf(nameof(DataExists))]
        public void Bake()
        {
            AnimationTool.BakeAnimationLink(playableDirector, animationLink);
        }
        
        [Button(ButtonSizes.Large,Icon =SdfIconType.Command)]
        [EnableIf(nameof(DataExists))]
        public void ApplyToDirector()
        {
            AnimationTool.ApplyBindings(playableDirector, animationLink);
        }

        protected override void OnImGUI()
        {
            base.OnImGUI();
            
            if (playableDirector == null && Selection.activeGameObject != null)
            {
                var target = Selection.activeGameObject;
                var director = target.GetComponent<PlayableDirector>();
                playableDirector = director;
            }
        }
        
    }
}