using Interface;
using Drawable;
using Enum;

namespace Enemies
{
    public abstract class Enemy : DrawableObject, IKillable
    {
        protected IMoveStrategy _movePattern; 
        public int CollisionDamage{get; protected set;} = 10;
        public int Health{get; protected set;} = 1;
        public EnemyType Type{get; init;}
        public Enemy(Position position, IMoveStrategy movePattern, EnemyType type) 
        {
            _position = position;
            _movePattern = movePattern;
            Type = type;
        }
        public void LoseHealth(int damage) => Health -= damage;
        public virtual void Update()
        {
            _position = _movePattern.Move(_position);
        }
    }
}

