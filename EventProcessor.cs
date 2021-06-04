using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace SpaceShooter
{
    public class EventProcessor
    {
        private Session _session;
        private GameMode _gameMode;
        public EventProcessor(Session session, GameMode gameMode)
        {
            _session = session;
            _gameMode = gameMode;
        }
        public void Update()
        {
            _gameMode.CheckGameEnding(_session.Player);
            _gameMode.AddEnemies((int)_session.Player.Score);

            UpdateExplosions(_session.Explosions);
            UpdatePlayer();
            UpdateEnemies();
            UpdateProjectiles(_session.PlayerProjectiles);
            UpdateProjectiles(_session.EnemyProjectiles);
            ProjectileCheck(_session.Player, _session.EnemyProjectiles);
        }
        private void UpdateEnemies()
        {
            foreach(Enemy enemy in _gameMode.Enemies)
            {
                enemy.Update();
                ProjectileCheck(enemy, _session.PlayerProjectiles);
                EnemyRemovalCheck(enemy);
                if (_session.Player.CollideWith(enemy.Image, enemy.X, enemy.Y) && !(enemy is Phantom && enemy is Nightmare))
                { 
                    _session.Player.LoseHealth(enemy.CollisionDamage);
                    _session.Explosions.Add(new Explosion(_session.Player.X, _session.Player.Y, Explosion.Type.Fire));
                    enemy.LoseHealth(100);
                }
                if (enemy is IHaveGun GunShip) 
                    if (GunShip.OverheatEnded) _session.EnemyProjectiles.Add(GunShip.Shoot());  
            }
        }
        private void ProjectileCheck(IShootableObject target, List<Bullet> projectiles)
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
        private void EnemyRemovalCheck(Enemy enemy)
        {
            if (enemy.Y > Global.Height || enemy.Health <= 0)
            {
                _gameMode.RemoveEnemy(enemy);            
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