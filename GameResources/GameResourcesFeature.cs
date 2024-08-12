namespace Game.Ecs.GameResources
{
    using Code.DataBase.Runtime.Abstract;
    using Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Systems;
    using UniGame.Context.Runtime.Extension;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Resources/Game Resources Feature", fileName = "Game Resources Feature")]
    public class GameResourcesFeature : BaseLeoEcsFeature
    {
        public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            var context = ecsSystems.GetShared<IContext>();
            var dataBase = await context.ReceiveFirstAsync<IGameDatabase>();
            
            var world = ecsSystems.GetWorld();
            world.SetGlobal(dataBase);

            var spawnTools = new GameSpawnTools();
            world.SetGlobal(spawnTools);
            ecsSystems.Add(spawnTools);
            
            //remove event when it complete
            ecsSystems.DelHere<GameResourceSpawnCompleteEvent>();
            
            //kill GameResourceTaskCompleteEvent event
            ecsSystems.Add(new DestroyCompletedResourceTaskSystem());
            
            //get spawn request and create resource loading task
            ecsSystems.Add(new ProcessSpawnRequestSystem());
            
            //load addressable resource by id add resource task component
            //when resource loaded add GameResourceResultComponent to entity
            ecsSystems.Add(new ProceedGameResourceRequestSystem());
            
            //if gameobject contains navmeshagent fix spawn position
            ecsSystems.Add(new FixNavMeshResourcePositionSpawnSystem());
            
            //create spawn object by GameResourceResultComponent
            ecsSystems.Add(new CreateSpawnObjectSystem());
            //handle non gameobject assets
            ecsSystems.Add(new ApplyPawnGameObjectSystem());
            
            //mark resource as loaded
            ecsSystems.Add(new CompleteGameResourceObjectSystem());
            
            //fire GameResourceTaskCompleteEvent when GameResourceResultComponent added
            ecsSystems.Add(new MarkResourceTaskAsCompleteSystem());

            //delete all processed spawn requests
            ecsSystems.DelHere<GameResourceSpawnRequest>();
        }
    }
}