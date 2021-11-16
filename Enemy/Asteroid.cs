using SplashKitSDK;
using Drawable;
using Interface;
using Enum;

namespace Enemies
{
    public class Asteroid : Enemy
    {
        public Asteroid(Position position, IMoveStrategy movePattern) : base(position, movePattern, EnemyType.Asteroid)
        {
            Bitmap bitmap;
            if (SplashKit.Rnd(0, 2) == 0) 
                bitmap = SplashKit.LoadBitmap("asteroid1", "Asteroids/grayAsteroid.png");
            else
                bitmap = SplashKit.LoadBitmap("asteroid2", "Asteroids/brownAsteroid.png");
            Image = new StaticImage(bitmap);
        }
    }
}