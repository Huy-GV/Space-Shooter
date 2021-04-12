using System;
using SplashKitSDK;

namespace Space_Shooter
{
    public class Bullet : GameObject
    {
        public enum Type
        {
            RedLaser,
            BlueLaser,
            RedBeam
        }
        public enum Direction
        {
            Up = 1,
            Down = -1
        }
        private int _direction;
        public Bullet(int x, int y, Direction direction, Type type, bool hasSound){
            if (hasSound) SplashKit.LoadSoundEffect("laserSound", "laser.mp3").Play();
            X = x;
            Y = y;
            _direction = (int)direction;
            SetType(type);
        }
        private void SetType(Type type)
        {
            switch(type)
            {
                case Type.RedLaser:
                    XOffset = 28;
                    YOffset = 41;
                    Bitmap = SplashKit.LoadBitmap("RedLaser", "Bullets/RedLaser.png");
                    break;
                case Type.BlueLaser:
                    XOffset = 21;
                    YOffset = 30;
                    Bitmap = SplashKit.LoadBitmap("BlueLaser", "Bullets/BlueLaser.png");
                    break;
                case Type.RedBeam:
                    XOffset = 21;
                    YOffset = 30;
                    Bitmap = SplashKit.LoadBitmap("RedBeam", "Bullets/RedBeam.png");
                    break;               
            }
        }
        private void Move() => Y -= 7 * _direction; 
        public override void Update() => Move(); 
        public bool HitTarget(GameObject gameObject)
        {
            return (SplashKit.BitmapCollision(
                Bitmap, AdjustedX, AdjustedY,
                gameObject.Bitmap, gameObject.AdjustedX, gameObject.AdjustedY));
        }
    }
}