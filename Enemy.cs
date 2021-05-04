using System.Collections.Generic;
namespace Space_Shooter{
    public abstract class Enemy : GameObject
    {
        protected MovePattern _movePattern;
        public Explosion.Type ExplosionType{get; protected set;}  
        public int CollisionDamage{get; protected set;}
        public int Health{get; protected set;}
        public Image Image{ get; protected set;}
        protected int Angle{get; set;}
        public Enemy()
        {
            Health = 1;
            CollisionDamage = 10;
            Angle = 90;
        }
        public void LoseHealth(int damage) => Health -= damage;
        public virtual void CheckPlayerBullets(List<Bullet> bullets)
        {
            foreach(var bullet in bullets.ToArray())
            {
                if (bullet.HitTarget(Image, X, Y))
                {
                    Health -= bullet.Damage;
                    bullets.Remove(bullet);
                }
            }
        }
        protected void Move()
        {
            _movePattern.Update();
            Y = _movePattern.UpdatedY;
            X = _movePattern.UpdatedX;
        }
    }
}

