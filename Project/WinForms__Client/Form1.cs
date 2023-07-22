using ConsoleClientForTest;

namespace WinForms__Client
{
    public partial class Form1 : Form
    {
        AsyncClient asyncClient;

        public Form1()
        {
            InitializeComponent();
            asyncClient = new AsyncClient(listBox1);
        }

        private void button_recive_Click(object sender, EventArgs e)
        {
            if (textBox_nick.Text != null)
                asyncClient.SendMessage("127.0.0.1", 49000, $"Get_message=|={textBox_nick.Text}");
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            if(textBox_message.Text != null)
                if(textBox_nick.Text != null)
                    if(textBox_komy.Text != null)
                    {
                        asyncClient.SendMessage("127.0.0.1", 49000, $"sent_message=|={textBox_komy.Text}=|={textBox_nick.Text}=|={textBox_message.Text}");

                        textBox_message.Text = string.Empty;
                        textBox_komy.Text = string.Empty;
                    }
        }
    }
}