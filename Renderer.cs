using System;
using System.Collections.Generic;
using SplashKitSDK;


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
            _session.Enemies.ForEach(enemy => enemy.Draw());
            _session.PlayerProjectiles.ForEach(projectile => projectile.Draw());
            _session.EnemyProjectiles.ForEach(projectile => projectile.Draw());
            _session.Explosions.ForEach(explosion => explosion.Draw());
            DrawPlayerInfo();
        }
        private void DrawPlayerInfo()
        {
            SplashKit.DrawText($"Health: {(int)_session.Player.Health}", Color.Green, Global.SmallFont, 24, 20, 40);
            SplashKit.DrawText($"Score: {(int)_session.Player.Score}", Color.Yellow, Global.SmallFont, 24, 20, 70);
        }
    }
}