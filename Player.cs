using System;
using SplashKitSDK;
using System.Collections.Generic;
namespace SpaceShooter

{
    public class Player : DrawableObject, IShootableObject
    {
        public bool CoolDownEnded{get => _gun.CoolDownEnded;}
        public Explosion.Type ExplosionType{get; init;}
        public enum ShipType
        {
            Versatile,
            Agile,
            Armoured
        }
        public int Health{get; private set;}
        private Gun _gun;
        private int _speed;
        public double Score{get; private set;}
        public Player(int option) : base()
        {
            _position.X = Global.Width / 2;
            _position.Y = Global.Height * 4 / 5;    
            Score = 0;
            SetAnimation((ShipType)option);
        }
        public void SetAnimation(ShipType type)
        {
            switch(type)
            {
                case ShipType.Armoured:
                    SetArmouredShip();
                    break;
                case ShipType.Agile:
                    SetAgileShip();
                    break;
                case ShipType.Versatile:
                    SetVersatileShip();
                    break;
                default: 
                    throw new NotImplementedException($"The type {type} does not exist");
            }
        }
        private void SetVersatileShip()
        {
            Health = 100;
            _gun = new Gun(1.25, Bullet.Type.RedBeam, true);
            _speed = 4;
            var bitmap = SplashKit.LoadBitmap("player1", "PlayerSprites/VersatileShipSprites.png");
            var cellDetails = new int[]{90, 90, 5, 1, 5};
            Image = new AnimatedImage("spaceship1Script", "flying", bitmap, cellDetails);
        }
        private void SetAgileShip()
        {  
            Health = 50;
            var bitmap = SplashKit.LoadBitmap("player2", "PlayerSprites/AgileShipSprites.png");
            var cellDetails = new int[]{90, 90, 4, 1, 4};
            Image = new AnimatedImage("spaceship2Script", "flying", bitmap, cellDetails);
            _gun = new Gun(1, Bullet.Type.RedBeam, true);
            _speed = 5;
        }
        private void SetArmouredShip()
        {
            Health = 150;
            var bitmap = SplashKit.LoadBitmap("player3", "PlayerSprites/ArmouredShipSprites.png");
            var cellDetails = new int[]{90, 90, 8, 1, 8};
            Image = new AnimatedImage("spaceship3Script", "flying", bitmap, cellDetails);
            _gun = new Gun(1.5, Bullet.Type.RedBeam, true);
            _speed = 3;
        }
        public void MoveLeft() => _position.X -= _speed;
        public void MoveRight() => _position.X += _speed;
        public void MoveUp() => _position.Y -= _speed;
        public void MoveDown() => _position.Y += _speed;
        public Bullet Shoot() => _gun.OpenFire(X, Y, -90, -180);
        public void Update() => _gun.Update();
        public void GainScore() => Score += 1/(double)60;
        public void LoseHealth(int damage) => Health -= damage;
        public bool CollideWith(Image image, int x, int y)
        {
            return (SplashKit.BitmapCollision(
                image.Bitmap, image.AdjustedX(x),  image.AdjustedY(y),
                Image.Bitmap, Image.AdjustedX(X),  Image.AdjustedY(Y)));
        }
    }
}