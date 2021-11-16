using System;
using Enum;
using Gameplay;
using Main;


namespace GameModes
{
    public class ByLevelMode : GameMode
    {
        private int _level;
        private bool _bossSpawned;
        public ByLevelMode(int level) : base()
        {
            _level = level;
            _bossSpawned = false;
            SetEnemyLimitsByLevel();
        }
        public override void AddEnemies(int score)
        {
            if (score < 100 ) base.AddEnemies(score);
            else if (!_bossSpawned && _level > 3)
            {
                _bossSpawned = true;
                switch(_level)
                {
                    case 3: 
                        _enemies.Add(EnemyFactory.Create(EnemyType.NightmareBoss, new int[]{})); 
                        break;
                    case 4: 
                        _enemies.Add(EnemyFactory.Create(EnemyType.PhantomBoss, new int[]{})); 
                        break;
                    default: throw new IndexOutOfRangeException($"The level {_level} is not implemented or the threshold for levels with bosses is incorrect");
                }
            }
        }
        public override void CheckGameEnding(Player player)
        {
            base.CheckGameEnding(player);
            if (_enemies.Count == 0 && ((_level < 3 && player.Score >= 100) || (_level >= 3 && _bossSpawned)))
                GameEnded = true;
        }
        private void SetEnemyLimitsByLevel()
        {
            if (_level < 1) _level = 1;
            if (_level > 4) _level = 4;
            switch(_level)
            {
                case 1:
                    _limits[EnemyType.BlueAlienship] = 5;
                    _limits[EnemyType.RedAlienship] = 2;
                    _limits[EnemyType.Asteroid] = 5;
                    _limits[EnemyType.Spacemine] = 1;
                    break;
                case 2:
                    _limits[EnemyType.PurpleAlienship] = 6;
                    _limits[EnemyType.Asteroid] = 5;
                    _limits[EnemyType.Spacemine] = 2;
                    break;
                case 3:
                    _limits[EnemyType.BlueAlienship] = 2;
                    _limits[EnemyType.RedAlienship] = 4;
                    _limits[EnemyType.Asteroid] = 4;
                    _limits[EnemyType.Spacemine] = 2;
                    break;
                case 4:
                    _limits[EnemyType.BlueAlienship] = 2;
                    _limits[EnemyType.PurpleAlienship] = 2;
                    _limits[EnemyType.RedAlienship] = 4;
                    _limits[EnemyType.KamikazeAlien] = 3;
                    _limits[EnemyType.Asteroid] = 4;
                    _limits[EnemyType.Spacemine] = 3; 
                    break;
                default: throw new NotImplementedException($"The level {_level} is not implemented");
            }
        }
    }
}