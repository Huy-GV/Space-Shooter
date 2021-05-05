using System;
using SplashKitSDK;

namespace Space_Shooter
{
    public class Bullet : GameObject
    {
        Image _image;
        private MovePattern _movePattern;
        public int X => _movePattern.X;
        public int Y => _movePattern.Y;
        public enum Type
        {
            RedLaser,
            BlueLaser,
            RedBeam,
            TripleLaser
        }
        private int _moveAngle;
        public int Damage{get; private set;}
        private int _speed;
        public Bullet(int x, int y, Type type, bool hasSound, int moveAngle, int imageAngle){
            if (hasSound) SplashKit.LoadSoundEffect("laserSound", "laser.mp3").Play();
            SetType(type, imageAngle);
            _moveAngle = moveAngle;
            _movePattern = new StraightLinePattern(_speed, x, y, _moveAngle);
        }
        private void SetType(Type type, int imageAngle)
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
            var option = SplashKit.OptionRotateBmp(imageAngle);
            _image = new StaticImage(bitmap, option);
        }
        public override void Update() 
        {            
            _movePattern.Update();

        }
        public override void Draw()
        {
            _image.Draw(_movePattern.X, _movePattern.Y);
        }
        public bool HitTarget(Image image, int x, int y)
        {
            return (SplashKit.BitmapCollision(
                image.Bitmap, image.AdjustedX(x),  image.AdjustedY(y),
                _image.Bitmap, _image.AdjustedX(X),  _image.AdjustedY(Y)));
        }
    }
}