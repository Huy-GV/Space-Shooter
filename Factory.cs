using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace SpaceShooter
{
    public class EnemyFactory
    {
        private List<Enemy> _enemies; 
        public EnemyFactory(List<Enemy> enemies)
        {
            _enemies = enemies;
        }
        public Enemy SpawnEnemy(Type enemyType)
        {
            switch(enemyType)
            {
                case typeof(BlueAlienship): return new BlueAlienship();
                case typeof(BlueAlienship): return new BlueAlienship();
                case typeof(BlueAlienship): return new BlueAlienship();
                case typeof(BlueAlienship): return new BlueAlienship();
                case typeof(BlueAlienship): return new BlueAlienship();
                case typeof(BlueAlienship): return new BlueAlienship();
                case typeof(BlueAlienship): return new BlueAlienship();
                case typeof(BlueAlienship): return new BlueAlienship();
                case typeof(BlueAlienship): return new BlueAlienship();
                case typeof(BlueAlienship): return new BlueAlienship();
                case typeof(BlueAlienship): return new BlueAlienship();
            }
        }
    }
}