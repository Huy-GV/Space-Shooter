using System.Collections.Generic;
namespace Space_Shooter{
    public abstract class Enemy : GameObject
    {
        protected MovePattern _movePattern;
        public Explosion.Type ExplosionType{get; protected set;}  
        public int CollisionDamage{get; protected set;}
        public bool IsDestroyed{get; protected set;}
        public void GetDestroyed() => IsDestroyed = true;
        public virtual void CheckPlayerBullets(List<Bullet> bullets)
        {
            foreach(var bullet in bullets.ToArray())
            {
                if (bullet.HitTarget(this))
                {
                    GetDestroyed();
                    bullets.Remove(bullet);
                }
            }
        }
        public Enemy()
        {
            IsDestroyed = false;
            CollisionDamage = 10;
        }
    }
}

