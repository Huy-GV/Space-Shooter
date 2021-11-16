using Drawable;
using Interface;
using Weapon;
using Enum;

namespace Enemies
{
    public class Alienship : Enemy
    {
        protected Gun _gun;
        public bool OverheatEnded => _gun.OverheatEnded;
        public Alienship(Position position, Gun gun, IMoveStrategy movePattern, Image image, EnemyType type) : base(position, movePattern, type)
        {
            _gun = gun;
            Image = image;
        }
        public override void Update()
        {
            base.Update();
            _gun.Update();
        }
        public Bullet Shoot() => _gun.OpenFire(X, Y, Angle, 0); 
    }
}