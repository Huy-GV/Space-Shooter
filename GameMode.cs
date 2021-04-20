using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Space_Shooter
{
    public abstract class GameMode
    {
        public enum Modes
        {
            ByLevels,
            Survival,
            BossRun
        }
        public Dictionary<Type, int> Limits;
        public Modes Mode{get; private set;}
        protected Dictionary<Type, int> _enemyAmountByClass;
        public GameMode()
        {
            Limits = new Dictionary<Type, int>()
            {
                {typeof(BlueAlienship), 0},
                {typeof(PurpleAlienship), 0},
                {typeof(RedAlienship), 0},
                {typeof(KamikazeAlien), 0},
                {typeof(Asteroid), 0},
                {typeof(Spacemine), 0}
            };
            _enemyAmountByClass = new Dictionary<Type, int>();
            _enemyAmountByClass[typeof(Asteroid)] = 0;
            _enemyAmountByClass[typeof(Spacemine)] = 0;
            _enemyAmountByClass[typeof(BlueAlienship)] = 0;
            _enemyAmountByClass[typeof(PurpleAlienship)] = 0;
            _enemyAmountByClass[typeof(RedAlienship)] = 0;
            _enemyAmountByClass[typeof(KamikazeAlien)] = 0;
        }
        public virtual void AddEnemies(int score, List<Enemy> enemies)
        {
            int enemyTypeAmount;
            foreach (var enemyType in _enemyAmountByClass.Keys)
            {
                enemyTypeAmount = _enemyAmountByClass[enemyType];
                if (SplashKit.Rnd(0, 70) == 0 && enemyTypeAmount < Limits[enemyType])
                {
                    if (enemyTypeAmount == 0) enemies.Add((Enemy)Activator.CreateInstance(enemyType));
                    else
                    {
                        var lastEnemy = enemies[enemies.Count - 1];
                        Object[] parameters = {lastEnemy.X, lastEnemy.Y};
                        enemies.Add((Enemy)Activator.CreateInstance(enemyType, parameters));
                    }
                    UpdateEnemyAmount(enemyType, 1);
                }
            }
        }
        public void UpdateEnemyAmount(Type type, int increment) => _enemyAmountByClass[type] += increment;
    }
    public class ByLevelMode : GameMode
    {
        private int _level;
        private bool _bossSpawned;
        public ByLevelMode(int level) : base()
        {
            _level = level;
            Console.WriteLine("level is " + _level);
            _bossSpawned = false;
            SetEnemyLimitsByLevel();
        }
        public override void AddEnemies(int score, List<Enemy> enemies)
        {
            if (score < 10 ) base.AddEnemies(score, enemies);
            //TODO: modify so that the correct boss is added
            else if (enemies.Count == 0 && !_bossSpawned)
            {
                SpawnBoss(enemies);
                Console.WriteLine("enemy type is {0}", enemies[0].GetType());
                _bossSpawned = true;
            }
        }
        private void SpawnBoss(List<Enemy> enemies)
        {
            Console.WriteLine("Boss for this level is {0}", _level);
            switch(_level)
            {
                case 3: enemies.Add(new Nightmare()); break;
                case 4: enemies.Add(new Phantom()); break;
                default: break;
            }
        }
        private void SetEnemyLimitsByLevel()
        {
            switch(_level)
            {
                case 1:
                    Limits[typeof(BlueAlienship)] = 5;
                    Limits[typeof(RedAlienship)] = 2;
                    Limits[typeof(Asteroid)] = 5;
                    Limits[typeof(Spacemine)] = 1;
                    break;
                case 3:
                    Limits[typeof(BlueAlienship)] = 3;
                    Limits[typeof(RedAlienship)] = 5;
                    Limits[typeof(Asteroid)] = 4;
                    Limits[typeof(Spacemine)] = 2;
                    break;
                case 4:
                    Limits[typeof(BlueAlienship)] = 3;
                    Limits[typeof(PurpleAlienship)] = 2;
                    Limits[typeof(RedAlienship)] = 4;
                    Limits[typeof(KamikazeAlien)] = 3;
                    Limits[typeof(Asteroid)] = 5;
                    Limits[typeof(Spacemine)] = 3; 
                    break;
                default: //TODO: find a way to avoid default
                //level 2
                    Limits[typeof(PurpleAlienship)] = 6;
                    Limits[typeof(Asteroid)] = 5;
                    Limits[typeof(Spacemine)] = 2;
                    break;
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
                case 2: 
                    Limits[typeof(BlueAlienship)] = 5;
                    Limits[typeof(PurpleAlienship)] = 3;
                    Limits[typeof(RedAlienship)] = 2;
                    Limits[typeof(KamikazeAlien)] = 3;
                    Limits[typeof(Asteroid)] = 7;
                    Limits[typeof(Spacemine)] = 3;
                    break;
                case 0:
                    Limits[typeof(BlueAlienship)] = 3;
                    Limits[typeof(PurpleAlienship)] = 2;
                    Limits[typeof(RedAlienship)] = 1;
                    Limits[typeof(KamikazeAlien)] = 0;
                    Limits[typeof(Asteroid)] = 5;
                    Limits[typeof(Spacemine)] = 1;
                    break;
                default:
                    Limits[typeof(BlueAlienship)] = 4;
                    Limits[typeof(PurpleAlienship)] = 3;
                    Limits[typeof(RedAlienship)] = 2;
                    Limits[typeof(KamikazeAlien)] = 1;
                    Limits[typeof(Asteroid)] = 6;
                    Limits[typeof(Spacemine)] = 2;
                    break;
            }
        }      
        public override void AddEnemies(int score, List<Enemy> enemies)
        {
            if (score > 50 && _stage == 0) _stage++;
            else if (score > 100 && _stage == 1) _stage++;
            base.AddEnemies(score, enemies);
        } 
    }
    public class MineFieldMode : GameMode
    {
        public MineFieldMode() : base()
        {
            Limits[typeof(Asteroid)] = 10;
            Limits[typeof(Spacemine)] = 4;
        }
    }
    public class BossRunMode : GameMode
    {
        private int _stage = 0;
        private Dictionary<Type, bool> _spawnList;
        public BossRunMode() : base()
        {
            _spawnList = new Dictionary<Type, bool>()
            {
                {typeof(Phantom), false},
                {typeof(Nightmare), false},
            };
        }
        public override void AddEnemies(int score, List<Enemy> enemies)
        {
            if (_stage == 0 && !_spawnList[typeof(Nightmare)])
            {
                _spawnList[typeof(Nightmare)] = true;
                _spawnList[typeof(Phantom)] = false;
                enemies.Add(new Nightmare());   
            } 
            else if (_stage == 1 && !_spawnList[typeof(Phantom)])
            {
                _spawnList[typeof(Nightmare)] = false;
                _spawnList[typeof(Phantom)] = true;
                enemies.Add(new Phantom());
            }
        }
    }
}