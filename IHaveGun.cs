using System.Collections.Generic;
namespace Space_Shooter
{
    public interface IHaveGun
    {
        public List<Bullet> Bullets{get;}
        public int Damage{get;}
    }
}