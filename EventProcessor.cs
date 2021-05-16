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
            UpdatePlayer(_player, _session.GameModeIndex);
            UpdateProjectiles(_session.PlayerProjectiles);
            UpdateProjectiles(_session.EnemyProjectiles);
            PlayerProjectileCheck(_player, _session.EnemyProjectiles);

            foreach(Enemy enemy in _gameMode.Enemies.ToArray())
            {
                enemy.Update();
                EnemyProjectileCheck(enemy, _session.PlayerProjectiles);
                EnemyStateCheck(enemy);
                if (_player.CollideWith(enemy.Image, enemy.X, enemy.Y) && !(enemy is Boss))
                { 
                    _player.LoseHealth(enemy.CollisionDamage);
                    enemy.LoseHealth(100);
                }
                if (enemy is IHaveGun GunShip) 
                    if (GunShip.CoolDownEnded) _session.EnemyProjectiles.Add(GunShip.Shoot());  
            }
        }
        private void PlayerProjectileCheck(Player player, List<Bullet> projectiles)
        {
            foreach(var projectile in projectiles.ToArray())
            {
                if (projectile.HitTarget(player.Image, player.X, player.Y))
                {
                    player.LoseHealth(projectile.Damage);
                    projectiles.Remove(projectile);
                }
            }
        }
        private void EnemyProjectileCheck(Enemy enemy, List<Bullet> projectiles)
        {
            foreach(var projectile in projectiles.ToArray())
            {
                if (projectile.HitTarget(enemy.Image, enemy.X, enemy.Y))
                {
                    enemy.LoseHealth(projectile.Damage);
                    projectiles.Remove(projectile);
                }
            }
        }
        private void UpdateProjectiles(List<Bullet> projectiles)
        {
            foreach( var projectile in projectiles.ToArray())
            {
                projectile.Update();
                if (projectile.Y < 0 || projectile.Y > Global.Height || projectile.X < 0 || projectile.X > Global.Width) 
                    projectiles.Remove(projectile);
            }
        }
        private void CreateExplosion(int x, int y, Explosion.Type type) => _session.Explosions.Add(new Explosion(x, y, type));
        private void UpdateExplosions(List<Explosion> explosions)
        {
            foreach (var explosion in explosions.ToArray())
            {
                explosion.Update();
                if (explosion.AnimationEnded) _session.Explosions.Remove(explosion);
            }
        }
        private void EnemyStateCheck(Enemy enemy)
        {
            if (enemy.Y > Global.Height || enemy.Health <= 0)
            {
                if (enemy.Health <= 0) 
                    CreateExplosion(enemy.X, enemy.Y, enemy.ExplosionType); 
                _gameMode.RemoveEnemy(enemy);            
            }
        }
        private void UpdatePlayer(Player player, int gameModeIndex)
        {
            player.Update();
            if ((player.Score < 100 && gameModeIndex < 7) || gameModeIndex >= 7)
                player.GainScore();
            if (_gameMode.GameEnded) 
                _session.End();
        }
    }
}