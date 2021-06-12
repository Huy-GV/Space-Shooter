using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace SpaceShooter
{
    public abstract class GameMode
    {
        protected Dictionary<EnemyType, int> _limits = new Dictionary<EnemyType, int>()
            {
                {EnemyType.BlueAlienship, 0},
                {EnemyType.PurpleAlienship, 0},
                {EnemyType.RedAlienship, 0},
                {EnemyType.KamikazeAlien, 0},
                {EnemyType.Asteroid, 0},
                {EnemyType.Spacemine, 0},
            };
        protected readonly List<Enemy> _enemies = new List<Enemy>();
        private EnemyQuantityList _quantityList = new EnemyQuantityList();
    ///<summary>
    ///Prevents other classes from adding/ removing enemies but still allows foreach loops
    ///</summary>
        public IEnumerable<Enemy> Enemies 
        {
            get
            {
                foreach(var enemy in _enemies.ToArray()) yield return enemy;
            }
        }
        public int SpawnRate{get; protected set;}
        public bool GameEnded{get; protected set;} = false;
        public GameMode()
        {
            SpawnRate = 70;
        }
        private bool TimeToSpawn()=> SplashKit.Rnd(0, SpawnRate) == 0;
        public virtual void AddEnemies(int score)
        {
            int[] parameters;
            foreach (var enemyType in _quantityList.Type)
            {
                if (TimeToSpawn() && _quantityList.GetQuantity(enemyType) < _limits[enemyType])
                {
                    if (_enemies.Count == 0) parameters = new int[]{Global.Width, Global.Height};
                    else
                    {
                        var lastEnemy = _enemies[_enemies.Count - 1];
                        parameters = new int[]{lastEnemy.X, lastEnemy.Y};
                    } 
                    _enemies.Add(EnemyFactory.Create(enemyType, parameters));
                    _quantityList.UpdateQuantity(enemyType, 1);
                }
            }
        }
    ///<summary>
    ///The game always ends when the player is dead. Subclassed game modes can add extra conditions
    ///</summary>
        public virtual void CheckGameEnding(Player player)
        {
            if (player.Health <= 0) GameEnded = true;
        }
        public void RemoveEnemy(Enemy enemy)
        {
            _quantityList.UpdateQuantity(enemy.Type, -1);
            _enemies.Remove(enemy);
        }
    }
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
                    _limits[EnemyType.BlueAlienship] = 3;
                    _limits[EnemyType.RedAlienship] = 5;
                    _limits[EnemyType.Asteroid] = 4;
                    _limits[EnemyType.Spacemine] = 2;
                    break;
                case 4:
                    _limits[EnemyType.BlueAlienship] = 3;
                    _limits[EnemyType.PurpleAlienship] = 2;
                    _limits[EnemyType.RedAlienship] = 4;
                    _limits[EnemyType.KamikazeAlien] = 3;
                    _limits[EnemyType.Asteroid] = 5;
                    _limits[EnemyType.Spacemine] = 3; 
                    break;
                default: throw new NotImplementedException($"The level {_level} is not implemented");
            }
        }
    }
    public class SurvivalMode : GameMode
    {
        private int _stage;
        public SurvivalMode() : base()
        {
            _stage = 0;
            SetEnemyLimitsByStage();
        }
        private void SetEnemyLimitsByStage()
        {
            switch(_stage)
            {
                case 0:
                    SpawnRate = SplashKit.Rnd(0,80);                
                    _limits[EnemyType.BlueAlienship] = 3;
                    _limits[EnemyType.PurpleAlienship] = 2;
                    _limits[EnemyType.RedAlienship] = 1;
                    _limits[EnemyType.KamikazeAlien] = 0;
                    _limits[EnemyType.Asteroid] = 5;
                    _limits[EnemyType.Spacemine] = 1;
                    break;
                case 1:
                    SpawnRate = SplashKit.Rnd(0,70);
                    _limits[EnemyType.BlueAlienship] = 4;
                    _limits[EnemyType.PurpleAlienship] = 3;
                    _limits[EnemyType.RedAlienship] = 2;
                    _limits[EnemyType.KamikazeAlien] = 2;
                    _limits[EnemyType.Asteroid] = 6;
                    _limits[EnemyType.Spacemine] = 2;
                    break;
                case 2: 
                    SpawnRate = SplashKit.Rnd(0,65);
                    _limits[EnemyType.BlueAlienship] = 5;
                    _limits[EnemyType.PurpleAlienship] = 3;
                    _limits[EnemyType.RedAlienship] = 2;
                    _limits[EnemyType.KamikazeAlien] = 2;
                    _limits[EnemyType.Asteroid] = 7;
                    _limits[EnemyType.Spacemine] = 3;
                    break;
                default: throw new IndexOutOfRangeException($"The stage index exceeds 2");
            }
        }      
        public override void AddEnemies(int score)
        {
            if ((score > 10 && _stage == 0) || (score > 20 && _stage == 1))
            {
                _stage++;
                SetEnemyLimitsByStage();
            } 
            // Console.WriteLine(_stage);
            base.AddEnemies(score);
        } 
    }
    public class MineFieldMode : GameMode
    {
        public MineFieldMode(int spawnRate) : base()
        {
            _limits[EnemyType.Asteroid] = 10;
            _limits[EnemyType.Spacemine] = 6;
            SpawnRate = spawnRate;
        }
    }
    public class BossRunMode : GameMode
    {
        private int _stage = 0;
        private readonly int _stageAmount = 3;
        public BossRunMode() : base(){}
        public override void CheckGameEnding(Player player)
        {
            base.CheckGameEnding(player);
            if (_stage == _stageAmount && _enemies.Count == 0) GameEnded = true;
        }
        public override void AddEnemies(int score)
        {
            if (_enemies.Count == 0)
            {
                switch(_stage)
                {
                    case 0:
                        _enemies.Add(EnemyFactory.Create(EnemyType.NightmareBoss, new int[]{}));   
                        _stage++;
                        break;
                    case 1:
                        _enemies.Add(EnemyFactory.Create(EnemyType.PhantomBoss, new int[]{}));
                        _stage++;
                        break;
                    case 2:
                        _enemies.Add(EnemyFactory.Create(EnemyType.PhantomBoss, new int[]{}));
                        _enemies.Add(EnemyFactory.Create(EnemyType.NightmareBoss, new int[]{}));
                        _stage++;
                        break;
                }
            }
        }  
    }
}