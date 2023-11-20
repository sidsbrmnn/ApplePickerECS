using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct BasketSpawnerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var spawner in SystemAPI.Query<RefRW<BasketSpawner>>())
            {
                if (!spawner.ValueRO.ShouldSpawn) continue;

                for (var i = 0; i < spawner.ValueRO.Count; i++)
                {
                    var basket = ecb.Instantiate(spawner.ValueRO.Prefab);
                    ecb.AddComponent<BasketTag>(basket);
                    ecb.AddComponent<MoveWithMouse>(basket);
                    ecb.SetComponent(basket, LocalTransform.FromPosition(new float3
                    {
                        y = spawner.ValueRO.BottomY + (spawner.ValueRO.Spacing * i)
                    }));
                }

                spawner.ValueRW.ShouldSpawn = false;
            }
        }
    }
}
