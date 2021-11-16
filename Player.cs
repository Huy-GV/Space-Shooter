using System;
using SplashKitSDK;
using Interface;
using Drawable;
using Weapon;
namespace Main

{
    public class Player : DrawableObject, IKillable
    {
        public bool CoolDownEnded{get => _currentGun.OverheatEnded;}
        private readonly double _swapTimeDuration = 0.5;
        private double _swapTime = 0;
        public enum ShipType
        {
            Versatile,
            Agile,
            Armoured
        }
        public int Health{get; private set;}
        private Gun _primaryGun, _secondaryGun, _currentGun;
        private int _speed;
        public double Score{get; private set;} = 0;
        public Player(int option)
        {
            _position = new Position(Global.Width / 2, Global.Height * 4 / 5);
            SetAnimation((ShipType)option);
            _primaryGun = new Gun(1, Bullet.Type.BlueLaser, true);
            _secondaryGun = new Gun(2, Bullet.Type.RedBeam, true);
            _currentGun = _primaryGun;
        }
        public void SwapGun()
        {
            if (_swapTime <= 0)
            {
                if (_currentGun == _primaryGun)
                    _currentGun = _secondaryGun;
                else 
                    _currentGun = _primaryGun;
                _swapTime = _swapTimeDuration;
            }
        } 
        private void SetAnimation(ShipType type)
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
            _speed = 4;
            var bitmap = SplashKit.LoadBitmap("player1", "PlayerSprites/VersatileShipSprites.png");
            var cellDetails = new int[]{90, 90, 5, 1, 5};
            Image = new AnimatedImage("spaceship1Script", "flying", bitmap, cellDetails);
        }
        private void SetAgileShip()
        {  
            Health = 50;
            _speed = 5;
            var bitmap = SplashKit.LoadBitmap("player2", "PlayerSprites/AgileShipSprites.png");
            var cellDetails = new int[]{90, 90, 4, 1, 4};
            Image = new AnimatedImage("spaceship2Script", "flying", bitmap, cellDetails);
        }
        private void SetArmouredShip()
        {
            Health = 150;
            _speed = 3;
            var bitmap = SplashKit.LoadBitmap("player3", "PlayerSprites/ArmouredShipSprites.png");
            var cellDetails = new int[]{90, 90, 8, 1, 8};
            Image = new AnimatedImage("spaceship3Script", "flying", bitmap, cellDetails);
        }
        public void MoveLeft() => _position.X -= _speed;
        public void MoveRight() => _position.X += _speed;
        public void MoveUp() => _position.Y -= _speed;
        public void MoveDown() => _position.Y += _speed;
        public Bullet Shoot() => _currentGun.OpenFire(X, Y, -90, -180);
        public void Update()
        {
            _currentGun.Update();
            if (_swapTime > 0) _swapTime -= 1/(double)60;
        }
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