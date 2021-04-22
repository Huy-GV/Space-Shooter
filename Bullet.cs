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
            RedBeam,
            TripleLaser
        }
        public enum Direction
        {
            Up = 1,
            Down = -1
        }
        public int Damage{get; private set;}
        private int _direction;
        private int _speed;
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
                    XOffset = 100;
                    YOffset = 146;
                    Bitmap = SplashKit.LoadBitmap("RedLaser", "Bullets/RedLaser.png");
                    _speed = 9;
                    Damage = 15;
                    break;
                case Type.BlueLaser:
                    XOffset = 21;
                    YOffset = 30;
                    Bitmap = SplashKit.LoadBitmap("BlueLaser", "Bullets/BlueLaser.png");
                    _speed = 7;
                    Damage = 7;
                    break;
                case Type.RedBeam:
                    XOffset = 21;
                    YOffset = 30;
                    Bitmap = SplashKit.LoadBitmap("RedBeam", "Bullets/RedBeam.png");
                    _speed = 8;
                    Damage = 7;
                    break;  
                case Type.TripleLaser:
                    XOffset = 63;
                    YOffset = 30;
                    Bitmap = SplashKit.LoadBitmap("TripleLaser", "Bullets/TripleLaser.png");
                    _speed = 7;
                    Damage = 20;
                    break;             
            }
        }
        private void Move() => Y -= _speed * _direction; 
        public override void Update() => Move(); 
        public bool HitTarget(GameObject gameObject)
        {
            return (SplashKit.BitmapCollision(
                Bitmap, AdjustedX, AdjustedY,
                gameObject.Bitmap, gameObject.AdjustedX, gameObject.AdjustedY));
        }
    }
}