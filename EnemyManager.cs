using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class EnemyManager
    {
        private List<Enemy> _enemies = new List<Enemy>();
        private List<Bullet> _enemyProjectiles;
        public IEnumerable<Enemy> Enemies
        {
            get 
            {
                foreach(var enemy in _enemies) yield return enemy;
            }
        }

        public EnemyManager(List<Bullet> enemyProjectiles)
        {
            _enemyProjectiles = enemyProjectiles;
        }
    }
}