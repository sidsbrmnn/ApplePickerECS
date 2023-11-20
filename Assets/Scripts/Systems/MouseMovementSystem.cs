using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial struct MouseMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var mousePosition2D = Input.mousePosition;
            mousePosition2D.z = -Camera.main.transform.position.z;

            var mousePosition3D = Camera.main.ScreenToWorldPoint(mousePosition2D);

            foreach (var transform in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<MoveWithMouse>())
            {
                var pos = transform.ValueRO.Position;
                pos.x = mousePosition3D.x;
                transform.ValueRW.Position = pos;
            }
        }
    }
}
