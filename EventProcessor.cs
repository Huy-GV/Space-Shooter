using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace SpaceShooter
{
    public class EventProcessor
    {
        private Session _session;
        private GameMode _gameMode;
        private Player _player;
        public EventProcessor(Session session, GameMode gameMode)
        {
            _session = session;
            _gameMode = gameMode;
            _player = _session.Player;
        }
        public void Update()
        {
            _gameMode.CheckGameEnding(_player);
            _gameMode.AddEnemies((int)_player.Score);

            UpdateExplosions(_session.Explosions);
            UpdatePlayer();
            UpdateProjectiles(_session.PlayerProjectiles);
            UpdateProjectiles(_session.EnemyProjectiles);
            ProjectileCheck(_player, _session.EnemyProjectiles);

            foreach(Enemy enemy in _gameMode.Enemies)
            {
                enemy.Update();
                ProjectileCheck(enemy, _session.PlayerProjectiles);
                EnemyRemovalCheck(enemy);
                if (_player.CollideWith(enemy.Image, enemy.X, enemy.Y) && !(enemy is Boss))
                { 
                    _player.LoseHealth(enemy.CollisionDamage);
                    _session.Explosions.Add(new Explosion(_player.X, _player.Y, Explosion.Type.Fire));
                    enemy.LoseHealth(100);
                }
                if (enemy is IHaveGun GunShip) 
                    if (GunShip.CoolDownEnded) _session.EnemyProjectiles.Add(GunShip.Shoot());  
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
                explosion.Update();
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
            _player.Update();
            if ((_player.Score < 100 && _gameMode is ByLevelMode) || (_gameMode is not ByLevelMode))
                _player.GainScore();
            if (_gameMode.GameEnded) 
                _session.End();
        }
    }
}