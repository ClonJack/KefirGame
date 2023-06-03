using Asteroids.Components;
using Asteroids.ECS.Ecs.Extension;
using InputControl;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECS.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Player>> _filter = default;

        private readonly EcsPoolInject<DirectionData> _directionDataPool = default;
        private readonly EcsPoolInject<AttackAction> _attackActionPool = default;
        private readonly EcsPoolInject<ChangeAction> _ammoActionPool = default;
        private readonly EcsPoolInject<MoveAction> _moveActionPool = default;
        private readonly EcsPoolInject<MoveLocalAction> _moveLocalActionPool = default;
        private readonly EcsPoolInject<RotateAction> _rotateActionPool = default;

        private readonly EcsCustomInject<IInputService> _inputServicePool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var directionData = ref _directionDataPool.Value.Get(entity);
                directionData.Direction = _inputServicePool.Value.AxisInput.Axis();

                if (_inputServicePool.Value.YAxis.IsHold())
                {
                    _moveActionPool.Value.Replace(entity);
                    _moveLocalActionPool.Value.Replace(entity);
                }

                if (_inputServicePool.Value.XAxis.IsHold())
                    _rotateActionPool.Value.Replace(entity);

                if (_inputServicePool.Value.ChangeWeapon.IsPressed())
                    _ammoActionPool.Value.Add(entity);

                if (_inputServicePool.Value.Shot.IsHold())
                    _attackActionPool.Value.Add(entity);
            }
        }
    }
}