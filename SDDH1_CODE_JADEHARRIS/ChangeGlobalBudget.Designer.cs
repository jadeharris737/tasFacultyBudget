namespace SDDH1_CODE_JADEHARRIS
{
    partial class frm_changeGlobalBudget
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_changeGlobalBudget));
            this.label2 = new System.Windows.Forms.Label();
            this.btn_changeGlobalBudget = new System.Windows.Forms.Button();
            this.txt_newBudget = new System.Windows.Forms.TextBox();
            this.lbl_newBudget = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.pnl_topBorder = new System.Windows.Forms.Panel();
            this.lbl_newGlobalLeft = new System.Windows.Forms.Label();
            this.lbl_currentGlobalLeft = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_alreadyAllocated = new System.Windows.Forms.Label();
            this.pnl_topBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(78, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 15);
            this.label2.TabIndex = 29;
            this.label2.Text = "$";
            // 
            // btn_changeGlobalBudget
            // 
            this.btn_changeGlobalBudget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_changeGlobalBudget.FlatAppearance.BorderSize = 0;
            this.btn_changeGlobalBudget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_changeGlobalBudget.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_changeGlobalBudget.Location = new System.Drawing.Point(152, 230);
            this.btn_changeGlobalBudget.Name = "btn_changeGlobalBudget";
            this.btn_changeGlobalBudget.Size = new System.Drawing.Size(132, 31);
            this.btn_changeGlobalBudget.TabIndex = 28;
            this.btn_changeGlobalBudget.Text = "SET";
            this.btn_changeGlobalBudget.UseVisualStyleBackColor = false;
            this.btn_changeGlobalBudget.Click += new System.EventHandler(this.btn_changeGlobalBudget_Click);
            // 
            // txt_newBudget
            // 
            this.txt_newBudget.Location = new System.Drawing.Point(94, 66);
            this.txt_newBudget.Name = "txt_newBudget";
            this.txt_newBudget.Size = new System.Drawing.Size(271, 20);
            this.txt_newBudget.TabIndex = 27;
            this.txt_newBudget.TextChanged += new System.EventHandler(this.txt_newBudget_TextChanged);
            this.txt_newBudget.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_newBudget_KeyPress);
            // 
            // lbl_newBudget
            // 
            this.lbl_newBudget.AutoSize = true;
            this.lbl_newBudget.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_newBudget.Location = new System.Drawing.Point(170, 49);
            this.lbl_newBudget.Name = "lbl_newBudget";
            this.lbl_newBudget.Size = new System.Drawing.Size(123, 14);
            this.lbl_newBudget.TabIndex = 26;
            this.lbl_newBudget.Text = "NEW GLOBAL BUDGET";
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_close.FlatAppearance.BorderSize = 0;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_close.Location = new System.Drawing.Point(397, 0);
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
            this.pnl_topBorder.Size = new System.Drawing.Size(455, 22);
            this.pnl_topBorder.TabIndex = 36;
            // 
            // lbl_newGlobalLeft
            // 
            this.lbl_newGlobalLeft.AutoSize = true;
            this.lbl_newGlobalLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_newGlobalLeft.Location = new System.Drawing.Point(375, 190);
            this.lbl_newGlobalLeft.Name = "lbl_newGlobalLeft";
            this.lbl_newGlobalLeft.Size = new System.Drawing.Size(13, 13);
            this.lbl_newGlobalLeft.TabIndex = 84;
            this.lbl_newGlobalLeft.Text = "0";
            // 
            // lbl_currentGlobalLeft
            // 
            this.lbl_currentGlobalLeft.AutoSize = true;
            this.lbl_currentGlobalLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_currentGlobalLeft.Location = new System.Drawing.Point(376, 153);
            this.lbl_currentGlobalLeft.Name = "lbl_currentGlobalLeft";
            this.lbl_currentGlobalLeft.Size = new System.Drawing.Size(13, 13);
            this.lbl_currentGlobalLeft.TabIndex = 85;
            this.lbl_currentGlobalLeft.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(22, 190);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(354, 13);
            this.label15.TabIndex = 86;
            this.label15.Text = "NEW GLOBAL BUDGET LEFT (FOR ALLOCATION)             $";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(23, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(353, 13);
            this.label9.TabIndex = 87;
            this.label9.Text = "CURRENT GLOBAL BUDGET LEFT (FOR ALLOCATION)     $";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.DimGray;
            this.pictureBox5.Location = new System.Drawing.Point(-1, 102);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(462, 1);
            this.pictureBox5.TabIndex = 83;
            this.pictureBox5.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 13);
            this.label1.TabIndex = 87;
            this.label1.Text = "AMOUNT ALREADY ALLOCATED TO SUBEJCTS                $";
            // 
            // lbl_alreadyAllocated
            // 
            this.lbl_alreadyAllocated.AutoSize = true;
            this.lbl_alreadyAllocated.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_alreadyAllocated.Location = new System.Drawing.Point(375, 126);
            this.lbl_alreadyAllocated.Name = "lbl_alreadyAllocated";
            this.lbl_alreadyAllocated.Size = new System.Drawing.Size(13, 13);
            this.lbl_alreadyAllocated.TabIndex = 85;
            this.lbl_alreadyAllocated.Text = "0";
            // 
            // frm_changeGlobalBudget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 280);
            this.Controls.Add(this.lbl_newGlobalLeft);
            this.Controls.Add(this.lbl_alreadyAllocated);
            this.Controls.Add(this.lbl_currentGlobalLeft);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pnl_topBorder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_changeGlobalBudget);
            this.Controls.Add(this.txt_newBudget);
            this.Controls.Add(this.lbl_newBudget);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_changeGlobalBudget";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangeGlobalBudget";
            this.Shown += new System.EventHandler(this.frm_changeGlobalBudget_Shown);
            this.pnl_topBorder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_changeGlobalBudget;
        private System.Windows.Forms.TextBox txt_newBudget;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Panel pnl_topBorder;
        private System.Windows.Forms.Label lbl_newBudget;
        private System.Windows.Forms.Label lbl_newGlobalLeft;
        private System.Windows.Forms.Label lbl_currentGlobalLeft;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_alreadyAllocated;
    }
}