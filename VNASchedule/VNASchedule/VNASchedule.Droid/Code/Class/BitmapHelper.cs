using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Media;

namespace VNASchedule.Droid.Code.Class
{
    public static class BitmapHelper
    {
        public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
        {
            // First we get the the dimensions of the file on disk
            BitmapFactory.Options options = new BitmapFactory.Options
            {
                InPurgeable = true,
                InJustDecodeBounds = true
            };
            BitmapFactory.DecodeFile(fileName, options);

            // Next we calculate the ratio that we need to resize the image by
            // in order to fit the requested dimensions.
            int outHeight = options.OutHeight;
            int outWidth = options.OutWidth;
            int inSampleSize = 1;

            if (outHeight > height || outWidth > width)
            {
                inSampleSize = outWidth > outHeight
                                   ? outHeight / height
                                   : outWidth / width;
            }

            // Now we will load the image and have BitmapFactory resize it for us.
            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;
            Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);
            resizedBitmap = modifyOrientation(resizedBitmap, fileName);
            return resizedBitmap;
        }

        public static Bitmap modifyOrientation(Bitmap bitmap, String image_absolute_path)
        {
            if (System.IO.File.Exists(image_absolute_path))
            {
                ExifInterface ei = new ExifInterface(image_absolute_path);
                int orientation = ei.GetAttributeInt(ExifInterface.TagOrientation, (int)Android.Media.Orientation.Normal);

                switch (orientation)
                {
                    case (int)Android.Media.Orientation.Rotate90:
                        return rotate(bitmap, 90);

                    case (int)Android.Media.Orientation.Rotate180:
                        return rotate(bitmap, 180);

                    case (int)Android.Media.Orientation.Rotate270:
                        return rotate(bitmap, 270);

                    case (int)Android.Media.Orientation.FlipHorizontal:
                        return flip(bitmap, true, false);

                    case (int)Android.Media.Orientation.FlipVertical:
                        return flip(bitmap, false, true);

                    default:
                        return bitmap;
                }
            }
            return null;
        }

        public static Bitmap rotate(Bitmap bitmap, float degrees)
        {
            Matrix matrix = new Matrix();
            matrix.PostRotate(degrees);
            return Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
        }

        public static Bitmap flip(Bitmap bitmap, bool horizontal, bool vertical)
        {
            Matrix matrix = new Matrix();
            matrix.PreScale(horizontal ? -1 : 1, vertical ? -1 : 1);
            return Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
        }
    }
}