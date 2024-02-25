namespace Game.Ecs.Ability.SubFeatures.Target.Abstract
{
	using Leopotam.EcsLite;

	public interface IZoneTargetsDetector
	{
		int GetTargetsInZone(EcsWorld world,int[] result, int entity, int[] targetEntities,int amount);
	}
}