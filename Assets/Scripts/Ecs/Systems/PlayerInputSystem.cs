using Asteroids.Components;
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

        private readonly EcsCustomInject<IInputService> _inputServicePool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var directionData = ref _directionDataPool.Value.Get(entity);
                directionData.Direction = _inputServicePool.Value.Axis;

                if (_inputServicePool.Value.IsChangeWeapon)
                    _ammoActionPool.Value.Add(entity);

                if (_inputServicePool.Value.IsShot)
                    _attackActionPool.Value.Add(entity);
            }
        }
    }
}