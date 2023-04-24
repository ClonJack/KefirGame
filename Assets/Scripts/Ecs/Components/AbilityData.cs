namespace Asteroids.Components
{
    public struct AbilityData
    {
        public readonly float Speed;
        public readonly float Damage;
        public AbilityData(float speed, float damage)
        {
            Speed = speed;
            Damage = damage;
        }
    }
}