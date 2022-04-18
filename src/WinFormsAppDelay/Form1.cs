namespace WinFormsAppDelay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using(HttpClient client = new HttpClient())
            {
                string s1= await client.GetStringAsync("https://www.baidu.com");
                textBox1.Text = s1.Substring(0, 100);
                await Task.Delay(5000);
                string s2 = await client.GetStringAsync("https://www.google.com");
                textBox1.Text= s2.Substring(0, 100);
            }
        }
    }
}