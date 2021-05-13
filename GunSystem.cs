using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class Gun
    {
        public List<Bullet> Bullets{get; private set;}
        private double _coolDownTime = 0;
        private double _coolDownLimit;
        private Bullet.Type _type;
        private bool _hasSound;
        public Gun(double coolDownLimit, Bullet.Type type, bool hasSound)
        {
            Bullets = new List<Bullet>();
            _coolDownLimit = coolDownLimit;
            _type = type;
            _hasSound = hasSound;
        }
        public Gun() : this ( 2.5, Bullet.Type.BlueLaser, false) { }
        public Gun(double coolDownLimit) : this(coolDownLimit, Bullet.Type.BlueLaser, false){ }
        public bool CoolDownEnded => _coolDownTime == 0;
        public void OpenFire(int x, int y, int moveAngle, int imageAngle)
        {
            Bullets.Add(new Bullet(x, y, _type, _hasSound, moveAngle, imageAngle));
            _coolDownTime = _coolDownLimit;
        }
        public void AutoFire(int x, int y, int moveAngle, int imageAngle)
        {
            if (CoolDownEnded)
                OpenFire(x, y, moveAngle, imageAngle);
        }
        private double UpdateCoolDown()=>  (_coolDownTime > 0 ? _coolDownTime - 1/(double)60 : 0);
        public void DrawBullets() => Bullets.ForEach(bullet => bullet.Draw());
        public void Update()
        {
            foreach(var bullet in Bullets.ToArray())
            {
                bullet.Update();
                if (bullet.Y < 0 || bullet.Y > Global.Height) Bullets.Remove(bullet);
            } 
            _coolDownTime = UpdateCoolDown();
        }
    }
}