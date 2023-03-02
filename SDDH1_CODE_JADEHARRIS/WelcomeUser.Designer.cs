namespace SDDH1_CODE_JADEHARRIS
{
    partial class frm_welcomeUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_welcomeUser));
            this.pnl_topBorder = new System.Windows.Forms.Panel();
            this.btn_closeWelcome = new System.Windows.Forms.Button();
            this.btn_openDocumentation = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.pnl_topBorder.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_topBorder
            // 
            this.pnl_topBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.pnl_topBorder.Controls.Add(this.btn_close);
            this.pnl_topBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_topBorder.Location = new System.Drawing.Point(0, 0);
            this.pnl_topBorder.Name = "pnl_topBorder";
            this.pnl_topBorder.Size = new System.Drawing.Size(421, 22);
            this.pnl_topBorder.TabIndex = 36;
            // 
            // btn_closeWelcome
            // 
            this.btn_closeWelcome.BackColor = System.Drawing.SystemColors.GrayText;
            this.btn_closeWelcome.FlatAppearance.BorderSize = 0;
            this.btn_closeWelcome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_closeWelcome.Font = new System.Drawing.Font("Lucida Sans", 9.75F);
            this.btn_closeWelcome.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_closeWelcome.Location = new System.Drawing.Point(153, 272);
            this.btn_closeWelcome.Name = "btn_closeWelcome";
            this.btn_closeWelcome.Size = new System.Drawing.Size(120, 24);
            this.btn_closeWelcome.TabIndex = 37;
            this.btn_closeWelcome.Text = "OK";
            this.btn_closeWelcome.UseVisualStyleBackColor = false;
            this.btn_closeWelcome.Click += new System.EventHandler(this.btn_updateUser_Click);
            // 
            // btn_openDocumentation
            // 
            this.btn_openDocumentation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_openDocumentation.FlatAppearance.BorderSize = 0;
            this.btn_openDocumentation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_openDocumentation.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_openDocumentation.ForeColor = System.Drawing.Color.White;
            this.btn_openDocumentation.Location = new System.Drawing.Point(12, 181);
            this.btn_openDocumentation.Name = "btn_openDocumentation";
            this.btn_openDocumentation.Size = new System.Drawing.Size(397, 63);
            this.btn_openDocumentation.TabIndex = 39;
            this.btn_openDocumentation.Text = "OPEN APPLICATION DOCUMENTATION PDF";
            this.btn_openDocumentation.UseVisualStyleBackColor = false;
            this.btn_openDocumentation.Click += new System.EventHandler(this.btn_openDocumentation_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(132, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 17);
            this.label1.TabIndex = 40;
            this.label1.Text = "WELCOME NEW USER";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(55, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(311, 103);
            this.label2.TabIndex = 40;
            this.label2.Text = "It is highly recommended you read this PDF of the application\'s overall documenta" +
    "tion and follow the brief tutorials to get started quickly and maximise your use" +
    ".";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_close.FlatAppearance.BorderSize = 0;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_close.Location = new System.Drawing.Point(363, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(58, 22);
            this.btn_close.TabIndex = 3;
            this.btn_close.Text = "X";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // frm_welcomeUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(421, 333);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_openDocumentation);
            this.Controls.Add(this.btn_closeWelcome);
            this.Controls.Add(this.pnl_topBorder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_welcomeUser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangeGlobalBudget";
            this.pnl_topBorder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnl_topBorder;
        private System.Windows.Forms.Button btn_closeWelcome;
        private System.Windows.Forms.Button btn_openDocumentation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_close;
    }
}