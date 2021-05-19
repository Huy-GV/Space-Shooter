using System.Collections.Generic;
//TODO: add an interface named IShootableObject that includes: image, x, y and losehealth
namespace SpaceShooter
{
    public interface IHaveGun
    {
        public bool CoolDownEnded{get;}
        public Bullet Shoot();
    }
    public interface IShootableObject
    {
        public Image Image{get;}
        public int X{get;}
        public int Y{get;}
        public abstract void LoseHealth(int damage);
    }
}