using System;
using System.Collections.Generic;
using SplashKitSDK;
using Enum;
using Enemies;
using Gameplay;
using Main;

namespace GameModes
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
        protected readonly List<Enemies.Enemy> _enemies = new List<Enemy>();
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
        public virtual void AddEnemies(int score)
        {
            int[] parameters;
            foreach (var enemyType in _quantityList.Type)
            {
                if (SpawnAllowed(_quantityList.GetQuantity(enemyType), enemyType))
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
        private bool SpawnAllowed(int typeQuantity, EnemyType type)
        {
            return SplashKit.Rnd(0, SpawnRate) == 0 && typeQuantity < _limits[type];
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
    

}