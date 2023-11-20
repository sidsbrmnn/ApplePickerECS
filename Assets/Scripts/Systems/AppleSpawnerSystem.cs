using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    public partial struct AppleSpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (transform, spawner, timer) in SystemAPI
                         .Query<RefRW<LocalTransform>, RefRO<AppleSpawner>, RefRW<Timer>>())
            {
                timer.ValueRW.Value = timer.ValueRO.Value - deltaTime;

                if (timer.ValueRO.Value > 0f) continue;

                var apple = ecb.Instantiate(spawner.ValueRO.Prefab);
                ecb.AddComponent<AppleTag>(apple);
                ecb.SetComponent(apple, LocalTransform.FromPosition(transform.ValueRO.Position));

                timer.ValueRW.Value = spawner.ValueRO.Delay;
            }
        }
    }
}
