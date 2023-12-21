using SkinCursorSizeChanger.Properties;

namespace SkinCursorSizeChanger
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            var size = trackBar1.Value / 10.0;
            label1.Text = "Size: " + size + "x";
        }

        private void button1_Click(object sender, EventArgs e) {
            using (var fbd = new FolderBrowserDialog()) {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) {
                    load_skins(fbd.SelectedPath);
                    Properties.Settings.Default.LastPath = fbd.SelectedPath;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            var size = trackBar1.Value / 10.0;
            string current_skin = (string)listBox1.SelectedItem;
            SkinUtil.edit_cursor(Properties.Settings.Default.LastPath, current_skin, (float)size);
        }

        private void load_skins(string path) {
            string[] skins = SkinUtil.get_skins(path);
            listBox1.Items.Clear();
            listBox1.Items.AddRange(skins);
        }
        private void Form1_Load(object sender, EventArgs e) {
            if (Properties.Settings.Default.LastPath.Length > 1) {
                load_skins(Properties.Settings.Default.LastPath);
            }
        }
    }
}