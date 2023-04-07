using Asteroids.ECS.Views.Classes.Ammo;

namespace Asteroids.Components
{
    public struct AmmoLifeTimeData
    {
        public float CoolDown;
        public float Timer;
        public AmmoView Ammo;
    }
}