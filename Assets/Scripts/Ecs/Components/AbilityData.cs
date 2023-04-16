namespace Asteroids.Components
{
    public struct AbilityData
    {
        public readonly float CoolDown;
        public readonly float Speed;
        public readonly float Damage;
        public readonly float AmmoLifeTime;
        public float Timer;

        public AbilityData(float coolDown, float speed, float damage, float timer, float ammoLifeTime)
        {
            CoolDown = coolDown;
            Speed = speed;
            Damage = damage;
            Timer = timer;
            AmmoLifeTime = ammoLifeTime;
        }
    }
}