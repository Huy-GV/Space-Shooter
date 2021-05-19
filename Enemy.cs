using System.Collections.Generic;
namespace SpaceShooter{
    public abstract class Enemy : IShootableObject
    {
        protected MovePattern _movePattern;
        public int X => _movePattern.X;
        public int Y => _movePattern.Y;
        public Explosion.Type ExplosionType{get; protected set;}  
        public int CollisionDamage{get; protected set;}
        public int Health{get; protected set;}
        public Image Image{ get; protected init;}
        protected int Angle{get; set;}
        public Enemy()
        {
            Health = 1;
            CollisionDamage = 10;
            Angle = 90;
        }
        public void LoseHealth(int damage) => Health -= damage;
        public virtual void Update()
        {
            _movePattern.Update();
        }
        public virtual void Draw()
        {
            Image.Draw(X, Y);
        }
    }
}

