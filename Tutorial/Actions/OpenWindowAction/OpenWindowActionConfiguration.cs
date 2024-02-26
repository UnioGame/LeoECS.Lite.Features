namespace Game.Ecs.Gameplay.Tutorial.Actions.OpenWindowAction
{
	using System;
	using Abstracts;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Extensions;
	using UniGame.UiSystem.Runtime.Settings;
	using UniModules.UniGame.UiSystem.Runtime;
    
	public class OpenWindowActionConfiguration : TutorialAction
	{
		#region inspector
        
		public ViewId view;
		public ViewType layoutType = ViewType.Window;

		#endregion
		
		protected override void Composer(EcsWorld world, int entity)
		{
			ref var openWindowActionComponent = ref world.AddComponent<OpenWindowActionComponent>(entity);
			openWindowActionComponent.View = view;
            openWindowActionComponent.LayoutType = layoutType;
		}
	}
}