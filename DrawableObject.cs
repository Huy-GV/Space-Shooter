using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Space_Shooter
{
    public abstract class DrawableObject
    {
        public DrawingOptions Option{ get; set;}
        public Bitmap Bitmap{ get; protected set;}
        public virtual void Draw(int x, int y)
        {
            SplashKit.DrawBitmap(Bitmap,AdjustedX(x) , AdjustedY(y), Option);
        }
        public double AdjustedX(int x) => x - Bitmap.CellWidth / 2;
        public double AdjustedY(int y) => y - Bitmap.CellHeight / 2;
    }
    public class StaticImage : DrawableObject
    {
        public StaticImage(Bitmap bitmap, DrawingOptions option)
        {
            Bitmap = bitmap;
            Option = option;
        }
        public StaticImage(Bitmap bitmap) : this (bitmap, SplashKit.OptionDefaults()){}
    }

    public class AnimatedImage: DrawableObject
    {
        private Animation _animation;
        private DrawingOptions _option;
        private AnimationScript _script;
        public bool AnimationEnded => _animation.Ended;
        public AnimatedImage(string animationScript, string animationName, Bitmap bitmap, int[] cellDetails)
        {
            _script = SplashKit.LoadAnimationScript(animationScript, animationScript + ".txt");
            _animation = _script.CreateAnimation(animationName);
            _option = SplashKit.OptionWithAnimation(_animation);
            Bitmap.SetCellDetails(cellDetails[0], cellDetails[1], cellDetails[2], cellDetails[3] ,cellDetails[4]);
        }
    }
}