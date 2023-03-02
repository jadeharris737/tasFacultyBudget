namespace SDDH1_CODE_JADEHARRIS
{
    partial class frm_addUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_addUser));
            this.txt_password = new System.Windows.Forms.TextBox();
            this.cmb_role = new System.Windows.Forms.ComboBox();
            this.btn_addUser = new System.Windows.Forms.Button();
            this.btn_generate = new System.Windows.Forms.Button();
            this.lbl_title_role = new System.Windows.Forms.Label();
            this.lbl_title_password = new System.Windows.Forms.Label();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.lbl_title_username = new System.Windows.Forms.Label();
            this.lbl_title_stage = new System.Windows.Forms.Label();
            this.cmb_stage = new System.Windows.Forms.ComboBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.pnl_topBorder = new System.Windows.Forms.Panel();
            this.lbl_title_subject = new System.Windows.Forms.Label();
            this.cmb_subject = new System.Windows.Forms.ComboBox();
            this.lbl_showpass = new System.Windows.Forms.Label();
            this.pnl_topBorder.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(32, 134);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(122, 20);
            this.txt_password.TabIndex = 36;
            this.txt_password.UseSystemPasswordChar = true;
            // 
            // cmb_role
            // 
            this.cmb_role.FormattingEnabled = true;
            this.cmb_role.Items.AddRange(new object[] {
            "Principal",
            "Head Teacher",
            "Teacher",
            "Teacher\'s Aid"});
            this.cmb_role.Location = new System.Drawing.Point(32, 196);
            this.cmb_role.Name = "cmb_role";
            this.cmb_role.Size = new System.Drawing.Size(254, 21);
            this.cmb_role.TabIndex = 35;
            this.cmb_role.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_role_KeyPress);
            // 
            // btn_addUser
            // 
            this.btn_addUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_addUser.FlatAppearance.BorderSize = 0;
            this.btn_addUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_addUser.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_addUser.Location = new System.Drawing.Point(74, 365);
            this.btn_addUser.Name = "btn_addUser";
            this.btn_addUser.Size = new System.Drawing.Size(167, 32);
            this.btn_addUser.TabIndex = 33;
            this.btn_addUser.Text = "ADD USER";
            this.btn_addUser.UseVisualStyleBackColor = false;
            this.btn_addUser.Click += new System.EventHandler(this.btn_addUser_Click);
            // 
            // btn_generate
            // 
            this.btn_generate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_generate.FlatAppearance.BorderSize = 0;
            this.btn_generate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_generate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_generate.Location = new System.Drawing.Point(151, 134);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(75, 20);
            this.btn_generate.TabIndex = 34;
            this.btn_generate.Text = "GENERATE";
            this.btn_generate.UseVisualStyleBackColor = false;
            this.btn_generate.Click += new System.EventHandler(this.btn_generate_Click);
            // 
            // lbl_title_role
            // 
            this.lbl_title_role.AutoSize = true;
            this.lbl_title_role.Location = new System.Drawing.Point(146, 179);
            this.lbl_title_role.Name = "lbl_title_role";
            this.lbl_title_role.Size = new System.Drawing.Size(36, 13);
            this.lbl_title_role.TabIndex = 31;
            this.lbl_title_role.Text = "ROLE";
            // 
            // lbl_title_password
            // 
            this.lbl_title_password.AutoSize = true;
            this.lbl_title_password.Location = new System.Drawing.Point(131, 117);
            this.lbl_title_password.Name = "lbl_title_password";
            this.lbl_title_password.Size = new System.Drawing.Size(70, 13);
            this.lbl_title_password.TabIndex = 32;
            this.lbl_title_password.Text = "PASSWORD";
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(32, 75);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(254, 20);
            this.txt_username.TabIndex = 30;
            // 
            // lbl_title_username
            // 
            this.lbl_title_username.AutoSize = true;
            this.lbl_title_username.Location = new System.Drawing.Point(133, 56);
            this.lbl_title_username.Name = "lbl_title_username";
            this.lbl_title_username.Size = new System.Drawing.Size(68, 13);
            this.lbl_title_username.TabIndex = 29;
            this.lbl_title_username.Text = "USERNAME";
            // 
            // lbl_title_stage
            // 
            this.lbl_title_stage.AutoSize = true;
            this.lbl_title_stage.Location = new System.Drawing.Point(139, 300);
            this.lbl_title_stage.Name = "lbl_title_stage";
            this.lbl_title_stage.Size = new System.Drawing.Size(43, 13);
            this.lbl_title_stage.TabIndex = 31;
            this.lbl_title_stage.Text = "STAGE";
            // 
            // cmb_stage
            // 
            this.cmb_stage.Enabled = false;
            this.cmb_stage.FormattingEnabled = true;
            this.cmb_stage.Items.AddRange(new object[] {
            "4",
            "5",
            "6"});
            this.cmb_stage.Location = new System.Drawing.Point(32, 316);
            this.cmb_stage.Name = "cmb_stage";
            this.cmb_stage.Size = new System.Drawing.Size(254, 21);
            this.cmb_stage.TabIndex = 35;
            this.cmb_stage.SelectedIndexChanged += new System.EventHandler(this.cmb_stage_SelectedIndexChanged);
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
            // lbl_title_subject
            // 
            this.lbl_title_subject.AutoSize = true;
            this.lbl_title_subject.Location = new System.Drawing.Point(133, 237);
            this.lbl_title_subject.Name = "lbl_title_subject";
            this.lbl_title_subject.Size = new System.Drawing.Size(55, 13);
            this.lbl_title_subject.TabIndex = 31;
            this.lbl_title_subject.Text = "SUBJECT";
            // 
            // cmb_subject
            // 
            this.cmb_subject.FormattingEnabled = true;
            this.cmb_subject.Items.AddRange(new object[] {
            "Technology Mandatory",
            "Food Technology",
            "Textiles",
            "Industrial Technology Timber",
            "Industrial Technology Engineering",
            "Industrial Technology Electronics",
            "Industrial Technology Graphics",
            "Industrial Technology Multimedia",
            "Information Software and Technology",
            "Design and Technology",
            "Information Processes and Technology",
            "Software Design and Development",
            "Engineering Studies",
            "Hospitality VET",
            "Construction VET",
            "Metals and Engineering VET",
            "Textiles and Design",
            "Design and Technology"});
            this.cmb_subject.Location = new System.Drawing.Point(32, 253);
            this.cmb_subject.Name = "cmb_subject";
            this.cmb_subject.Size = new System.Drawing.Size(254, 21);
            this.cmb_subject.TabIndex = 35;
            this.cmb_subject.SelectedIndexChanged += new System.EventHandler(this.cmb_subject_SelectedIndexChanged);
            this.cmb_subject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_subject_KeyPress);
            // 
            // lbl_showpass
            // 
            this.lbl_showpass.BackColor = System.Drawing.SystemColors.GrayText;
            this.lbl_showpass.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_showpass.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_showpass.Location = new System.Drawing.Point(225, 134);
            this.lbl_showpass.Name = "lbl_showpass";
            this.lbl_showpass.Size = new System.Drawing.Size(61, 20);
            this.lbl_showpass.TabIndex = 38;
            this.lbl_showpass.Text = "SHOW";
            this.lbl_showpass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_showpass.MouseEnter += new System.EventHandler(this.lbl_showpass_MouseEnter);
            this.lbl_showpass.MouseLeave += new System.EventHandler(this.lbl_showpass_MouseLeave);
            // 
            // frm_addUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 435);
            this.Controls.Add(this.btn_generate);
            this.Controls.Add(this.lbl_showpass);
            this.Controls.Add(this.pnl_topBorder);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.cmb_subject);
            this.Controls.Add(this.cmb_stage);
            this.Controls.Add(this.cmb_role);
            this.Controls.Add(this.lbl_title_subject);
            this.Controls.Add(this.btn_addUser);
            this.Controls.Add(this.lbl_title_stage);
            this.Controls.Add(this.lbl_title_role);
            this.Controls.Add(this.lbl_title_password);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.lbl_title_username);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_addUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddUser";
            this.pnl_topBorder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.ComboBox cmb_role;
        private System.Windows.Forms.Button btn_addUser;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.Label lbl_title_role;
        private System.Windows.Forms.Label lbl_title_password;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.Label lbl_title_username;
        private System.Windows.Forms.Label lbl_title_stage;
        private System.Windows.Forms.ComboBox cmb_stage;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Panel pnl_topBorder;
        private System.Windows.Forms.Label lbl_title_subject;
        private System.Windows.Forms.ComboBox cmb_subject;
        private System.Windows.Forms.Label lbl_showpass;
    }
}