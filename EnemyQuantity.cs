using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class EnemyQuantityList
    {
        private Dictionary<Type, int> _quantityByType;   
        public IEnumerable<Type> Type
        {
            get
            {
                foreach(var key in _quantityByType.Keys) yield return key;
            }
        }
        public EnemyQuantityList()
        {
            _quantityByType = new Dictionary<Type, int>()
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
        public void UpdateQuantity(Type type, int increment ) => _quantityByType[type] += increment;
        public int GetQuantity(Type type) => _quantityByType[type];
    }
}