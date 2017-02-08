namespace MultiExcelToOneExcel
{
    partial class Frm_Main
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_SelectMultiExcel = new System.Windows.Forms.Button();
            this.txt_MultiExcel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.btn_Gather = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_SelectMultiExcel);
            this.groupBox1.Controls.Add(this.txt_MultiExcel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            // 
            // btn_SelectMultiExcel
            // 
            this.btn_SelectMultiExcel.Location = new System.Drawing.Point(319, 21);
            this.btn_SelectMultiExcel.Name = "btn_SelectMultiExcel";
            this.btn_SelectMultiExcel.Size = new System.Drawing.Size(50, 23);
            this.btn_SelectMultiExcel.TabIndex = 2;
            this.btn_SelectMultiExcel.Text = "选择";
            this.btn_SelectMultiExcel.UseVisualStyleBackColor = true;
            this.btn_SelectMultiExcel.Click += new System.EventHandler(this.btn_SelectMultiExcel_Click);
            // 
            // txt_MultiExcel
            // 
            this.txt_MultiExcel.Location = new System.Drawing.Point(137, 23);
            this.txt_MultiExcel.Name = "txt_MultiExcel";
            this.txt_MultiExcel.ReadOnly = true;
            this.txt_MultiExcel.Size = new System.Drawing.Size(172, 19);
            this.txt_MultiExcel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请选择多个Excel文件：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_Browse);
            this.groupBox2.Controls.Add(this.btn_Gather);
            this.groupBox2.Location = new System.Drawing.Point(10, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 51);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作";
            // 
            // btn_Browse
            // 
            this.btn_Browse.Location = new System.Drawing.Point(306, 18);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(63, 23);
            this.btn_Browse.TabIndex = 1;
            this.btn_Browse.Text = "查看";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // btn_Gather
            // 
            this.btn_Gather.Location = new System.Drawing.Point(237, 19);
            this.btn_Gather.Name = "btn_Gather";
            this.btn_Gather.Size = new System.Drawing.Size(63, 23);
            this.btn_Gather.TabIndex = 0;
            this.btn_Gather.Text = "汇总";
            this.btn_Gather.UseVisualStyleBackColor = true;
            this.btn_Gather.Click += new System.EventHandler(this.btn_Gather_Click);
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 135);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "将多个Excel文件汇总到一个Excel文件";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_SelectMultiExcel;
        private System.Windows.Forms.TextBox txt_MultiExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.Button btn_Gather;
    }
}

