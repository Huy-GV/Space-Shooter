using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class EnemyController
    {
        private IEnumerable<Enemy> _enemies;
        private List<Bullet> _enemyProjectiles;
        public EnemyController(List<Bullet> enemyProjectiles, IEnumerable<Enemy> enemies)
        {
            _enemyProjectiles = enemyProjectiles;
            _enemies = enemies;
        }
        public void UpdateEnemies()
        {
            foreach(var enemy in _enemies)
            {
                enemy.Update();
                if (enemy is Alienship alienship)
                {
                    if (alienship.OverheatEnded) _enemyProjectiles.Add(alienship.Shoot());
                }
            }
        }
    }
}