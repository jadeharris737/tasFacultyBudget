namespace SDDH1_CODE_JADEHARRIS
{
    partial class frm_sudoForUserHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_sudoForUserHistory));
            this.btn_selectUser = new System.Windows.Forms.Button();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.lbl_title_username = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.pnl_topBorder = new System.Windows.Forms.Panel();
            this.dgv_users = new System.Windows.Forms.DataGridView();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pnl_topBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_users)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_selectUser
            // 
            this.btn_selectUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_selectUser.FlatAppearance.BorderSize = 0;
            this.btn_selectUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_selectUser.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_selectUser.Location = new System.Drawing.Point(70, 396);
            this.btn_selectUser.Name = "btn_selectUser";
            this.btn_selectUser.Size = new System.Drawing.Size(167, 32);
            this.btn_selectUser.TabIndex = 33;
            this.btn_selectUser.Text = "SELECT USER TO SUDO";
            this.btn_selectUser.UseVisualStyleBackColor = false;
            this.btn_selectUser.Click += new System.EventHandler(this.btn_selectUser_Click);
            // 
            // txt_username
            // 
            this.txt_username.Enabled = false;
            this.txt_username.Location = new System.Drawing.Point(25, 365);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(254, 20);
            this.txt_username.TabIndex = 30;
            this.txt_username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_title_username
            // 
            this.lbl_title_username.AutoSize = true;
            this.lbl_title_username.Location = new System.Drawing.Point(116, 349);
            this.lbl_title_username.Name = "lbl_title_username";
            this.lbl_title_username.Size = new System.Drawing.Size(68, 13);
            this.lbl_title_username.TabIndex = 29;
            this.lbl_title_username.Text = "USERNAME";
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_close.FlatAppearance.BorderSize = 0;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_close.Location = new System.Drawing.Point(260, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(58, 22);
            this.btn_close.TabIndex = 3;
            this.btn_close.Text = "X";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // pnl_topBorder
            // 
            this.pnl_topBorder.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pnl_topBorder.Controls.Add(this.btn_close);
            this.pnl_topBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_topBorder.Location = new System.Drawing.Point(0, 0);
            this.pnl_topBorder.Name = "pnl_topBorder";
            this.pnl_topBorder.Size = new System.Drawing.Size(318, 22);
            this.pnl_topBorder.TabIndex = 37;
            // 
            // dgv_users
            // 
            this.dgv_users.AllowUserToAddRows = false;
            this.dgv_users.AllowUserToDeleteRows = false;
            this.dgv_users.AllowUserToResizeColumns = false;
            this.dgv_users.AllowUserToResizeRows = false;
            this.dgv_users.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_users.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_users.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_users.ColumnHeadersVisible = false;
            this.dgv_users.Location = new System.Drawing.Point(41, 44);
            this.dgv_users.Name = "dgv_users";
            this.dgv_users.ReadOnly = true;
            this.dgv_users.RowHeadersVisible = false;
            this.dgv_users.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_users.Size = new System.Drawing.Size(238, 288);
            this.dgv_users.TabIndex = 38;
            this.dgv_users.SelectionChanged += new System.EventHandler(this.dgv_users_SelectionChanged);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.DimGray;
            this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox5.Location = new System.Drawing.Point(0, 454);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(318, 1);
            this.pictureBox5.TabIndex = 69;
            this.pictureBox5.TabStop = false;
            // 
            // frm_sudoForUserHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 455);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.dgv_users);
            this.Controls.Add(this.pnl_topBorder);
            this.Controls.Add(this.btn_selectUser);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.lbl_title_username);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_sudoForUserHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddUser";
            this.pnl_topBorder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_users)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_selectUser;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.Label lbl_title_username;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Panel pnl_topBorder;
        private System.Windows.Forms.DataGridView dgv_users;
        private System.Windows.Forms.PictureBox pictureBox5;
    }
}