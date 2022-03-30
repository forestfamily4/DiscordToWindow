using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace discordwindow読み上げ
{
    public partial class Form2 : Form
    {
        private JObject data;
        private string auth;
        private string channelguildid;
        private bool isconpact = false;
        private bool isgazo画像表示 = false;
        private Form1 form1 = null;
        private message_notconpactmode[] boxes = new message_notconpactmode[10];
        public List<avatars> avatarsdata = new List<avatars>();

        public struct avatars
        {
            public string id;
            public string imagepath;
        }

        public Form2(string firstdata, string channelid, bool[] ssettei, Form1 form, float opacity)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.auth = auth;
            this.channelguildid = channelid;
            data = JObject.Parse(firstdata);

            this.BackColor = Color.WhiteSmoke;
            this.TransparencyKey = Color.WhiteSmoke;


            if (ssettei[0]) { this.TopMost = true; }
            else { this.TopMost = false; }
            isconpact = ssettei[1];
            isgazo画像表示 = ssettei[2];

            form1 = form;
            alltime();
            Debug.WriteLine(opacity);
            this.Opacity = opacity;
            //   avatarsdata[0].id = "0";
            //  avatarsdata[0].image = null;

            getinfo();

        }

        private void Form2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs eventargs)
        {
            Debug.Write(eventargs.KeyCode);

        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        void getinfo()
        {

            string datastring = Post(channelguildid);
            data = JObject.Parse(datastring);

            for (int i = 0; i < 10; i++)
            {
                string time = data["data"][i]["time"].ToString();
                var time2 = DateTimeOffset.FromUnixTimeSeconds(Int64.Parse(time.Substring(0, 10)));
                string time3 = time2.Hour + ":" + time2.Minute + ":" + time2.Second;
                var box = new message_notconpactmode(getavarar(data["data"][i]["author"].ToString(), data["data"][i]["avatar"].ToString()), data["data"][i]["name"].ToString(), time3, data["data"][i]["content"].ToString());

                //ディスプレイの高さ
                int h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                //ディスプレイの幅
                int w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

                box.Location = new Point((int)(MathF.Floor(w * 0.6f)), 200 * i);
                this.Controls.Add(box);

                boxes[i] = box;
            }


        }

        void getinfo2()
        {
            string datastring = "";
            try
            {
                datastring = Post(channelguildid);
                if (data == JObject.Parse(datastring)) { return; }
                data = JObject.Parse(datastring);

                for (int i = 0; i < 10; i++)
                {
                    string time = data["data"][i]["time"].ToString();
                    Debug.WriteLine(time);
                    var time2 = DateTimeOffset.FromUnixTimeSeconds(Int64.Parse(time.Substring(0, 10))+9*60*60);
                    string time3 = time2.Hour + ":" + time2.Minute + ":" + time2.Second;
                    boxes[i].imagepath = getavarar(data["data"][i]["author"].ToString(), data["data"][i]["avatar"].ToString());
                    boxes[i].name = data["data"][i]["name"].ToString();
                    boxes[i].time = time3;
                    boxes[i].message = data["data"][i]["content"].ToString();
                    boxes[i].init();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }


        async void alltime()
        {
            await Task.Delay(1000);
            while (true)
            {

                getinfo2();
                await Task.Delay(1000);
                GC.Collect();
            }
        }

        private string Post(string content)
        {

            string url = "https://djs-v13.forestfamily4.repl.co/get";

            System.Net.WebClient wc = new System.Net.WebClient();
            System.Collections.Specialized.NameValueCollection ps =
                new System.Collections.Specialized.NameValueCollection();
            ps.Add("content", content);
            byte[] resData = wc.UploadValues(url, ps);
            wc.Dispose();
            string resText = System.Text.Encoding.UTF8.GetString(resData);
            return resText;
        }


        string getavarar(string id, string url)
        {
            if (isdownloaded(id) == -1)
            {
                WebClient webClient = new WebClient();
                string path = Path.GetTempPath() + @"\discordavatar" + id + ".png";
                webClient.DownloadFile(url, path);
                avatarsdata.Add(new avatars { id = id, imagepath = path });
                return path;
            }
            else
            {
                return avatarsdata[isdownloaded(id)].imagepath;
            }
        }

        void setidandavatar()
        {

        }

        int isdownloaded(string id)
        {
            for (int i = 0; i < avatarsdata.Count; i++)
            {
                if (avatarsdata[i].id == id) { return i; }
            }
            return -1;
        }

    }

}
