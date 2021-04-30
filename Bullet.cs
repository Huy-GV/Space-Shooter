using System;
using SplashKitSDK;

namespace Space_Shooter
{
    public class Bullet : GameObject
    {
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
            _movePattern = new StraightMovement(_speed, X, Y, angle);
        }
        private void SetType(Type type)
        {
            switch(type)
            {
                case Type.RedLaser:
                    Bitmap = SplashKit.LoadBitmap("RedLaser", "Bullets/RedLaser.png");
                    _speed = 9;
                    Damage = 15;
                    break;
                case Type.BlueLaser:
                    Bitmap = SplashKit.LoadBitmap("BlueLaser", "Bullets/BlueLaser.png");
                    _speed = 7;
                    Damage = 7;
                    break;
                case Type.RedBeam:
                    Bitmap = SplashKit.LoadBitmap("RedBeam", "Bullets/RedBeam.png");
                    _speed = 8;
                    Damage = 7;
                    break;  
                case Type.TripleLaser:
                    Bitmap = SplashKit.LoadBitmap("TripleLaser", "Bullets/TripleLaser.png");
                    _speed = 7;
                    Damage = 20;
                    break;             
            }
        }
        public override void Update() 
        {            
            _movePattern.Update();
            Y = _movePattern.UpdatedY;
            X = _movePattern.UpdatedX;
        } 

        public bool HitTarget(GameObject gameObj)
        {
            return (SplashKit.BitmapCollision(
                Bitmap, X - Bitmap.CellCenter.X,  Y - Bitmap.CellCenter.Y,
                gameObj.Bitmap, gameObj.X - gameObj.Bitmap.CellCenter.X,  gameObj.Y - gameObj.Bitmap.CellCenter.Y));
        }
    }
}