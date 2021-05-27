using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class EnemyQuantity
    {
        private Dictionary<Type, int> _enemyAmountByType;   
        public EnemyQuantity()
        {
            _enemyAmountByType = new Dictionary<Type, int>()
            {
                {typeof(BlueAlienship), 0},
                {typeof(PurpleAlienship), 0},
                {typeof(RedAlienship), 0},
                {typeof(KamikazeAlien), 0},
                {typeof(Asteroid), 0},
                {typeof(Spacemine), 0},
                {typeof(Nightmare), 1},
                {typeof(Phantom), 1}
            };
        }
        public void UpdateQuantity(Type type, int increment ) => _enemyAmountByType[type] += increment;
    }
}