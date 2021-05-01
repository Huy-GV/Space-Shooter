using SplashKitSDK;

namespace Space_Shooter
{
    public class Explosion : GameObject
    {
        private DrawableObject _image;
        public enum Type{
            RedLaser,
            BlueLaser,
            Fire,
            Default
        }
        private Animation _animation;
        private AnimationScript _explosionScript;
        private DrawingOptions _option;
        public bool AnimationEnded(){return (_animation.Ended);}
        public Explosion(int x, int y, Type type)
        {
            var explosionSound = SplashKit.LoadSoundEffect("explosionSound", "explosion.mp3");
            SplashKit.PlaySoundEffect("explosionSound", (float)0.12);
            X = x;
            Y = y;
            SetAnimations(type);
        }
        public void SetAnimations(Type type)
        {
            //different types only vary in animation so sub-classing them like alienship is unnecessary
            switch(type)
            {
                // case Type.RedLaser:
                //     SetLaserExplosion();
                //     break;
                // case Type.Fire:
                //     SetFireExplosion();
                //     break;
                // case Type.BlueLaser:
                //     SetBlueLaserExplosion();
                //     break; 
                default:
                    SetFireExplosion();
                    break;
            }
        }
        private void SetLaserExplosion()
        {
            var bitmap = SplashKit.LoadBitmap("laserExplosion", "Explosions/laserExplosion.png"); 
            // Bitmap.SetCellDetails(180, 180, 17, 1, 17);
            var cellDetails = new int[]{180,180, 17, 1, 17};
            // _explosionScript = SplashKit.LoadAnimationScript("laserExplosion", "laserExplosion.txt");
            // _animation = _explosionScript.CreateAnimation("laserExplosion");
            // _option = SplashKit.OptionWithAnimation(_animation);
            _image = new AnimatedImage("laserExplosion", "laserExplosion", bitmap, cellDetails);
        }

        //! to be deleted
        // private void SetBlueLaserExplosion()
        // {
        //     Bitmap = SplashKit.LoadBitmap("blueExplosion", "Explosions/blueExplosion.png"); 
        //     Bitmap.SetCellDetails(120, 120, 17, 1, 17);
        //     _explosionScript = SplashKit.LoadAnimationScript("blueLaserExplosion", "blueLaserExplosion.txt");
        //     _animation = _explosionScript.CreateAnimation("blueLaserExplosion");
        //     _option = SplashKit.OptionWithAnimation(_animation);
        // }
        private void SetFireExplosion()
        {
            var bitmap = SplashKit.LoadBitmap("fireExplosion", "Explosions/fireExplosion.png"); 
            // Bitmap.SetCellDetails(100, 100, 15, 1, 15);
            var cellDetails = new int[]{100, 100, 15, 1, 15};
            // _explosionScript = SplashKit.LoadAnimationScript("fireExplosion", "fireExplosion.txt");
            // _animation = _explosionScript.CreateAnimation("fireExplosion");
            // _option = SplashKit.OptionWithAnimation(_animation);
            _image = new AnimatedImage("fireExplosion", "fireExplosion", bitmap, cellDetails);
        }
        // private void SetDefaultExplosion()
        // {
        //     Bitmap = SplashKit.LoadBitmap("defaultExplosion", "Explosions/defaultExplosion.png"); 
        //     Bitmap.SetCellDetails(180, 180, 20, 1, 20);
        //     _explosionScript = SplashKit.LoadAnimationScript("defaultExplosion", "defaultExplosion.txt");
        //     _animation = _explosionScript.CreateAnimation("defaultExplosion");
        //     _option = SplashKit.OptionWithAnimation(_animation);
        // }
        public override void Draw()
        {
            // SplashKit.DrawBitmap(Bitmap,X - Bitmap.CellCenter.X,  Y - Bitmap.CellCenter.Y,_option ); 
            _image.Draw(X, Y);
        } 
        public override void Update()
        {
            _animation.Update();
            Y += 2;
        }
    }
}
    





    























