using System.Collections.Generic;
namespace SpaceShooter
{
    public interface IHaveGun
    {
        public bool CoolDownEnded{get;}
        public Bullet Shoot();
    }
}