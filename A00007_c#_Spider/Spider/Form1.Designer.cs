namespace Spider
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._webBrowser = new System.Windows.Forms.WebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            this.lbAppState = new System.Windows.Forms.ListBox();
            this.lblState = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.Label();
            this.txtDataPath = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _webBrowser
            // 
            this._webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._webBrowser.Location = new System.Drawing.Point(152, 230);
            this._webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this._webBrowser.Name = "_webBrowser";
            this._webBrowser.Size = new System.Drawing.Size(269, 67);
            this._webBrowser.TabIndex = 0;
            this._webBrowser.Visible = false;
            this._webBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this._webBrowser_Navigated);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(390, 322);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "开始";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbAppState
            // 
            this.lbAppState.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbAppState.FormattingEnabled = true;
            this.lbAppState.ItemHeight = 12;
            this.lbAppState.Location = new System.Drawing.Point(6, 8);
            this.lbAppState.Name = "lbAppState";
            this.lbAppState.ScrollAlwaysVisible = true;
            this.lbAppState.Size = new System.Drawing.Size(446, 220);
            this.lbAppState.TabIndex = 3;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(4, 261);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(35, 12);
            this.lblState.TabIndex = 4;
            this.lblState.Text = "label1";
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(4, 287);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(337, 12);
            this.lblUrl.TabIndex = 5;
            this.lblUrl.Text = "https://www.jp.usedmachinery.bz/machines/general_view/149844";
            // 
            // txtDataPath
            // 
            this.txtDataPath.Enabled = false;
            this.txtDataPath.Location = new System.Drawing.Point(69, 237);
            this.txtDataPath.Name = "txtDataPath";
            this.txtDataPath.Size = new System.Drawing.Size(329, 19);
            this.txtDataPath.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(404, 235);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "浏览";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "存盘路径";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 313);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(369, 84);
            this.label2.TabIndex = 9;
            this.label2.Text = "注解:\r\n\r\n1.存盘路径如果未选择，程序将自动在TEMP目录下生成Data文件夹。\r\n且会从零开始爬数据，如果存盘路径选择。则会从上次爬的数据上继续\r\n往下爬数" +
    "据。\r\n2.如果想更新某个公司数据，请删除对应公司文件夹。\r\n3.如果想从新获取公司LIST，请删除Data目录下LIST.TXT";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(390, 368);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 40);
            this.button3.TabIndex = 10;
            this.button3.Text = "继续";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(309, 368);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 40);
            this.button4.TabIndex = 11;
            this.button4.Text = "下一个公司停止";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 420);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtDataPath);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.lbAppState);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._webBrowser);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spider";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser _webBrowser;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lbAppState;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TextBox txtDataPath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

