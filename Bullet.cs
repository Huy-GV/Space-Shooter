using System;
using SplashKitSDK;

namespace SpaceShooter
{
    public class Bullet : DrawableObject
    {
        private IMoveStrategy _movePattern;
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
        public Bullet(int x, int y, Type type, int moveAngle, int imageAngle) : base()
        {
            SetType(type, imageAngle);
            _position = new Position(x, y);
            _moveAngle = moveAngle;
            _movePattern = new StraightLinePattern(_speed, _moveAngle);
        }
        private void SetType(Type type, int imageAngle)
        {
            Bitmap bitmap;
            switch(type)
            {
                case Type.RedLaser:
                    bitmap = SplashKit.LoadBitmap("RedLaser", "Bullets/RedLaser.png");
                    _speed = 12;
                    Damage = 25;
                    break;
                case Type.BlueLaser:
                    bitmap = SplashKit.LoadBitmap("BlueLaser", "Bullets/BlueLaser2.png");
                    _speed = 7;
                    Damage = 7;
                    break;
                case Type.RedBeam:
                    bitmap = SplashKit.LoadBitmap("RedBeam", "Bullets/RedBeam.png");
                    _speed = 9;
                    Damage = 7;
                    break;  
                case Type.TripleLaser:
                    bitmap = SplashKit.LoadBitmap("TripleLaser", "Bullets/TripleLaser.png");
                    _speed = 8;
                    Damage = 30;
                    break;  
                default:
                    throw new NotImplementedException($"The bullet type {type} doesnt exist");           
            }
            var option = SplashKit.OptionRotateBmp(imageAngle);
            Image = new StaticImage(bitmap, option);
        }
        public void Update() 
        {            
            _position = _movePattern.Move(_position);
        }
        public bool HitTarget(Image targetImage, int x, int y)
        {
            return (SplashKit.BitmapCollision(
                targetImage.Bitmap, targetImage.AdjustedX(x),  targetImage.AdjustedY(y),
                Image.Bitmap, Image.AdjustedX(X),  Image.AdjustedY(Y)));
        }
    }
}