using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace Jobs
{
    public struct CollisionJob : ICollisionEventsJob
    {
        [ReadOnly] public ComponentLookup<AppleTag> AppleData;
        [ReadOnly] public ComponentLookup<BasketTag> BasketData;

        public EntityCommandBuffer ECB;

        public void Execute(CollisionEvent collisionEvent)
        {
            var entityA = collisionEvent.EntityA;
            var entityB = collisionEvent.EntityB;

            if (AppleData.HasComponent(entityA) && BasketData.HasComponent(entityB))
            {
               ECB.DestroyEntity(entityA);
            }
            else if (AppleData.HasComponent(entityB) && BasketData.HasComponent(entityA))
            {
                ECB.DestroyEntity(entityB);
            }
        }
    }
}
