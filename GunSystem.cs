using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class GunSystem
    {
        public List<Bullet> Bullets{get; private set;}
        private double _coolDownTime = 0;
        private double _coolDownLimit;
        private Bullet.Type _type;
        private bool _hasSound;
        public GunSystem(double coolDownLimit, Bullet.Type type, bool hasSound)
        {
            Bullets = new List<Bullet>();
            _coolDownLimit = coolDownLimit;
            _type = type;
            _hasSound = hasSound;
        }
        public GunSystem() : this ( 2.5, Bullet.Type.BlueLaser, false) { }
        public GunSystem(double coolDownLimit) : this(coolDownLimit, Bullet.Type.BlueLaser, false){ }
        public bool CoolDownEnded{ get{ return _coolDownTime == 0;}}
        public void OpenFire(int x, int y, int angle)
        {
            Bullets.Add(new Bullet(x, y, _type, _hasSound, angle));
            SetCoolDown();
        }
        public void AutoFire(int x, int y, int angle)
        {
            if (CoolDownEnded)
            {
                Bullets.Add(new Bullet(x, y, _type, _hasSound, angle));
                SetCoolDown();
            }
        }
        private void SetCoolDown()=>_coolDownTime = _coolDownLimit;
        private void UpdateCoolDown()=> _coolDownTime = (_coolDownTime > 0 ? _coolDownTime - 1/(double)60 : 0);
        public void DrawBullets() => Bullets.ForEach(bullet => bullet.Draw());
        public void Update()
        {
            foreach(var bullet in Bullets.ToArray())
            {
                bullet.Update();
                if (bullet.Y < 0 || bullet.Y > Global.Height) Bullets.Remove(bullet);
            } 
            UpdateCoolDown();
        }
    }
}