using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VNASchedule.Droid.Code.Class
{
    public class CustomImageView : ImageView
    {
        private float radius = 0.0f;
        private Path path;
        private RectF rect;
        Canvas canvas = null;

        public CustomImageView(Context context) : base(context)
        {
            init();
        }

        public CustomImageView(Context context, Android.Util.IAttributeSet attrs) : base(context, attrs)
        {
            init();
        }

        public CustomImageView(Context context, Android.Util.IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            init();
        }

        private void init()
        {
            path = new Path();
        }

        public float Radius
        {
            get => radius;
        }
        public void SetRadius(float radius)
        {
            init();
            Android.Graphics.Drawables.BitmapDrawable bmd = (Android.Graphics.Drawables.BitmapDrawable)this.Drawable;
            Bitmap bm = bmd.Bitmap;

            this.radius = radius;
            Canvas canvas = new Canvas(bm);
            OnDraw(canvas);
        }

        protected override void OnDraw(Canvas canvas)
        {
            rect = new RectF(0, 0, this.Width, this.Height);
            path.AddRoundRect(rect, radius, radius, Path.Direction.Cw);
            canvas.ClipPath(path);

            base.OnDraw(canvas);
        }
    }
}