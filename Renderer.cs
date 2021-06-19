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
        public void Draw()
        {
            _session.Player.Draw();
            DrawFromList(_session.PlayerProjectiles);
            DrawFromList(_session.EnemyProjectiles);
            DrawFromList(_session.Explosions);
            DrawFromList(_session.Enemies);
            DrawPlayerInfo();
        }
        private void DrawFromList(IEnumerable<DrawableObject> collection)
        {
            foreach(var item in collection) item.Draw();
        }
        private void DrawPlayerInfo()
        {
            SplashKit.DrawText($"Health: {(int)_session.Player.Health}", Color.Green, Global.SmallFont, 24, 20, 40);
            SplashKit.DrawText($"Score: {(int)_session.Player.Score}", Color.Yellow, Global.SmallFont, 24, 20, 70);
        }
    }
}