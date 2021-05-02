using SplashKitSDK;

namespace Space_Shooter
{
    public class Explosion : GameObject
    {
        private AnimatedImage _image;
        public enum Type{
            RedLaser,
            Fire,
            Default
        }
        public bool AnimationEnded=>_image.AnimationEnded;
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
            switch(type)
            {
                case Type.RedLaser:
                    SetLaserExplosion();
                    break;
                case Type.Fire:
                    SetFireExplosion();
                    break; 
                default:
                    SetDefaultExplosion();
                    break;
            }
        }
        private void SetLaserExplosion()
        {
            var bitmap = SplashKit.LoadBitmap("laserExplosion", "Explosions/laserExplosion.png"); 
            var cellDetails = new int[]{180,180, 17, 1, 17};
            _image = new AnimatedImage("laserExplosion", "laserExplosion", bitmap, cellDetails);
        }
        private void SetFireExplosion()
        {
            var bitmap = SplashKit.LoadBitmap("fireExplosion", "Explosions/fireExplosion.png"); 
            var cellDetails = new int[]{100, 100, 15, 1, 15};
            _image = new AnimatedImage("fireExplosion", "fireExplosion", bitmap, cellDetails);
        }
        private void SetDefaultExplosion()
        {
            var bitmap = SplashKit.LoadBitmap("defaultExplosion", "Explosions/defaultExplosion.png"); 
            var cellDetails = new int[]{180, 180, 20, 1, 20};
            _image = new AnimatedImage("defaultExplosion", "defaultExplosion", bitmap, cellDetails);
        }
        public override void Draw()
        {
            _image.Draw(X, Y);
        } 
        public override void Update()
        {
            Y += 2;
        }
    }
}
    





    























