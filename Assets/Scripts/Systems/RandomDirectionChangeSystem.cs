using Components;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public partial struct RandomDirectionChangeSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (properties, random) in SystemAPI.Query<RefRW<AppleTreeProperties>, RefRW<Random>>())
            {
                if (random.ValueRW.Value.NextFloat() < properties.ValueRO.ChangeDirectionChance)
                {
                    properties.ValueRW.Speed = properties.ValueRO.Speed * -1;
                }
            }
        }
    }
}
