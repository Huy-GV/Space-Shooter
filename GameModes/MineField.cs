
using Enum;

namespace GameModes
{
    public class MineFieldMode : GameMode
    {
        public MineFieldMode(int spawnRate) : base()
        {
            _limits[EnemyType.Asteroid] = 10;
            _limits[EnemyType.Spacemine] = 6;
            SpawnRate = spawnRate;
        }
    }
}