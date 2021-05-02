using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class Spacemine : Enemy
    {
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
            var bitmap = SplashKit.LoadBitmap("blueSpacemine", "Spacemines/blueSpacemine.png");
            var cellDetails = new int[]{120, 120, 2, 1, 2};
            _image = new AnimatedImage("spacemineScript", "floating", bitmap, cellDetails);
        }
        public Spacemine() : this(Global.Width, Global.Height){}
        public override void Draw()
        {
            _image.Draw(X, Y);
        } 
        public override void Update()
        { 
            Move();
        }
    }
}