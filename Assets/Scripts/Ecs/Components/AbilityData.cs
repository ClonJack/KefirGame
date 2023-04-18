namespace Asteroids.Components
{
    public struct AbilityData
    {
        public readonly float Speed;
        public readonly float Damage;
        public readonly float AmmoLifeTime;
        public AbilityData(float speed, float damage, float ammoLifeTime)
        {
            Speed = speed;
            Damage = damage;
            AmmoLifeTime = ammoLifeTime;
        }
    }
}