using System;
using System.Collections.Generic;
using SplashkitSDK;

namespace SpaceShooter
{
    public class Renderer
    {
        private Session _session;
        public Renderer(Session session)
        {
            _session = session;
        }
        //create environment that will contain bullets?
        public void Draw()
        {
            _session.Player.Draw();
            _session.Enemies.Foreach(enemy => enemy.Draw());
            _session.Explosion.Foreach(explosion => explosion.Draw());
        }
    }

    public class LogicProcessor
    {
        private Session _session;
        private GameMode _gameMode;
        public LogicProcessor(Session session, GameMode gameMode)
        {
            _session = session;
            _gameMode = gameMode;
        }
        public void Update()
        {
            _session.Player.Update();
            _session.Enemies.Foreach(enemy => enemy.Update());
            _session.Explosion.Foreach(explosion => explosion.Update());
            _gameMode.CheckGameEnding(_player);
            _gameMode.AddEnemies((int)_player.Score);
            foreach(Enemy enemy in _gameMode.Enemies.ToArray())
            {
                enemy.Update();
                enemy.CheckPlayerBullets(_player.Bullets);
                CheckEnemyStatus(enemy);
                if (_player.CollideWith(enemy.Image, enemy.X, enemy.Y) && !(enemy is Boss))
                { 
                    _player.LoseHealth(enemy.CollisionDamage);
                    enemy.LoseHealth(100);
                }
                if (enemy is IHaveGun Gunner) 
                    _player.CheckEnemyBullets(Gunner.Bullets);   
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
    }
}