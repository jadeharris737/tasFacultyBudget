namespace SDDH1_CODE_JADEHARRIS
{
    partial class frm_changeSubBudget
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_changeSubBudget));
            this.btn_close = new System.Windows.Forms.Button();
            this.pnl_topBorder = new System.Windows.Forms.Panel();
            this.btn_changesubBudget = new System.Windows.Forms.Button();
            this.txt_subject = new System.Windows.Forms.TextBox();
            this.lbl_subject = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_newSubBudget = new System.Windows.Forms.TextBox();
            this.lbl_newSubBudget = new System.Windows.Forms.Label();
            this.lbl_newGlobalBudgetLeft = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbl_newSubBudgetLeft = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_stage = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_currentSubBudget = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lbl_currentSubBudgetSpent = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lbl_currentSubBudgetLeft = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_currentGlobal = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_currentGlobalBudgetLeft = new System.Windows.Forms.Label();
            this.pnl_topBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_close.FlatAppearance.BorderSize = 0;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_close.Location = new System.Drawing.Point(402, 0);
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
            this.pnl_topBorder.Size = new System.Drawing.Size(460, 22);
            this.pnl_topBorder.TabIndex = 36;
            // 
            // btn_changesubBudget
            // 
            this.btn_changesubBudget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_changesubBudget.FlatAppearance.BorderSize = 0;
            this.btn_changesubBudget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_changesubBudget.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_changesubBudget.Location = new System.Drawing.Point(139, 557);
            this.btn_changesubBudget.Name = "btn_changesubBudget";
            this.btn_changesubBudget.Size = new System.Drawing.Size(167, 32);
            this.btn_changesubBudget.TabIndex = 52;
            this.btn_changesubBudget.Text = "SET SUBJECT BUDGET";
            this.btn_changesubBudget.UseVisualStyleBackColor = false;
            this.btn_changesubBudget.Click += new System.EventHandler(this.btn_changesubBudget_Click);
            // 
            // txt_subject
            // 
            this.txt_subject.Enabled = false;
            this.txt_subject.HideSelection = false;
            this.txt_subject.Location = new System.Drawing.Point(108, 78);
            this.txt_subject.Name = "txt_subject";
            this.txt_subject.Size = new System.Drawing.Size(254, 20);
            this.txt_subject.TabIndex = 51;
            this.txt_subject.TabStop = false;
            // 
            // lbl_subject
            // 
            this.lbl_subject.AutoSize = true;
            this.lbl_subject.Location = new System.Drawing.Point(204, 62);
            this.lbl_subject.Name = "lbl_subject";
            this.lbl_subject.Size = new System.Drawing.Size(55, 13);
            this.lbl_subject.TabIndex = 47;
            this.lbl_subject.Text = "SUBJECT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(87, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 15);
            this.label2.TabIndex = 45;
            this.label2.Text = "$";
            // 
            // txt_newSubBudget
            // 
            this.txt_newSubBudget.Location = new System.Drawing.Point(108, 188);
            this.txt_newSubBudget.Name = "txt_newSubBudget";
            this.txt_newSubBudget.Size = new System.Drawing.Size(254, 20);
            this.txt_newSubBudget.TabIndex = 40;
            this.txt_newSubBudget.TextChanged += new System.EventHandler(this.txt_newSubBudget_TextChanged);
            this.txt_newSubBudget.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_newSubBudget_KeyPress);
            // 
            // lbl_newSubBudget
            // 
            this.lbl_newSubBudget.AutoSize = true;
            this.lbl_newSubBudget.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_newSubBudget.Location = new System.Drawing.Point(136, 171);
            this.lbl_newSubBudget.Name = "lbl_newSubBudget";
            this.lbl_newSubBudget.Size = new System.Drawing.Size(193, 14);
            this.lbl_newSubBudget.TabIndex = 37;
            this.lbl_newSubBudget.Text = "NEW SUBJECT BUDGET ALLOCATED";
            // 
            // lbl_newGlobalBudgetLeft
            // 
            this.lbl_newGlobalBudgetLeft.AutoSize = true;
            this.lbl_newGlobalBudgetLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_newGlobalBudgetLeft.Location = new System.Drawing.Point(371, 515);
            this.lbl_newGlobalBudgetLeft.Name = "lbl_newGlobalBudgetLeft";
            this.lbl_newGlobalBudgetLeft.Size = new System.Drawing.Size(13, 13);
            this.lbl_newGlobalBudgetLeft.TabIndex = 83;
            this.lbl_newGlobalBudgetLeft.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(54, 516);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(314, 13);
            this.label13.TabIndex = 84;
            this.label13.Text = "NEW GLOBAL BUDGET LEFT FOR ALLOCATION     $";
            // 
            // lbl_newSubBudgetLeft
            // 
            this.lbl_newSubBudgetLeft.AutoSize = true;
            this.lbl_newSubBudgetLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_newSubBudgetLeft.Location = new System.Drawing.Point(371, 484);
            this.lbl_newSubBudgetLeft.Name = "lbl_newSubBudgetLeft";
            this.lbl_newSubBudgetLeft.Size = new System.Drawing.Size(13, 13);
            this.lbl_newSubBudgetLeft.TabIndex = 81;
            this.lbl_newSubBudgetLeft.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(54, 484);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(315, 13);
            this.label14.TabIndex = 82;
            this.label14.Text = "NEW SUBJECT BUDGET LEFT                               $";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(204, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "STAGE";
            // 
            // txt_stage
            // 
            this.txt_stage.Enabled = false;
            this.txt_stage.HideSelection = false;
            this.txt_stage.Location = new System.Drawing.Point(108, 127);
            this.txt_stage.Name = "txt_stage";
            this.txt_stage.Size = new System.Drawing.Size(254, 20);
            this.txt_stage.TabIndex = 51;
            this.txt_stage.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(57, 361);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(312, 13);
            this.label9.TabIndex = 82;
            this.label9.Text = "CURRENT SUBJECT BUDGET ALLOCATED            $";
            // 
            // lbl_currentSubBudget
            // 
            this.lbl_currentSubBudget.AutoSize = true;
            this.lbl_currentSubBudget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_currentSubBudget.Location = new System.Drawing.Point(370, 361);
            this.lbl_currentSubBudget.Name = "lbl_currentSubBudget";
            this.lbl_currentSubBudget.Size = new System.Drawing.Size(13, 13);
            this.lbl_currentSubBudget.TabIndex = 81;
            this.lbl_currentSubBudget.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(57, 394);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(313, 13);
            this.label15.TabIndex = 82;
            this.label15.Text = "CURRENT SUBJECT BUDGET SPENT                    $";
            // 
            // lbl_currentSubBudgetSpent
            // 
            this.lbl_currentSubBudgetSpent.AutoSize = true;
            this.lbl_currentSubBudgetSpent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_currentSubBudgetSpent.Location = new System.Drawing.Point(370, 394);
            this.lbl_currentSubBudgetSpent.Name = "lbl_currentSubBudgetSpent";
            this.lbl_currentSubBudgetSpent.Size = new System.Drawing.Size(13, 13);
            this.lbl_currentSubBudgetSpent.TabIndex = 81;
            this.lbl_currentSubBudgetSpent.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(58, 426);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(310, 13);
            this.label17.TabIndex = 82;
            this.label17.Text = "CURRENT SUBJECT BUDGET LEFT                      $";
            // 
            // lbl_currentSubBudgetLeft
            // 
            this.lbl_currentSubBudgetLeft.AutoSize = true;
            this.lbl_currentSubBudgetLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_currentSubBudgetLeft.Location = new System.Drawing.Point(370, 427);
            this.lbl_currentSubBudgetLeft.Name = "lbl_currentSubBudgetLeft";
            this.lbl_currentSubBudgetLeft.Size = new System.Drawing.Size(13, 13);
            this.lbl_currentSubBudgetLeft.TabIndex = 81;
            this.lbl_currentSubBudgetLeft.Text = "0";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.DimGray;
            this.pictureBox5.Location = new System.Drawing.Point(0, 248);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(462, 1);
            this.pictureBox5.TabIndex = 68;
            this.pictureBox5.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(60, 282);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(309, 13);
            this.label3.TabIndex = 82;
            this.label3.Text = "CURRENT GLOBAL BUDGET ALLOCATED             $";
            // 
            // lbl_currentGlobal
            // 
            this.lbl_currentGlobal.AutoSize = true;
            this.lbl_currentGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_currentGlobal.Location = new System.Drawing.Point(369, 283);
            this.lbl_currentGlobal.Name = "lbl_currentGlobal";
            this.lbl_currentGlobal.Size = new System.Drawing.Size(13, 13);
            this.lbl_currentGlobal.TabIndex = 81;
            this.lbl_currentGlobal.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(57, 315);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(309, 13);
            this.label8.TabIndex = 82;
            this.label8.Text = "GLOBAL BUDGET LEFT FOR ALLOCATION            $";
            // 
            // lbl_currentGlobalBudgetLeft
            // 
            this.lbl_currentGlobalBudgetLeft.AutoSize = true;
            this.lbl_currentGlobalBudgetLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_currentGlobalBudgetLeft.Location = new System.Drawing.Point(366, 316);
            this.lbl_currentGlobalBudgetLeft.Name = "lbl_currentGlobalBudgetLeft";
            this.lbl_currentGlobalBudgetLeft.Size = new System.Drawing.Size(13, 13);
            this.lbl_currentGlobalBudgetLeft.TabIndex = 81;
            this.lbl_currentGlobalBudgetLeft.Text = "0";
            // 
            // frm_changeSubBudget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 637);
            this.Controls.Add(this.lbl_newGlobalBudgetLeft);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lbl_currentSubBudgetLeft);
            this.Controls.Add(this.lbl_currentSubBudgetSpent);
            this.Controls.Add(this.lbl_currentGlobalBudgetLeft);
            this.Controls.Add(this.lbl_currentGlobal);
            this.Controls.Add(this.lbl_currentSubBudget);
            this.Controls.Add(this.lbl_newSubBudgetLeft);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.btn_changesubBudget);
            this.Controls.Add(this.pnl_topBorder);
            this.Controls.Add(this.txt_newSubBudget);
            this.Controls.Add(this.txt_stage);
            this.Controls.Add(this.txt_subject);
            this.Controls.Add(this.lbl_newSubBudget);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_subject);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_changeSubBudget";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangeGlobalBudget";
            this.pnl_topBorder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Panel pnl_topBorder;
        private System.Windows.Forms.Button btn_changesubBudget;
        private System.Windows.Forms.TextBox txt_subject;
        private System.Windows.Forms.Label lbl_subject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_newSubBudget;
        private System.Windows.Forms.Label lbl_newSubBudget;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label lbl_newGlobalBudgetLeft;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbl_newSubBudgetLeft;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_stage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_currentSubBudget;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbl_currentSubBudgetSpent;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbl_currentSubBudgetLeft;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_currentGlobal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_currentGlobalBudgetLeft;
    }
}