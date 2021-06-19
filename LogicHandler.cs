using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace SpaceShooter
{
    public class LogicHandler
    {
        private Session _session;
        private GameMode _gameMode;
        private EnemyController _enemyController;
        public LogicHandler(Session session, GameMode gameMode)
        {
            _session = session;
            _gameMode = gameMode;
            _enemyController = new EnemyController(_session.EnemyProjectiles, _gameMode.Enemies);
        }
        public void Update()
        {
            _gameMode.CheckGameEnding(_session.Player);
            _gameMode.AddEnemies((int)_session.Player.Score);

            UpdateExplosions(_session.Explosions);
            UpdatePlayer();
            EnemyKillCheck();
            _enemyController.UpdateEnemies();
            UpdateProjectiles(_session.PlayerProjectiles);
            UpdateProjectiles(_session.EnemyProjectiles);
            ProjectileCheck(_session.Player, _session.EnemyProjectiles);
        }
        private void EnemyKillCheck()
        {
            foreach(var enemy in _gameMode.Enemies)
            {
                ProjectileCheck(enemy, _session.PlayerProjectiles);
                if (enemy.Y > Global.Height || enemy.Health <= 0)
                {
                    _gameMode.RemoveEnemy(enemy);            
                }
                if (_session.Player.CollideWith(enemy.Image, enemy.X, enemy.Y) && 
                !(nameof(enemy.Type).Contains("Boss")))
                { 
                    _session.Player.LoseHealth(enemy.CollisionDamage);
                    var explosion = new Explosion(_session.Player.X, _session.Player.Y);
                    _session.Explosions.Add(explosion);
                    enemy.LoseHealth(100);
                }
            }
        }
        private void ProjectileCheck(IKillable target, List<Bullet> projectiles)
        {
            foreach(var projectile in projectiles.ToArray())
            {
                if (projectile.HitTarget(target.Image, target.X, target.Y))
                {
                    _session.Explosions.Add(new Explosion(target.X, target.Y, Explosion.Type.Fire));
                    target.LoseHealth(projectile.Damage);
                    projectiles.Remove(projectile);
                }
            }
        }
        private void UpdateProjectiles(List<Bullet> projectiles)
        {
            foreach( var projectile in projectiles.ToArray())
            {
                projectile.Update();
                if (Global.InWindow(projectile.X, projectile.Y)) 
                    projectiles.Remove(projectile);
            }
        }
        private void UpdateExplosions(List<Explosion> explosions)
        {
            foreach (var explosion in explosions.ToArray())
            {
                if (explosion.AnimationEnded) _session.Explosions.Remove(explosion);
            }
        }
        private void UpdatePlayer()
        {
            _session.Player.Update();
            if ((_session.Player.Score < 100 && _gameMode is ByLevelMode) || 
            (_gameMode is not ByLevelMode))
                _session.Player.GainScore();
            if (_gameMode.GameEnded) 
                _session.End();
        }
    }
}