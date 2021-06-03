using System.Collections.Generic;
using SplashKitSDK;

namespace SpaceShooter{
    public abstract class Enemy : DrawableObject, IShootableObject
    {
        protected MovePattern _movePattern; 
        public int CollisionDamage{get; protected set;} = 10;
        public int Health{get; protected set;} = 1;
        protected int Angle{get; set;} = 90;
        public void LoseHealth(int damage) => Health -= damage;
        public virtual void Update()
        {
            _position = _movePattern.Update();
        }
    }
}

