using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SkinCursorSizeChanger
{
    public class SkinUtil 
    {
        public static String[] get_skins(string path) 
        {
            var directories = Directory.GetDirectories(path+"\\skins");
            List<String> skins = new List<String>();
            foreach (string dir in directories) {
                if (File.Exists(dir+"/cursor.png")) {
                    skins.Add(dir.Split("\\").Last());
                }
            }
            return skins.ToArray();
        }

        private static void resize_img(string src_path, string dest_path, float multiplier) {
            using (Image src = Image.FromFile(src_path)) {
                var w = Math.Round(src.Width * multiplier);
                var h = Math.Round(src.Height * multiplier);

                using (Bitmap dst = new Bitmap((int)w, (int)h))
                using (Graphics g = Graphics.FromImage(dst)) {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(src, 0, 0, dst.Width, dst.Height);
                    dst.Save(dest_path, ImageFormat.Png);
                }
            }
        }

        public static void edit_cursor(string path, string skin_name, float multiplier) {
            var final_path = path + "\\skins\\" + skin_name;
            var og_cursor = final_path + "\\cursor.bak";
            var new_cursor = final_path + "\\cursor.png";
            var og_cursortrail = final_path + "\\cursortrail.bak";
            var new_cursortrail = final_path + "\\cursortrail.png";
            var og_cursor2x = final_path + "\\cursor@2x.bak";
            var new_cursor2x = final_path + "\\cursor@2x.png";
            var og_cursortrail2x = final_path + "\\cursortrail@2x.bak";
            var new_cursortrail2x = final_path + "\\cursortrail@2x.png";
            if (!File.Exists(og_cursor) || !File.Exists(og_cursortrail)) { 
                File.Copy(new_cursor, og_cursor);
                File.Copy(new_cursortrail, og_cursortrail);
            }
            resize_img(og_cursor, new_cursor, multiplier);
            resize_img(og_cursortrail, new_cursortrail, multiplier);
            if (File.Exists(new_cursor2x)) {
                if (!File.Exists(og_cursor2x) || !File.Exists(og_cursortrail2x)) {
                    File.Copy(new_cursor2x, og_cursor2x);
                    File.Copy(new_cursortrail2x, og_cursortrail2x);
                }
                resize_img(og_cursor2x, new_cursor2x, multiplier);
                resize_img(og_cursortrail2x, new_cursortrail2x, multiplier);
            }
        }

    }
}
