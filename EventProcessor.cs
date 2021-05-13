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
            _session.Enemies.ForEach(enemy => enemy.Update());
            _session.Explosions.ForEach(explosion => explosion.Update());
            _gameMode.CheckGameEnding(_session.Player);
            _gameMode.AddEnemies((int)_session.Player.Score);
            foreach(Enemy enemy in _gameMode.Enemies.ToArray())
            {
                enemy.Update();
                enemy.CheckPlayerBullets(_session.Player.Bullets);
                CheckEnemyStatus(enemy);
                if (_session.Player.CollideWith(enemy.Image, enemy.X, enemy.Y) && !(enemy is Boss))
                { 
                    _session.Player.LoseHealth(enemy.CollisionDamage);
                    enemy.LoseHealth(100);
                }
                if (enemy is IHaveGun Gunner) 
                    _session.Player.CheckEnemyBullets(Gunner.Bullets);   
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