using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace SpaceShooter
{
    public class EnemyFactory
    {
        //TODO: enemies is accessible by factory?
        private List<Enemy> _enemies; 
        public EnemyFactory(List<Enemy> enemies)
        {
            _enemies = enemies;
        }
        public Enemy SpawnEnemy(Type enemyType)
        {
            if (enemyType == typeof(Asteroid))
                return SpawnAsteroid();
            else if ((enemyType == typeof(BlueAlienship) || 
                enemyType == typeof(PurpleAlienship) || 
                enemyType == typeof(RedAlienship) || 
                enemyType == typeof(Spacemine)))
                return SpawnBasedOnXY(enemyType);
            else if (enemyType == typeof(KamikazeAlien))
                return new KamikazeAlien();
            else
                throw new NotImplementedException($"The type {enemyType} does not exist");
        }
        private Enemy SpawnAsteroid()
        {
            if (_enemies.Count != 0)
            {
                var lastEnemyY = _enemies[_enemies.Count - 1].Y;
                return new Asteroid(lastEnemyY);
            }
            return (Enemy)new Asteroid();
        }
        private Enemy SpawnBasedOnXY(Type enemyType)
        {
            object[] parameters;
            if (_enemies.Count != 0)
            {
                var lastEnemy = _enemies[_enemies.Count - 1];
                parameters = new object[]{lastEnemy.X, lastEnemy.Y};
            } else 
                parameters = new object[]{null, null};
            return (Enemy)Activator.CreateInstance(enemyType, parameters);
        }
    }
}