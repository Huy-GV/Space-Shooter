// using System;
// using SplashKitSDK;
// using System.Collections.Generic;

// namespace Space_Shooter
// {
//     public class Spacemine : Enemy
//     {
//         private Animation _animation;
//         private DrawingOptions _option;
//         private AnimationScript _flyScript;
//         public Spacemine(int lastSpacemineX, int lastSpacemineY): base()
//         {
//             Damage = 15;
//             XOffset = 60;
//             YOffset = 60;
//             ExplosionType = Explosion.Type.Default;
//             SetAnimations();
//             SetCoordinates(lastSpacemineX, lastSpacemineY);
//             _movePattern = new VerticalMovement(3, X, Y);
//         }
//         private void SetCoordinates(int lastSpacemineX, int lastSpacemineY)
//         {
//             int randomX = SplashKit.Rnd(0,3);
//             if (lastSpacemineY >= 120)
//             {
//                 X = (2 * randomX + 1) * 100; 
//                 Y = -120;
//             } else
//             {
//                 X = (2 * SplashKit.Rnd(0,3) + 1) * 100;
//                 Y = lastSpacemineY - 240;
//             }
//         }
//         private void SetAnimations()
//         {
//             Bitmap = SplashKit.LoadBitmap("spaceMine", "spaceMine.png");
//             Bitmap.SetCellDetails(120, 120, 2, 1, 2);
//             _flyScript = SplashKit.LoadAnimationScript("FloatingScript", "spacemineScript.txt");
//             _animation = _flyScript.CreateAnimation("floating");
//             _option = SplashKit.OptionWithAnimation(_animation);
//         }
//         public Spacemine() : this(Global.Width, Global.Height){}
//         public override void Draw() { SplashKit.DrawBitmap(Bitmap, AdjustedX, AdjustedY, _option); }
//         public override void Update()
//         { 
//             _animation.Update();
//             _movePattern.Perform();
//             Y = (int)_movePattern.UpdatedY;
//         }
//         private void Move() => Y += 3;
        
//     }
//     public class RedAlienship : Alienship
//     {
//         public RedAlienship(int lastEnemyX, int lastEnemyY) : base(lastEnemyX, lastEnemyY)
//         {
//             Damage = 3;
//             ExplosionType = Explosion.Type.RedLaser;
//             SetAnimations();
//             _gunSystem = new GunSystem(Bullet.Direction.Down, 2);
            
//             _movePattern = new ZigzagMovement(2, X, Y);
//         }
//         public RedAlienship() : this(Global.Width, Global.Height) { }
//         private void SetAnimations()
//         {
//             XOffset = 55;
//             YOffset = 53;
//             Bitmap = SplashKit.LoadBitmap("RedAlienship", "Alienships/RedAlienship.png");
//         }
//         public override void Draw()
//         { 
//             SplashKit.DrawBitmap(Bitmap, AdjustedX, AdjustedY); 
//             _gunSystem.DrawBullets();
//         }
//         public override void Update()
        
//             if (Y >= 0) _gunSystem.AutoFire(X, Y);
//             _gunSystem.Update();
//             _movePattern.Perform();
//             Y = (int)_movePattern.UpdatedY;
//             X = (int)_movePattern.UpdatedX;
//         }
//     }
//     public class PurpleAlienship : Alienship
//     {
//         public PurpleAlienship(int lastEnemyX, int lastEnemyY) : base(lastEnemyX, lastEnemyY)
//         {
//             Damage = 4;
//             ExplosionType = Explosion.Type.Fire;
//             SetAnimations();
//             _gunSystem = new GunSystem(Bullet.Direction.Down, 3);
//             _movePattern = new HorizontalMovement(2, 3, X, Y);
//         }
//         public PurpleAlienship() : this(Global.Width, Global.Height) { }
//         private void SetAnimations()
//         {
//             XOffset = 55;
//             YOffset = 55;
//             Bitmap = SplashKit.LoadBitmap("PurpleAlienship", "Alienships/PurpleAlienship.png");
//         }
//         public override void Draw()
//         { 
//             SplashKit.DrawBitmap(Bitmap, AdjustedX, AdjustedY); 
//             _gunSystem.DrawBullets();
//         }
//         public override void Update()
//         {
//             if (Y >= 0) _gunSystem.AutoFire(X, Y);
//             _gunSystem.Update();
//             _movePattern.Perform();
//             Y = (int)_movePattern.UpdatedY;
//             X = (int)_movePattern.UpdatedX;
//         }
//     }
//     public class BlueAlienship : Alienship
//     {
//         public BlueAlienship(int lastEnemyX, int lastEnemyY) : base(lastEnemyX, lastEnemyY)
//         {
//             Damage = 5;
//             ExplosionType = Explosion.Type.Fire;
//             SetAnimations();
//             _gunSystem = new GunSystem(Bullet.Direction.Down, 3);
//             _movePattern = new VerticalMovement(3, X, Y);
//         }
//         public BlueAlienship() : this(Global.Width, Global.Height) { }
//         private void SetAnimations()
//         {
//             XOffset = 40;
//             YOffset = 55;
//             Bitmap = SplashKit.LoadBitmap("BlueAlienship", "Alienships/BlueAlienship.png");
//         }
//         public override void Draw()
//         { 
//             SplashKit.DrawBitmap(Bitmap, AdjustedX, AdjustedY); 
//             _gunSystem.DrawBullets();
//         }
//         public override void Update()
//         {
//             if (Y >= 0 && Y <= Global.Height / 2) _gunSystem.AutoFire(X, Y);
//             _gunSystem.Update();
//             _movePattern.Perform();
//             Y = (int)_movePattern.UpdatedY;
//             X = (int)_movePattern.UpdatedX;
//         }
//     }
//     public class Asteroid : Enemy
//     {
//         public Asteroid(int lastAsteroidX, int lastAsteroidY) : base()
//         {
//             SetCoordinates(lastAsteroidX, lastAsteroidY);
//             _movePattern = new VerticalMovement(4, X, Y);
//             XOffset = 45;
//             YOffset = 45;
//             Damage = 4;
//             ExplosionType = Explosion.Type.Fire;
//             Bitmap = (SplashKit.Rnd(0, 2) == 0) ? SplashKit.LoadBitmap("asteroid1", "Asteroids/grayAsteroid.png") : SplashKit.LoadBitmap("asteroid2", "Asteroids/brownAsteroid.png");
//         }
//         public Asteroid() : this(Global.Width, Global.Height){}
//         public void SetCoordinates(int lastAsteroidX, int lastAsteroidY)
//         {
//             if (lastAsteroidY >= 90)
//             {
//                 X = (2 * SplashKit.Rnd(0, 6) + 1) * 50; 
//                 Y = -45;
//             } else
//             {
//                 X = (2 * SplashKit.Rnd(0, 6) + 1) * 50;
//                 Y = lastAsteroidY - 90;
//             }
//         }
//         private void Move() => Y += 4; 
//         public override void Update()
//         {
//             _movePattern.Perform();
//             Y = (int)_movePattern.UpdatedY;
//         } 
//     }
//     public class Player : GameObject
//     {
//         public enum ShipType
//         {
//             Versatile,
//             Agile,
//             Armoured
//         }
//         public double CoolDown{get; private set;}
//         public int Health{get; private set;}
//         private GunSystem _gunSystem;
//         public List<Bullet> Bullets{ get { return _gunSystem.Bullets; }}
//         private Animation _animation;
//         private DrawingOptions _option;
//         private AnimationScript _flyScript;
//         private Explosion _damageExplosion;
//         private SoundEffect _laserSound = SplashKit.LoadSoundEffect("laserSound", "laser.mp3");
//         public Player(int option){
//             X = Global.Width / 2;
//             Y = Global.Height * 4 / 5;
//             XOffset = 45;
//             YOffset = 45;       
//             SetAnimation((ShipType)option);
//         }
//         public void SetAnimation(ShipType type)
//         {
//             switch(type)
//             {
//                 case ShipType.Versatile:
//                     SetVersatileShip();
//                     break;
//                 case ShipType.Agile:
//                     SetAgileShip();
//                     break;
//                 default:
//                     SetArmouredShip();
//                     break;
//             }
//         }
//         private void SetVersatileShip()
//         {
//             Health = 100;
//             Bitmap = SplashKit.LoadBitmap("player1", "PlayerSprites/VersatileShipSprites.png");
//             Bitmap.SetCellDetails(90, 90, 5, 1, 5);
//             _flyScript = SplashKit.LoadAnimationScript("FlyingScript1", "spaceship1Script.txt");
//             _animation = _flyScript.CreateAnimation("flying");
//             _option = SplashKit.OptionWithAnimation(_animation);
//             _gunSystem = new GunSystem(Bullet.Direction.Up, 1.25, Bullet.Type.RedBeam, true);
//         }
//         private void SetAgileShip()
//         {  
//             Health = 50;
//             Bitmap = SplashKit.LoadBitmap("player2", "PlayerSprites/AgileShipSprites.png");
//             Bitmap.SetCellDetails(90, 90, 4, 1, 4);
//             _flyScript = SplashKit.LoadAnimationScript("FlyingScript2", "spaceship2Script.txt");
//             _animation = _flyScript.CreateAnimation("flying");
//             _option = SplashKit.OptionWithAnimation(_animation);
//             _gunSystem = new GunSystem(Bullet.Direction.Up, 1, Bullet.Type.RedBeam, true);
//         }
//         private void SetArmouredShip()
//         {
//             Health = 150;
//             Bitmap = SplashKit.LoadBitmap("player3", "PlayerSprites/ArmouredShipSprites.png");
//             Bitmap.SetCellDetails(90, 90, 8, 1, 8);
//             _flyScript = SplashKit.LoadAnimationScript("FlyingScript3", "spaceship3Script.txt");
//             _animation = _flyScript.CreateAnimation("flying");
//             _option = SplashKit.OptionWithAnimation(_animation);
//             _gunSystem = new GunSystem(Bullet.Direction.Up, 1.5, Bullet.Type.RedBeam, true);
//         }
//         public override void Draw()
//         { 
//             SplashKit.DrawBitmap(Bitmap,AdjustedX,AdjustedY,_option );
//             DrawBullets();
//             DrawDamage();    
//         }
//         private void DrawDamage(){ if (_damageExplosion != null) _damageExplosion.Draw();}
//         private void UpdateAnimation() => _animation.Update();
//         public void MoveLeft() => X -= 4;
//         public void MoveRight() => X += 4;
//         public void MoveUp() => Y -= 4;
//         public void MoveDown() => Y += 4;
//         public void Shoot() 
//         { 
//             if (_gunSystem.CoolDownEnded) _gunSystem.OpenFire(X, Y);
//         }
//         public override void Update()
//         {
//             UpdateAnimation();
//             UpdateDamageExplosion();
//             _gunSystem.Update();
//         }
//         private void UpdateBullets()
//         {
//             foreach(var bullet in Bullets.ToArray())
//             {
//                 bullet.Update();
//                 if (bullet.Y < -bullet.AdjustedY) Bullets.Remove(bullet);
//             }
//         }
//         private void UpdateDamageExplosion()
//         {
//             if (_damageExplosion != null)
//             { 
//                 _damageExplosion.Update();
//                 if (_damageExplosion.AnimationEnded()) _damageExplosion = null;
//             }
//         }
//         public void LoseHealth(int damage) => Health -= damage;
//         private void DrawBullets()
//         {
//             foreach(var bullet in Bullets.ToArray()) bullet.Draw(); 
//         }
//         public void CheckEnemyBullets(List<Bullet> enemyBullets)
//         {
//             foreach(var bullet in enemyBullets.ToArray())
//             {
//                 if (bullet.HitTarget(this))
//                 {
//                     Health -= 10;
//                     _damageExplosion = new Explosion(X, Y, Explosion.Type.BlueLaser);
//                     enemyBullets.Remove(bullet);
//                 }
//             }
//         }
//         public bool CollideWith(GameObject gameObject)
//         {
//             return (SplashKit.BitmapCollision(
//                 Bitmap, AdjustedX, AdjustedY,
//                 gameObject.Bitmap, gameObject.AdjustedX, gameObject.AdjustedY));
//         }
//     }
//     public abstract class MovePattern
//     {
//         protected int _direction;
//         protected Point2D _position;
//         public int UpdatedX { get{ return (int)_position.X;}}
//         public int UpdatedY { get{ return (int)_position.Y;}}
//         public int Speed{get; protected set;}
//         public MovePattern(int speed, double x, double y)
//         {
//             Speed = speed;
//             _position = new Point2D();
//             _position.Y = y;
//             _position.X = x;
//             //direction is either 1 or -1
//             _direction = SplashKit.Rnd(2) * 2 - 1;
//         }
//         public abstract void Perform();
//     }
//     public class HorizontalMovement : MovePattern
//     {
//         private int _verticalLimit, _horizontalSpeed;
//         public HorizontalMovement(int horizontalSpeed, int verticalSpeed, double x, double y) : base(verticalSpeed, x ,y) 
//         { 
//             _horizontalSpeed = verticalSpeed;
//             _verticalLimit = (Global.Width / 2) / (SplashKit.Rnd(4) + 1);
//         }
//         public override void Perform()
//         {
//             if (_position.Y < _verticalLimit) _position.Y += Speed;
//             else
//             {
//                 _position.X += _direction * _horizontalSpeed;
//                 if (_position.X >= Global.Width - 5 || _position.X <= 5) _direction *= -1;
//             }
//         }
//     }
//     public class VerticalMovement : MovePattern
//     {
//         public VerticalMovement(int speed, double x, double y) : base(speed, x, y) {}
//         public override void Perform()
//         {
//             _position.Y += Speed;
//         }
//     }

//     public class ZigzagMovement : MovePattern
//     {
//         private int _verticalLimit, _verticalDirection;
//         private bool _firstCrossing;
//         public ZigzagMovement(int speed, double x, double y) : base(speed, x, y)
//         {
//             _verticalDirection = 1;
//             _verticalLimit = Global.Width / 2;
//             _firstCrossing = true;
//         }
//         public override void Perform()
//         {
//             _position.Y += Speed * _verticalDirection;
//             _position.X += _direction * Speed;
//             if (_position.X >= Global.Width - 5 || _position.X <= 5) _direction *= -1;
//             if (_position.Y >= _verticalLimit)
//             {
//                 if (_firstCrossing) _firstCrossing = false;
//                 _verticalDirection *= -1;
//             } else if (_position.Y <= 0 && !_firstCrossing) _verticalDirection *= -1;
//         }   
//     }

//     public class ChargingMovement : MovePattern
//     {
//         private Vector2D _pathVector;
//         public ChargingMovement(int speed, int x, int y) : base(speed,x , y)
//         {
//             var randomX = SplashKit.Rnd(0, 6);
//             var targetX = (2 * randomX + 1) * 50; 
//             var targetY = Global.Height - 10;

//             _pathVector = new Vector2D();
//             _pathVector.X = targetX - x;
//             _pathVector.Y = targetY - y;
//             _pathVector = SplashKit.UnitVector(_pathVector);
//         }
//         public override void Perform()
//         {
//             _position.X += _pathVector.X * Speed;
//             _position.Y += _pathVector.Y * Speed;
//         }
//         public double GetAngle(int x)
//         {
//             var yAxis = new Vector2D();
//             yAxis.X = 0;
//             yAxis.Y = -1;
//             var angle = SplashKit.AngleBetween(yAxis, _pathVector) - 90;
//             return angle;
//         }
//     }
//     public static class Menu
//     {
//         public enum GameScene
//         {
//             MainMenu,
//             PauseMenu,
//             GamePlay,
//             GameOver
//         }
//         public static GameScene Scene = GameScene.MainMenu;
//         public static void ChangeScene(GameScene newScene) => Scene = newScene;
//         public static void DrawMainMenu(int difficulty)
//         {
//             string text;
//             Color color;
//             switch(difficulty)
//             {
//                 case 1:
//                     text = "MEDIUM";
//                     color = Color.Orange;
//                     break;
//                 case 2:
//                     text = "HARD";
//                     color = Color.Red;
//                     break;
//                 default:
//                     text = "EASY";
//                     color = Color.Green;
//                     break;    
//             }
//             SplashKit.DrawText("SPACE SHOOTER", Color.Yellow, Global.BigFont, 60, 100, 50);
//             SplashKit.DrawText("Play", Color.White, Global.MediumFont, 40, 150, 200);
//             SplashKit.DrawText("Difficulty: " + text, color, Global.MediumFont, 40, 150, 300);
//             SplashKit.DrawText("Change Space Ship", Color.Blue, Global.MediumFont, 40, 150, 400);
//         }
//         public static void DrawPauseMenu()
//         {
//             SplashKit.DrawText("Resume", Color.Green, Global.MediumFont, 40, 150, 200);
//             SplashKit.DrawText("Quit to Menu", Color.Red, Global.MediumFont, 40, 150, 300);
//         }
//         public static void DrawGameOver()
//         {
//             SplashKit.DrawText("GAME OVER", Color.Yellow, Global.BigFont, 60, 150, 50);
//             //TODO: DRAW PLAYER SCORE HERE AND CHANGE THE MOUSE CLICK POSITION        
//             SplashKit.DrawText("Quit to Menu", Color.Red, Global.MediumFont, 40, 150, 200);
//         }
//         public static void DrawPauseButton(){ SplashKit.DrawText("Pause", Color.Red, Global.SmallFont, 24, 500, 40);}
//         public static void DrawGameInfo(int playerHealth, double score){
//             SplashKit.DrawText($"Health: {playerHealth}", Color.Green, Global.SmallFont, 24, 20, 40);
//             SplashKit.DrawText($"Score: {(int)score}", Color.Yellow, Global.SmallFont, 24, 20, 70);
//         }
//         public static void DrawPlayerOption(int option)
//         {
//             Bitmap bitmap;
//             switch(option)
//             {
//                 case 1: 
//                     bitmap = SplashKit.LoadBitmap("option2", "options/option2.png");
//                     break;
//                 case 2:
//                     bitmap = SplashKit.LoadBitmap("option3", "options/option3.png");
//                     break;
//                 default:
//                     bitmap = SplashKit.LoadBitmap("option1", "options/option1.png");
//                     break;
//             }
//             bitmap.Draw(150, 500);
//         }
//         public static bool FirstOptionSelected(){return SplashKit.MouseClicked(MouseButton.LeftButton) &&
//                 SplashKit.MouseY() >= 200 && SplashKit.MouseY() <= 250; }
//         public static bool SecondOptionSelected(){return SplashKit.MouseClicked(MouseButton.LeftButton) && 
//                 SplashKit.MouseY() >= 300 && SplashKit.MouseY() <= 350; }
//         public static bool ThirdOptionSelected(){return SplashKit.MouseClicked(MouseButton.LeftButton) && 
//                 SplashKit.MouseY() >= 400 && SplashKit.MouseY() <= 450;}
//         public static bool PauseOptionSelected(){return SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.MouseX() >= 200 && SplashKit.MouseY() <= 55;}
//     }
//     public class KamikazeAlien : Alienship
//     {
//         private DrawingOptions _option;
//         public KamikazeAlien(int lastEnemyX, int lastEnemyY) : base(lastEnemyX, lastEnemyY)
//         {
//             Damage = 12;
//             ExplosionType = Explosion.Type.Fire;
//             _gunSystem = new GunSystem(Bullet.Direction.Down, 4);
//             _movePattern = new ChargingMovement(9, X, Y);
//             SetAnimations();
//         }
//         public KamikazeAlien() : this(Global.Width, Global.Height) { }
//         private void SetAnimations()
//         {
//             XOffset = 40;
//             YOffset = 55;
//             Bitmap = SplashKit.LoadBitmap("KamikazeAlien", "Alienships/KamikazeAlien.png");
//             var angle = (_movePattern as ChargingMovement).GetAngle(X);
//             _option = SplashKit.OptionRotateBmp(angle);
//         }
//         public override void Draw()
//         { 
//             SplashKit.DrawBitmap(Bitmap, AdjustedX, AdjustedY, _option); 
//         }
//         public override void Update()
//         {
//             _movePattern.Perform();
//             Y = (int)_movePattern.UpdatedY;
//             X = (int)_movePattern.UpdatedX;
//         }
//     }
//     public interface IHaveGun
//     {
//         public List<Bullet> Bullets{get;}
//     }
//     public class GunSystem
//     {
//         public List<Bullet> Bullets{get; private set;}
//         private Bullet.Direction _direction;
//         private double _coolDownTime = 0;
//         private double _coolDownLimit;
//         private Bullet.Type _type;
//         private bool _hasSound;
//         public GunSystem(Bullet.Direction direction, double coolDownLimit, Bullet.Type type, bool hasSound)
//         {
//             Bullets = new List<Bullet>();
//             _direction = direction;
//             _coolDownLimit = coolDownLimit;
//             _type = type;
//             _hasSound = hasSound;
//         }
//         public GunSystem() : this (Bullet.Direction.Down, 2.5, Bullet.Type.BlueLaser, false) { }
//         public GunSystem(Bullet.Direction direction, double coolDownLimit) : this( direction, coolDownLimit, Bullet.Type.BlueLaser, false){ }
//         public bool CoolDownEnded{ get{ return _coolDownTime == 0;}}
//         public void OpenFire(int x, int y)
//         {
//             Bullets.Add(new Bullet(x, y, _direction, _type, _hasSound));
//             SetCoolDown();
//         }
//         public void AutoFire(int x, int y)
//         {
//             if (CoolDownEnded)
//             {
//                 Bullets.Add(new Bullet(x, y, _direction, _type, _hasSound));
//                 SetCoolDown();
//             }
//         }
//         private void SetCoolDown()=>_coolDownTime = _coolDownLimit;
//         private void UpdateCoolDown()=> _coolDownTime = (_coolDownTime > 0 ? _coolDownTime - 1/(double)60 : 0);
//         public void DrawBullets(){ foreach( var bullets in Bullets) bullets.Draw(); }
//         public void Update()
//         {
//             foreach(var bullet in Bullets.ToArray())
//             {
//                 bullet.Update();
//                 if (bullet.Y < 0 || bullet.Y > Global.Height) Bullets.Remove(bullet);
//             } 
//             UpdateCoolDown();
//         }
//     }
//     public abstract class GameObject
//     {
//         public int X{get;  protected set;}
//         public int Y{get;  protected set;}
//         protected double XOffset{get; set;}
//         protected double YOffset{get; set;}
//         public double AdjustedX => X - XOffset;
//         public double AdjustedY => Y - YOffset; 
//         public Bitmap Bitmap{get; protected set;}
//         public abstract void Update();
//         public virtual void Draw() => SplashKit.DrawBitmap(Bitmap, AdjustedX, AdjustedY);
//     }
//     public static class Global
//     {
//         public const int Width = 600, Height = 800;
//         public static readonly Font SmallFont = SplashKit.LoadFont("Consolas", "consolab.ttf");
//         public static readonly  Font BigFont = SplashKit.LoadFont("Consolas", "consolab.ttf");
//         public static readonly  Font MediumFont = SplashKit.LoadFont("SegouUI", "segoeuib.ttf");
//     }
//     public static class GameBackground
//     {
//         private static SoundEffect _music = SplashKit.LoadSoundEffect("arcade", "arcade.mp3");
//         private static Bitmap _background = SplashKit.LoadBitmap("space", "space.png");
//         private static List<Explosion> _explosions = new List<Explosion>();
//         //explosions are considered 'background' because they do not affect the logic of the game, they merely add visual effects
//         public static double Score = 0;
//         public static void GainScore(){ Score += 1/(double)60;}
//         public static void DrawBackground()
//         { 
//             SplashKit.ClearScreen(Color.Black);
//             SplashKit.DrawBitmap(_background, 0, 0);
//         }
//         public static void UpdateExplosions()
//         {
//             foreach (var explosion in _explosions.ToArray())
//             {
//                 explosion.Update();
//                 if (explosion.AnimationEnded()) _explosions.Remove(explosion);
//             }
//         }
//         public static void DrawExplosions()
//         {
//             foreach (var explosion in _explosions.ToArray()) explosion.Draw();
//         }
//         public static void PlayMusic(){if (!SplashKit.SoundEffectPlaying("arcade")) SplashKit.PlaySoundEffect("arcade", (float)0.1); }
//         public static void ResetScore() {Score = 0;}
//         public static void CreateExplosion(int x, int y, Explosion.Type type) => _explosions.Add(new Explosion(x, y, type));
//     }
//     public class Explosion : GameObject
//     {
//         public enum Type{
//             RedLaser,
//             BlueLaser,
//             Fire,
//             Default
//         }
//         private Animation _animation;
//         private AnimationScript _explosionScript;
//         private DrawingOptions _option;
//         public bool AnimationEnded(){return (_animation.Ended);}
//         public Explosion(int x, int y, Type type)
//         {
//             var explosionSound = SplashKit.LoadSoundEffect("explosionSound", "explosion.mp3");
//             SplashKit.PlaySoundEffect("explosionSound", (float)0.12);
//             X = x;
//             Y = y;
//             SetAnimations(type);
//         }
//         public void SetAnimations(Type type)
//         {
//             //different types only vary in animation so sub-classing them like alienship is unnecessary
//             switch(type)
//             {
//                 case Type.RedLaser:
//                     SetLaserExplosion();
//                     break;
//                 case Type.Fire:
//                     SetFireExplosion();
//                     break;
//                 case Type.BlueLaser:
//                     SetBlueLaserExplosion();
//                     break; 
//                 default:
//                     SetDefaultExplosion();
//                     break;
//             }
//         }
//         private void SetLaserExplosion()
//         {
//             XOffset = 90;
//             YOffset = 90;
//             Bitmap = SplashKit.LoadBitmap("laserExplosion", "Explosions/laserExplosion.png"); 
//             Bitmap.SetCellDetails(180, 180, 17, 1, 17);
//             _explosionScript = SplashKit.LoadAnimationScript("laserExplosion", "laserExplosion.txt");
//             _animation = _explosionScript.CreateAnimation("laserExplosion");
//             _option = SplashKit.OptionWithAnimation(_animation);
//         }
//         private void SetBlueLaserExplosion()
//         {
//             XOffset = 60;
//             YOffset = 60;
//             Bitmap = SplashKit.LoadBitmap("blueExplosion", "Explosions/blueExplosion.png"); 
//             Bitmap.SetCellDetails(120, 120, 17, 1, 17);
//             _explosionScript = SplashKit.LoadAnimationScript("blueLaserExplosion", "blueLaserExplosion.txt");
//             _animation = _explosionScript.CreateAnimation("blueLaserExplosion");
//             _option = SplashKit.OptionWithAnimation(_animation);
//         }
//         private void SetFireExplosion()
//         {
//             XOffset = 50;
//             YOffset = 50;
//             Bitmap = SplashKit.LoadBitmap("fireExplosion", "Explosions/fireExplosion.png"); 
//             Bitmap.SetCellDetails(100, 100, 15, 1, 15);
//             _explosionScript = SplashKit.LoadAnimationScript("fireExplosion", "fireExplosion.txt");
//             _animation = _explosionScript.CreateAnimation("fireExplosion");
//             _option = SplashKit.OptionWithAnimation(_animation);
//         }
//         private void SetDefaultExplosion()
//         {
//             XOffset = 90;
//             YOffset = 90;
//             Bitmap = SplashKit.LoadBitmap("defaultExplosion", "Explosions/defaultExplosion.png"); 
//             Bitmap.SetCellDetails(180, 180, 20, 1, 20);
//             _explosionScript = SplashKit.LoadAnimationScript("defaultExplosion", "defaultExplosion.txt");
//             _animation = _explosionScript.CreateAnimation("defaultExplosion");
//             _option = SplashKit.OptionWithAnimation(_animation);
//         }
//         public override void Draw() => SplashKit.DrawBitmap( Bitmap,  AdjustedX, AdjustedY,  _option); 
//         public override void Update()
//         {
//             _animation.Update();
//             Y += 2;
//         }
//     }
//     public abstract class Enemy : GameObject
//     {
//         protected MovePattern _movePattern;
//         public Explosion.Type ExplosionType{get; protected set;}  
//         public int Damage{get; protected set;}
//         public bool IsDestroyed{get; protected set;}
//         public void GetDestroyed() => IsDestroyed = true;
//         public void CheckPlayerBullets(List<Bullet> bullets)
//         {
//             foreach(var bullet in bullets.ToArray())
//             {
//                 if (bullet.HitTarget(this))
//                 {
//                     GetDestroyed();
//                     bullets.Remove(bullet);
//                 }
//             }
//         }
//         public Enemy()
//         {
//             IsDestroyed = false;
//         }
//     }
//     public static class Difficulty
//     {
//         public enum Level
//         {
//             Easy,
//             Medium,
//             Hard
//         }
//         public static Dictionary<Type, int> Limit = new Dictionary<Type, int>()
//         {
//             {typeof(BlueAlienship), 3},
//             {typeof(PurpleAlienship), 2},
//             {typeof(RedAlienship), 1},
//             {typeof(KamikazeAlien), 0},
//             {typeof(Asteroid), 7},
//             {typeof(Spacemine), 1}
//         };
//         public static int Index = 0;
//         public static void ChangeLevel()
//         {
//             Index = Index == 2 ? 0 : Index + 1;
//             UpdateEnemyLimit();
//         }
//         public static void UpdateEnemyLimit()
//         {
//             switch((Level)Index)
//             {
//                 case Level.Medium:
//                     Limit[typeof(BlueAlienship)] = 4;
//                     Limit[typeof(PurpleAlienship)] = 3;
//                     Limit[typeof(RedAlienship)] = 2;
//                     Limit[typeof(KamikazeAlien)] = 1;
//                     Limit[typeof(Asteroid)] = 6;
//                     Limit[typeof(Spacemine)] = 2;
//                     break;
//                 case Level.Hard: 
//                     Limit[typeof(BlueAlienship)] = 5;
//                     Limit[typeof(PurpleAlienship)] = 3;
//                     Limit[typeof(RedAlienship)] = 2;
//                     Limit[typeof(KamikazeAlien)] = 3;
//                     Limit[typeof(Asteroid)] = 7;
//                     Limit[typeof(Spacemine)] = 3;
//                     break;
//                 default:
//                     Limit[typeof(BlueAlienship)] = 3;
//                     Limit[typeof(PurpleAlienship)] = 2;
//                     Limit[typeof(RedAlienship)] = 1;
//                     Limit[typeof(KamikazeAlien)] = 0;
//                     Limit[typeof(Asteroid)] = 5;
//                     Limit[typeof(Spacemine)] = 1;
//                     break;
//             }
//         }
//     }
//     public class Bullet : GameObject
//     {
//         public enum Type
//         {
//             RedLaser,
//             BlueLaser,
//             RedBeam
//         }
//         public enum Direction
//         {
//             Up = 1,
//             Down = -1
//         }
//         private int _direction;
//         public Bullet(int x, int y, Direction direction, Type type, bool hasSound){
//             if (hasSound) SplashKit.LoadSoundEffect("laserSound", "laser.mp3").Play();
//             X = x;
//             Y = y;
//             _direction = (int)direction;
//             SetType(type);
//         }
//         private void SetType(Type type)
//         {
//             switch(type)
//             {
//                 case Type.RedLaser:
//                     XOffset = 28;
//                     YOffset = 41;
//                     Bitmap = SplashKit.LoadBitmap("RedLaser", "Bullets/RedLaser.png");
//                     break;
//                 case Type.BlueLaser:
//                     XOffset = 21;
//                     YOffset = 30;
//                     Bitmap = SplashKit.LoadBitmap("BlueLaser", "Bullets/BlueLaser.png");
//                     break;
//                 case Type.RedBeam:
//                     XOffset = 21;
//                     YOffset = 30;
//                     Bitmap = SplashKit.LoadBitmap("RedBeam", "Bullets/RedBeam.png");
//                     break;               
//             }
//         }
//         private void Move() => Y -= 7 * _direction; 
//         public override void Update() => Move(); 
//         public bool HitTarget(GameObject gameObject)
//         {
//             return (SplashKit.BitmapCollision(
//                 Bitmap, AdjustedX, AdjustedY,
//                 gameObject.Bitmap, gameObject.AdjustedX, gameObject.AdjustedY));
//         }
//     }
//     public abstract class Alienship : Enemy, IHaveGun
//     {
//         protected GunSystem _gunSystem;
//         public List<Bullet> Bullets{ get { return _gunSystem.Bullets; }}
//         //alienships all have guns but their fire-rates can vary
//         public Alienship(int lastEnemyX, int lastEnemyY) : base()
//         {
//             int randomX = SplashKit.Rnd(0, 6);
//             if (lastEnemyY >= 100){
//                 X = (2 * randomX + 1) * 50; 
//                 Y = -50;
//             }else
//             {
//                 while ( (2 * randomX + 1) * 50  == lastEnemyX) randomX = SplashKit.Rnd(0, 6);
//                 X = (2 * randomX + 1) * 50; 
//                 Y = lastEnemyY - 100;
//             } 
//         }
//     }
//     class Program
//     {
//         static int _spaceshipChoice = 0;
//         static Player _player;
//         static List<Enemy> _enemies;
//         static Dictionary<Type, int> _enemyAmountByClass = new Dictionary<Type, int>();
//         static Window gameWindow = new Window("Space Shooter", Global.Width, Global.Height);
//         static void Main(string[] args)
//         {
//             GameBackground.ResetScore();
//             RegisterEnemies();
//             _player = new Player(_spaceshipChoice);
//             while (!SplashKit.QuitRequested())
//             {
//                 Update();
//                 Draw();
//                 HandleInputs();
//             }
//         }
//         //add enemy types and their amount so that we can spawn them easily in AddEnemies
//         static void RegisterEnemies()
//         {
//             _enemyAmountByClass[typeof(Asteroid)] = 0;
//             _enemyAmountByClass[typeof(Spacemine)] = 0;
//             _enemyAmountByClass[typeof(BlueAlienship)] = 0;
//             _enemyAmountByClass[typeof(PurpleAlienship)] = 0;
//             _enemyAmountByClass[typeof(RedAlienship)] = 0;
//             _enemyAmountByClass[typeof(KamikazeAlien)] = 0;
//         }
//         static void UpdateEnemyAmount(Type type, int increment) => _enemyAmountByClass[type] += increment;
//         static void ResetGame()
//         {
//             _enemyAmountByClass.Clear();
//             RegisterEnemies();
//             GameBackground.ResetScore();
//         }
//         static void Draw()
//         {                
//             GameBackground.DrawBackground();
//             switch(Menu.Scene)
//             {
//                 case Menu.GameScene.MainMenu:
//                     Menu.DrawMainMenu(Difficulty.Index);
//                     Menu.DrawPlayerOption(_spaceshipChoice);
//                     break;
//                 case Menu.GameScene.PauseMenu:
//                     Menu.DrawPauseMenu();
//                     break;
//                 case Menu.GameScene.GameOver:
//                     Menu.DrawGameOver();
//                     break;
//                 default:
//                     Menu.DrawPauseButton();
//                     Menu.DrawGameInfo((int)_player.Health, GameBackground.Score);
//                     GameBackground.GainScore();
//                     GameBackground.DrawExplosions();
//                     DrawEnemies();
//                     _player.Draw();
//                     break;
//             }
//             SplashKit.RefreshScreen(60);
//         }
//         static void HandleInputs()
//         {
//             //the game has different 'scenes' that can be accessed by buttons displayed on screen
//             switch(Menu.Scene)
//             {
//                 case Menu.GameScene.MainMenu:
//                     if (Menu.FirstOptionSelected())
//                     {
//                         _enemies = new List<Enemy>();
//                         _player = new Player(_spaceshipChoice);
//                         Menu.ChangeScene(Menu.GameScene.GamePlay);
//                     }
//                     else if (Menu.SecondOptionSelected())
//                     {
//                         Difficulty.ChangeLevel();
//                     }
//                     else if (Menu.ThirdOptionSelected())
//                     {
//                         UpdatePlayerChoice();
//                     }
//                     break;
//                 case Menu.GameScene.PauseMenu:
//                     if (Menu.FirstOptionSelected()) 
//                         Menu.ChangeScene(Menu.GameScene.GamePlay);
//                     else if (Menu.SecondOptionSelected())
//                     {
//                         ResetGame();
//                         Menu.ChangeScene(Menu.GameScene.MainMenu);
//                     }
//                     break;
//                 case Menu.GameScene.GameOver:
//                     if (Menu.FirstOptionSelected())
//                     {
//                         ResetGame();
//                         Menu.ChangeScene(Menu.GameScene.MainMenu);
//                     }
//                     break;
//                 default:
//                     HandleKeyboardInputs();
//                     if (Menu.PauseOptionSelected()) Menu.ChangeScene(Menu.GameScene.PauseMenu); 
//                     break;
//             }
//         }
//         static void HandleKeyboardInputs()
//         {
//             if (SplashKit.KeyDown(KeyCode.LeftKey) && _player.X > 0)   _player.MoveLeft();
//             if (SplashKit.KeyDown(KeyCode.RightKey) && _player.X < Global.Width)  _player.MoveRight();
//             if (SplashKit.KeyDown(KeyCode.UpKey) && _player.Y > Global.Height / 2)   _player.MoveUp();
//             if (SplashKit.KeyDown(KeyCode.DownKey) && _player.Y < Global.Height)  _player.MoveDown();
//             if (SplashKit.KeyDown(KeyCode.SpaceKey) && _player.CoolDown == 0) _player.Shoot();
//         }
//         static void Update()
//         {
//             SplashKit.ProcessEvents();
//             GameBackground.PlayMusic();
//             if (Menu.Scene == Menu.GameScene.GamePlay)
//             {
//                 _player.Update();
//                 if (_player.Health <= 0) Menu.ChangeScene(Menu.GameScene.GameOver);
//                 GameBackground.UpdateExplosions();
//                 UpdateEnemies();
//                 AddEnemies();
//             }
//         }
//         static void UpdatePlayerChoice(){ _spaceshipChoice = _spaceshipChoice == 2 ? 0 : _spaceshipChoice + 1; }
//         static void AddEnemies()
//         {
//             int enemyTypeAmount;
//             foreach (var enemyType in _enemyAmountByClass.Keys)
//             {
//                 enemyTypeAmount = _enemyAmountByClass[enemyType];
//                 //generate enemies, their amount and spawning location is dependent on the current amount and closest enemy created
//                 if (SplashKit.Rnd(0, 70) == 0 && enemyTypeAmount < Difficulty.Limit[enemyType])
//                 {
//                     if (enemyTypeAmount == 0) _enemies.Add((Enemy)Activator.CreateInstance(enemyType));
//                     else
//                     {
//                         var lastEnemy = _enemies[_enemies.Count - 1];
//                         Object[] parameters = {lastEnemy.X, lastEnemy.Y};
//                         _enemies.Add((Enemy)Activator.CreateInstance(enemyType, parameters));
//                     }
//                     UpdateEnemyAmount(enemyType, 1);
//                 }
//             }
//         }
//         static void UpdateEnemies()
//         {
//             foreach(Enemy enemy in _enemies.ToArray())
//             {
//                 enemy.Update();
//                 enemy.CheckPlayerBullets(_player.Bullets);
//                 if (enemy.AdjustedY > Global.Height || enemy.IsDestroyed)
//                 {
//                     if (enemy.IsDestroyed) 
//                     {
//                         GameBackground.CreateExplosion(enemy.X, enemy.Y, enemy.ExplosionType);
//                     }
//                     UpdateEnemyAmount(enemy.GetType(), -1);
//                     //because enemy spawning frequency depends on their current amount, we need to update it everytime an enemy is destroyed or spawned
//                     _enemies.Remove(enemy);                     
//                 }
//                 if (_player.CollideWith(enemy))
//                 { 
//                     enemy.GetDestroyed();
//                     _player.LoseHealth(enemy.Damage);
//                 }
//                 if (enemy is IHaveGun) _player.CheckEnemyBullets(((IHaveGun)enemy).Bullets);   
//             }
//         }
//         static void DrawEnemies() {foreach(Enemy enemy in _enemies.ToArray()) enemy.Draw();}
//     }
// }