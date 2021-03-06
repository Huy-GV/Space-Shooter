using Enum;
using System.Collections.Generic;

namespace Gameplay
{
    public class EnemyQuantityList
    {
        private Dictionary<EnemyType, int> _quantityByType = new Dictionary<EnemyType, int>()
            {
                {EnemyType.Asteroid, 0},
                {EnemyType.Spacemine, 0},
                {EnemyType.BlueAlienship, 0},
                {EnemyType.RedAlienship, 0},
                {EnemyType.PurpleAlienship, 0},
                {EnemyType.KamikazeAlien, 0},
                {EnemyType.NightmareBoss, 0}
            }; 
        public IEnumerable<EnemyType> Type
        {
            get
            {
                foreach(var key in _quantityByType.Keys) yield return key;
            }
        }
        public void UpdateQuantity(EnemyType type, int increment ) => _quantityByType[type] += increment;
        public int GetQuantity(EnemyType type) => _quantityByType[type];
    }
}