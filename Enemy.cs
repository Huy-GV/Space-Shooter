using System.Collections.Generic;
using SplashKitSDK;

namespace SpaceShooter
{
    public abstract class Enemy : DrawableObject, IShootableObject
    {
        protected IMoveStrategy _movePattern; 
        public int CollisionDamage{get; protected set;} = 10;
        public int Health{get; protected set;} = 1;
        public void LoseHealth(int damage) => Health -= damage;
        public virtual void Update()
        {
            _position = _movePattern.Move(_position);
        }
        public Enemy(Position position, IMoveStrategy movePattern) 
        {
            _position = position;
            _movePattern = movePattern;
        }
    }
}

