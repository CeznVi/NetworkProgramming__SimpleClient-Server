namespace WinForms__Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textBox_nick = new TextBox();
            listBox1 = new ListBox();
            button_send = new Button();
            textBox_message = new TextBox();
            button_recive = new Button();
            textBox_komy = new TextBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 5);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 0;
            label1.Text = "Никнейм";
            // 
            // textBox_nick
            // 
            textBox_nick.Location = new Point(74, 2);
            textBox_nick.Name = "textBox_nick";
            textBox_nick.Size = new Size(196, 23);
            textBox_nick.TabIndex = 1;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(10, 111);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(657, 334);
            listBox1.TabIndex = 3;
            // 
            // button_send
            // 
            button_send.Location = new Point(504, 60);
            button_send.Name = "button_send";
            button_send.Size = new Size(163, 37);
            button_send.TabIndex = 4;
            button_send.Text = "Отправить сообщение";
            button_send.UseVisualStyleBackColor = true;
            button_send.Click += button_send_Click;
            // 
            // textBox_message
            // 
            textBox_message.Location = new Point(10, 68);
            textBox_message.Name = "textBox_message";
            textBox_message.Size = new Size(332, 23);
            textBox_message.TabIndex = 5;
            // 
            // button_recive
            // 
            button_recive.Location = new Point(313, 5);
            button_recive.Name = "button_recive";
            button_recive.Size = new Size(163, 37);
            button_recive.TabIndex = 6;
            button_recive.Text = "Получить сообщение";
            button_recive.UseVisualStyleBackColor = true;
            button_recive.Click += button_recive_Click;
            // 
            // textBox_komy
            // 
            textBox_komy.Location = new Point(358, 68);
            textBox_komy.Name = "textBox_komy";
            textBox_komy.Size = new Size(130, 23);
            textBox_komy.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(389, 50);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 8;
            label2.Text = "Кому";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(671, 450);
            Controls.Add(label2);
            Controls.Add(textBox_komy);
            Controls.Add(button_recive);
            Controls.Add(textBox_message);
            Controls.Add(button_send);
            Controls.Add(listBox1);
            Controls.Add(textBox_nick);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Client";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox_nick;
        private ListBox listBox1;
        private Button button_send;
        private TextBox textBox_message;
        private Button button_recive;
        private TextBox textBox_komy;
        private Label label2;
    }
}