
using System.Diagnostics;

namespace discordwindow読み上げ
{
    public partial class message_notconpactmode : UserControl
    {
        public string  name, time, message;
        public string imagepath;
        public message_notconpactmode(string imagepath,string name,string time,string message)
        {
            InitializeComponent();
            groupBox1.Text = name;
            label1.Text = time;
            Debug.WriteLine(message);
            pictureBox1.Image = Image.FromFile(imagepath);
            label7.Text = message;
            this.imagepath = imagepath;
            this.name = name;
            this.time = time;
            this.message = message;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }
        public void init()
        {
            groupBox1.Text = name;
            label1.Text = time;
            label7.Text = message;
            pictureBox1.Image = Image.FromFile(imagepath);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
