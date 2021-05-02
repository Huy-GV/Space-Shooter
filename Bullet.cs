using System;
using SplashKitSDK;

namespace Space_Shooter
{
    public class Bullet : GameObject
    {
        //TODO: rotate all bullets to one angle and use code angle to rotate it again
        DrawableObject _image;
        private MovePattern _movePattern;
        public enum Type
        {
            RedLaser,
            BlueLaser,
            RedBeam,
            TripleLaser
        }
        private int _angle;
        public int Damage{get; private set;}
        private int _speed;
        public Bullet(int x, int y, Type type, bool hasSound, int angle){
            if (hasSound) SplashKit.LoadSoundEffect("laserSound", "laser.mp3").Play();
            X = x;
            Y = y;
            SetType(type);
            _angle = angle;
            _movePattern = new StraightLinePattern(_speed, X, Y, angle);
        }
        private void SetType(Type type)
        {
            Bitmap bitmap;
            switch(type)
            {
                case Type.RedLaser:
                    bitmap = SplashKit.LoadBitmap("RedLaser", "Bullets/RedLaser.png");
                    _speed = 9;
                    Damage = 15;
                    break;
                case Type.BlueLaser:
                    bitmap = SplashKit.LoadBitmap("BlueLaser", "Bullets/BlueLaser.png");
                    _speed = 7;
                    Damage = 7;
                    break;
                case Type.RedBeam:
                    bitmap = SplashKit.LoadBitmap("RedBeam", "Bullets/RedBeam.png");
                    _speed = 8;
                    Damage = 7;
                    break;  
                default:
                    bitmap = SplashKit.LoadBitmap("TripleLaser", "Bullets/TripleLaser.png");
                    _speed = 7;
                    Damage = 20;
                    break;             
            }
            _image = new StaticImage(bitmap);
        }
        public override void Update() 
        {            
            _movePattern.Update();
            Y = _movePattern.UpdatedY;
            X = _movePattern.UpdatedX;
        }
        public override void Draw()
        {
            _image.Draw(X, Y);
        }
        public bool HitTarget(DrawableObject image, int x, int y)
        {
            return (SplashKit.BitmapCollision(
                image.Bitmap, image.AdjustedX(x),  image.AdjustedY(y),
                _image.Bitmap, _image.AdjustedX(X),  _image.AdjustedY(Y)));
        }
    }
}