using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class Spacemine : Enemy
    {
        private Animation _animation;
        private DrawingOptions _option;
        private AnimationScript _flyScript;
        public Spacemine(int lastSpacemineX, int lastSpacemineY): base()
        {
            ExplosionType = Explosion.Type.Default;
            CollisionDamage = 16;
            SetAnimations();
            SetCoordinates(lastSpacemineX, lastSpacemineY);
            _movePattern = new StraightLinePattern(3, X, Y, 90);
        }
        private void SetCoordinates(int lastSpacemineX, int lastSpacemineY)
        {
            int randomX = SplashKit.Rnd(0,3);
            if (lastSpacemineY >= 120)
            {
                X = (2 * randomX + 1) * 100; 
                Y = -140;
            } else
            {
                X = (2 * SplashKit.Rnd(0,3) + 1) * 100;
                Y = lastSpacemineY - 240;
            }
        }
        private void SetAnimations()
        {
            Bitmap = SplashKit.LoadBitmap("spaceMine", "spaceMine.png");
            Bitmap.SetCellDetails(120, 120, 2, 1, 2);
            _flyScript = SplashKit.LoadAnimationScript("FloatingScript", "spacemineScript.txt");
            _animation = _flyScript.CreateAnimation("floating");
            _option = SplashKit.OptionWithAnimation(_animation);
        }
        public Spacemine() : this(Global.Width, Global.Height){}
        public override void Draw()=> SplashKit.DrawBitmap(Bitmap, X - Bitmap.CellCenter.X,  Y - Bitmap.CellCenter.Y, _option); 
        public override void Update()
        { 
            _animation.Update();
            Move();
        }
    }
}