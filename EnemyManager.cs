using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class EnemyManager
    {
        private List<Enemy> _enemies = new List<Enemy>();
        private List<Bullet> _enemyProjectiles;
        public int[] LastEnemyPostion 
        {
            get 
            {
                if (_enemies.Count == 0) return new int[]{Global.Width, Global.Height};
                else
                {
                    var parameters = new int[2];
                    parameters[0] = _enemies[_enemies.Count - 1].X;
                    parameters[1] = _enemies[_enemies.Count - 1].Y;
                    return parameters;
                }
            }
        }
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
        public void RemoveEnemy(Enemy enemy) => _enemies.Remove(enemy);
        public void AddEnemy(Enemy enemy) => _enemies.Add(enemy);
    }
}