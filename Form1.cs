namespace discordwindow読み上げ;

using System.Diagnostics;
using System;

public partial class Form1 : Form
{
    private bool tune常に前面に表示する = false;
    public Form1()
    {
        InitializeComponent();
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        this.TopMost= checkBox1.Checked;
    }
    
    

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private async void  button1_Click(object sender, EventArgs e)
    {
        tune常に前面に表示する = checkBox1.Checked;
        string text=textBox2.Text;
        string data = "";

        try
        {
            data = Post(textBox2.Text);
             if (data== "error")
            {
                label6.Text = "idが有効ではありません！\nまたは、サーバーの状態がおかしいかもしれません。もういちど開始してみてください。";
                return;
            }
            else
            { 
            label6.Text = data;
        }
        }
        catch(System.Net.WebException ex)
        {
            label6.Text = "サーバーを立てる際にエラーが発生しました。idが有効でなかったり、サーバーの状態がおかしい可能性があります。もういちど開始してみてください。\n"+ex.ToString();
            return;
        }
        catch(Exception ex)
        {
            data = "error";
            label6.Text = ex.ToString();
        }

        if (data != "error")
        {
            this.Hide();
            bool[] bools =new bool[] { tune常に前面に表示する, checkBox3.Checked, checkBox4.Checked };
            

            Form2 form = new Form2(data,textBox2.Text,bools,this, (float)trackBar1.Value / (float)trackBar1.Maximum);
            form.ShowDialog();
            
        }
       
        



    }

    private string Post(string content)
    {

        string url = "https://djs-v13.forestfamily4.repl.co/get";

        System.Net.WebClient wc = new System.Net.WebClient();
        //NameValueCollectionの作成
        System.Collections.Specialized.NameValueCollection ps =
            new System.Collections.Specialized.NameValueCollection();
        //送信するデータ（フィールド名と値の組み合わせ）を追加
        ps.Add("content", content);
        //データを送信し、また受信する
        byte[] resData = wc.UploadValues(url, ps);
        wc.Dispose();

        //受信したデータを表示する
        string resText = System.Text.Encoding.UTF8.GetString(resData);
        return resText;
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

   
    private void pictureBox1_Click(object sender, EventArgs e)
    {

    }

    private void label6_Click(object sender, EventArgs e)
    {

    }

    
}



