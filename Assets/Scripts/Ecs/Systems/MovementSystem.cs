﻿using Asteroids.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveAction>> _filter = default;

        private readonly EcsPoolInject<MoveLocalAction> _moveLocalActionPool = default;
        private readonly EcsPoolInject<MovementData> _movementDataPool = default;
        private readonly EcsPoolInject<DirectionData> _directionDataPool = default;
        private readonly EcsPoolInject<ComponentRef<Rigidbody2D>> _rigiBodyRefPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var movementData = ref _movementDataPool.Value.Get(entity);
                ref var directionData = ref _directionDataPool.Value.Get(entity);
                ref var rigidbodyRef = ref _rigiBodyRefPool.Value.Get(entity);

                var targetMove = directionData.Direction * (movementData.MoveSpeed * Time.fixedDeltaTime);

                var direction = targetMove;

                if (_moveLocalActionPool.Value.Has(entity))
                    direction = rigidbodyRef.Component.transform.TransformDirection(targetMove);
                
                rigidbodyRef.Component.AddForce(direction);
            }
        }
    }
}