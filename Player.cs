using System;
using SplashKitSDK;
using System.Collections.Generic;
namespace Space_Shooter

{
    public class Player : GameObject
    {
        public enum ShipType
        {
            Versatile,
            Agile,
            Armoured
        }
        public double CoolDown{get; private set;}
        public int Health{get; private set;}
        private Gun _gun;
        public List<Bullet> Bullets{ get { return _gun.Bullets; }}
        private Animation _animation;
        private DrawingOptions _option;
        private AnimationScript _flyScript;
        public AnimatedImage _image;
        private int _speed;
        public double Score{get; private set;}
        private SoundEffect _laserSound = SplashKit.LoadSoundEffect("laserSound", "laser.mp3");
        public Player(int option){
            X = Global.Width / 2;
            Y = Global.Height * 4 / 5;      
            Score = 0;
            SetAnimation((ShipType)option);
        }
        public void SetAnimation(ShipType type)
        {
            switch(type)
            {
                // case ShipType.Armoured:
                //     SetArmouredShip();
                //     break;
                // case ShipType.Agile:
                //     SetAgileShip();
                //     break;
                default:
                    SetVersatileShip();
                    break;
            }
        }
        private void SetVersatileShip()
        {
            Health = 100;
            // Bitmap = SplashKit.LoadBitmap("player1", "PlayerSprites/VersatileShipSprites.png");
            // Bitmap.SetCellDetails(90, 90, 5, 1, 5);
            // _flyScript = SplashKit.LoadAnimationScript("FlyingScript1", "spaceship1Script.txt");
            // _animation = _flyScript.CreateAnimation("flying");
            // _option = SplashKit.OptionWithAnimation(_animation);
            _gun = new Gun(1.25, Bullet.Type.RedBeam, true);
            _speed = 4;


            var bitmap = SplashKit.LoadBitmap("player1", "PlayerSprites/VersatileShipSprites.png");
            var cellDetails = new int[]{90, 90, 5, 1, 5};
            _image = new AnimatedImage("spaceship1Script", "flying", bitmap, cellDetails);
        }
        // private void SetAgileShip()
        // {  
        //     Health = 50;
        //     Bitmap = SplashKit.LoadBitmap("player2", "PlayerSprites/AgileShipSprites.png");
        //     Bitmap.SetCellDetails(90, 90, 4, 1, 4);
        //     _flyScript = SplashKit.LoadAnimationScript("FlyingScript2", "spaceship2Script.txt");
        //     _animation = _flyScript.CreateAnimation("flying");
        //     _option = SplashKit.OptionWithAnimation(_animation);
        //     _gun = new Gun(1, Bullet.Type.RedBeam, true);
        //     _speed = 5;
        // }
        // private void SetArmouredShip()
        // {
        //     Health = 150;
        //     Bitmap = SplashKit.LoadBitmap("player3", "PlayerSprites/ArmouredShipSprites.png");
        //     Bitmap.SetCellDetails(90, 90, 8, 1, 8);
        //     _flyScript = SplashKit.LoadAnimationScript("FlyingScript3", "spaceship3Script.txt");
        //     _animation = _flyScript.CreateAnimation("flying");
        //     _option = SplashKit.OptionWithAnimation(_animation);
        //     _gun = new Gun(1.5, Bullet.Type.RedBeam, true);
        //     _speed = 3;
        // }
        public override void Draw()
        { 
            // SplashKit.DrawBitmap(Bitmap,X - Bitmap.CellCenter.X,  Y - Bitmap.CellCenter.Y,_option );
            _image.Draw(X, Y);
            _gun.DrawBullets();   
        }
        public void MoveLeft() => X -= _speed;
        public void MoveRight() => X += _speed;
        public void MoveUp() => Y -= _speed;
        public void MoveDown() => Y += _speed;
        public void Shoot() 
        { 
            if (_gun.CoolDownEnded) _gun.OpenFire(X, Y, -90);
        }
        public override void Update()
        {
            // _animation.Update();
            _gun.Update();
        }
        public void GainScore(){ Score += 1/(double)60;}
        public void LoseHealth(int damage) => Health -= damage;
        public void CheckEnemyBullets(List<Bullet> enemyBullets)
        {
            foreach(var bullet in enemyBullets.ToArray())
            {
                if (bullet.HitTarget(_image, X, Y))
                {
                    Health -= bullet.Damage;
                    enemyBullets.Remove(bullet);
                }
            }
        }
        public bool CollideWith(DrawableObject image, int x, int y)
        {
            // return (SplashKit.BitmapCollision(
            //     Bitmap, X - Bitmap.CellCenter.X,  Y - Bitmap.CellCenter.Y,
            //     gameObj.Bitmap, gameObj.X - gameObj.Bitmap.CellCenter.X,  gameObj.Y - gameObj.Bitmap.CellCenter.Y));

            return (SplashKit.BitmapCollision(
                image.Bitmap, image.AdjustedX(x),  image.AdjustedY(y),
                _image.Bitmap, image.AdjustedX(X),  image.AdjustedY(Y)));
        }
    }
}