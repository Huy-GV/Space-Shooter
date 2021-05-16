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
            _session.Player.Update();
            _session.Player.CheckEnemyBullets(_session.EnemyProjectiles);
            _session.Explosions.ForEach(explosion => explosion.Update());
            _gameMode.CheckGameEnding(_session.Player);
            _gameMode.AddEnemies((int)_session.Player.Score);
            UpdateProjectiles(_session.PlayerProjectiles);
            UpdateProjectiles(_session.EnemyProjectiles);
            foreach(Enemy enemy in _gameMode.Enemies.ToArray())
            {
                enemy.Update();
                enemy.CheckPlayerBullets(_session.PlayerProjectiles);
                CheckEnemyStatus(enemy);
                if (_session.Player.CollideWith(enemy.Image, enemy.X, enemy.Y) && !(enemy is Boss))
                { 
                    _session.Player.LoseHealth(enemy.CollisionDamage);
                    enemy.LoseHealth(100);
                }
                if (enemy is IHaveGun Gunner) 
                    if (Gunner.CoolDownEnded) _session.EnemyProjectiles.Add(Gunner.Shoot());  
            }
        }
        private void UpdateProjectiles(List<Bullet> projectiles)
        {
            foreach( var projectile in projectiles)
            {
                projectile.Update();
                if (projectile.Y < 0 || projectile.Y > Global.Height || projectile.X < 0 || projectile.X > Global.Width) projectiles.Remove(projectile);
            }
        }
        private void CreateExplosion(int x, int y, Explosion.Type type) => _session.Explosions.Add(new Explosion(x, y, type));
        private void UpdateExplosions()
        {
            foreach (var explosion in _session.Explosions.ToArray())
            {
                explosion.Update();
                if (explosion.AnimationEnded) _session.Explosions.Remove(explosion);
            }
        }
        private void CheckEnemyStatus(Enemy enemy)
        {
            if (enemy.Y > Global.Height || enemy.Health <= 0)
            {
                if (enemy.Health <= 0) 
                    CreateExplosion(enemy.X, enemy.Y, enemy.ExplosionType); 
                _gameMode.RemoveEnemy(enemy);            
            }
        }
        private void UpdatePlayer()
        {
            _session.Player.Update();
            if ((_session.Player.Score < 100 && _session.GameModeIndex < 7) || _session.GameModeIndex >= 7)
                _session.Player.GainScore();
            if (_gameMode.GameEnded) 
                _session.End();
        }
    }
}