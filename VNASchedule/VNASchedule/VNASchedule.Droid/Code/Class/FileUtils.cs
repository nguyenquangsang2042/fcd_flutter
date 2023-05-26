using System;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Java.IO;
using static SQLite.TableMapping;
using Exception = System.Exception;

namespace VNASchedule.Droid.Code.Class
{
    public class FileUtils
    {
        private static Android.Net.Uri contentUri = null;
        private Context context;
        public FileUtils(Context context)
        {
            this.context = context;
        }
        public string GetPath(Android.Net.Uri uri)
        {
            // check here to KITKAT or new version            bool isKitKat = Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat;
            string selection = null;
            string[] selectionArgs = null;
            // DocumentProvider            if (isKitKat)
            {
                // ExternalStorageProvider                if (isExternalStorageDocument(uri))
                {
                    string docId = DocumentsContract.GetDocumentId(uri);
                    string[] split = docId.Split(":");
                    string fullPath = GetPathFromExtSD(split);
                    if (!fullPath.Equals(""))
                    {
                        return fullPath;
                    }
                    else
                    {
                        return null;
                    }
                }
                // DownloadsProvider
                if (isDownloadsDocument(uri))
                {
                    if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
                    {
                        string id;
                        try
                        {
                            ICursor cursor = context.ContentResolver.Query(uri, new string[] { MediaStore.MediaColumns.DisplayName }, null, null, null);
                            if (cursor != null)
                            {
                                string fileName = cursor.GetString(0);
                                string path = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/Download/" + fileName;
                                if (!string.IsNullOrEmpty(path))
                                {
                                    return path;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        id = DocumentsContract.GetDocumentId(uri);
                        if (!string.IsNullOrEmpty(id))
                        {
                            if (id.StartsWith("raw:"))
                            {
                                return id.Replace("raw:", "");
                            }
                            string[] contentUriPrefixesToTry = new string[]{
                                "content://downloads/public_downloads",
                                "content://downloads/my_downloads"                            };
                            foreach (var contentUriPrefix in contentUriPrefixesToTry)
                            {
                                try
                                {
                                    Android.Net.Uri contentUri = ContentUris.WithAppendedId(Android.Net.Uri.Parse(contentUriPrefix), long.Parse(id));
                                    return GetDataColumn(context, contentUri, null, null);
                                }
                                catch (Exception ex)
                                {
                                    return uri.Path.Replace("^/document/raw:", "").Replace("^raw:", "");
                                }
                            }
                        }
                    }
                    else
                    {
                        string id = DocumentsContract.GetDocumentId(uri);
                        if (id.StartsWith("raw:"))
                        {
                            return id.Replace("raw:", "");
                        }
                        try
                        {
                            contentUri = ContentUris.WithAppendedId(
                                    Android.Net.Uri.Parse("content://downloads/public_downloads"), long.Parse(id));
                        }
                        catch (Java.Lang.NumberFormatException e)
                        {
                            e.PrintStackTrace();
                        }
                        if (contentUri != null)
                        {
                            return GetDataColumn(context, contentUri, null, null);
                        }
                    }
                }
                else
                {
                    if ("content".Equals(uri.Scheme.ToLower()))
                    {
                        string[] projection = {
                        MediaStore.Images.Media.InterfaceConsts.Data                  };
                        ICursor cursor;
                        try
                        {
                            cursor = context.ContentResolver.Query(uri, projection, selection, selectionArgs, null);
                            int column_index = cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.Data);
                            if (cursor.MoveToFirst())
                            {
                                return cursor.GetString(column_index);
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
            }
            return null;
        }
        private string GetDataColumn(Context context, Android.Net.Uri uri, string selection, string[] selectionArgs)
        {
            ICursor cursor = null;
            string column = "_data";
            string[] projection = { column };
            try
            {
                cursor = context.ContentResolver.Query(uri, projection,
                        selection, selectionArgs, null);
                if (cursor != null && cursor.MoveToFirst())
                {
                    int index = cursor.GetColumnIndexOrThrow(column);
                    return cursor.GetString(index);
                }
            }
            finally
            {
                if (cursor != null)
                    cursor.Close();
            }
            return null;
        }
        private string GetPathFromExtSD(string[] pathData)
        {
            string type = pathData[0];
            string relativePath = "/" + pathData[1];
            string fullPath;
            if ("primary".Equals(type.ToLower()))
            {
                fullPath = Android.OS.Environment.ExternalStorageDirectory + relativePath;
                if (fileExists(fullPath))
                {
                    return fullPath;
                }
            }
            fullPath = Java.Lang.JavaSystem.Getenv("SECONDARY_STORAGE") + relativePath;
            if (fileExists(fullPath))
            {
                return fullPath;
            }
            fullPath = Java.Lang.JavaSystem.Getenv("EXTERNAL_STORAGE") + relativePath;
            if (fileExists(fullPath))
            {
                return fullPath;
            }
            return fullPath;
        }
        private bool fileExists(String filePath)
        {
            Java.IO.File file = new Java.IO.File(filePath);
            return file.Exists();
        }
        private bool isExternalStorageDocument(Android.Net.Uri uri)
        {
            return "com.android.externalstorage.documents".Equals(uri.Authority);
        }
        private bool isDownloadsDocument(Android.Net.Uri uri)
        {
            return "com.android.providers.downloads.documents".Equals(uri.Authority);
        }
        private bool isMediaDocument(Android.Net.Uri uri)
        {
            return "com.android.providers.media.documents".Equals(uri.Authority);
        }
        private bool isGooglePhotosUri(Android.Net.Uri uri)
        {
            return "com.google.android.apps.photos.content".Equals(uri.Authority);
        }
        public bool isWhatsAppFile(Android.Net.Uri uri)
        {
            return "com.whatsapp.provider.media".Equals(uri.Authority);
        }
        private bool isGoogleDriveUri(Android.Net.Uri uri)
        {
            return "com.google.android.apps.docs.storage".Equals(uri.Authority) || "com.google.android.apps.docs.storage.legacy".Equals(uri.Authority);
        }

        public File getFile(Android.Net.Uri uri)
        {
            File destinationFilename = new File(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) +"/"+ QueryName(context, uri));
            try
            {
                System.IO.Stream ins = context.ContentResolver.OpenInputStream(uri);
                CreateFileFromStream(ins, destinationFilename);
            }
            catch (Exception ex)
            {
            }
            return destinationFilename;
        }
        public void CreateFileFromStream(System.IO.Stream ins, File destination)
        {
            try
            {
                OutputStream os = new FileOutputStream(destination);
                byte[] buffer = new byte[4096];
                int length;
                while ((length = ins.Read(buffer)) > 0)
                {
                    os.Write(buffer, 0, length);
                }
                os.Flush();
            }
            catch (Exception ex)
            {
            }
        }
        public string QueryName(Context context, Android.Net.Uri uri)
        {
            string result = null;
            if (uri.Scheme.ToLower().Equals("content"))
            {
                try
                {
                    ICursor cursor = context.ContentResolver.Query(uri, null, null, null, null);
                    if (cursor != null && cursor.MoveToFirst())
                    {
                        int nameIndex = cursor.GetColumnIndex(OpenableColumns.DisplayName);
                        result = cursor.GetString(nameIndex);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            if (result == null)
            {
                result = uri.LastPathSegment;
            }
            return result;
        }
    }
}