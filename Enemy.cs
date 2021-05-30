using System.Collections.Generic;
using SplashKitSDK;

namespace SpaceShooter{
    public abstract class Enemy : GameObject, IShootableObject
    {
        protected MovePattern _movePattern; 
        public int CollisionDamage{get; protected set;}
        public int Health{get; protected set;}
        public Image Image{ get; protected init;}
        protected int Angle{get; set;}
        public Enemy() : base()
        {
            Health = 1;
            CollisionDamage = 10;
            Angle = 90;
            
        }
        public void LoseHealth(int damage) => Health -= damage;
        public virtual void Update()
        {
            _position = _movePattern.Update();
        }
        public virtual void Draw()
        {
            Image.Draw(X, Y);
        }
    }
}

