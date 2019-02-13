namespace AutomatedSchedule
{
    partial class AutomatedScheduler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutomatedScheduler));
            this.getDataBtn = new System.Windows.Forms.Button();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.tenDayInc = new System.Windows.Forms.RadioButton();
            this.twentyDayInc = new System.Windows.Forms.RadioButton();
            this.thirtyDayInc = new System.Windows.Forms.RadioButton();
            this.fNameBox = new System.Windows.Forms.TextBox();
            this.lNameBox = new System.Windows.Forms.TextBox();
            this.editUserDataBtn = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.About = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.fiveDayInc = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // getDataBtn
            // 
            this.getDataBtn.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.getDataBtn.Location = new System.Drawing.Point(253, 290);
            this.getDataBtn.Name = "getDataBtn";
            this.getDataBtn.Size = new System.Drawing.Size(100, 42);
            this.getDataBtn.TabIndex = 0;
            this.getDataBtn.Text = "Search";
            this.getDataBtn.UseVisualStyleBackColor = false;
            this.getDataBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(119, 181);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(100, 26);
            this.usernameBox.TabIndex = 1;
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(119, 235);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(100, 26);
            this.passwordBox.TabIndex = 2;
            this.passwordBox.UseSystemPasswordChar = true;
            this.passwordBox.TextChanged += new System.EventHandler(this.passwordBox_TextChanged);
            // 
            // tenDayInc
            // 
            this.tenDayInc.AutoSize = true;
            this.tenDayInc.Location = new System.Drawing.Point(253, 123);
            this.tenDayInc.Name = "tenDayInc";
            this.tenDayInc.Size = new System.Drawing.Size(92, 24);
            this.tenDayInc.TabIndex = 3;
            this.tenDayInc.TabStop = true;
            this.tenDayInc.Text = "10 Days";
            this.tenDayInc.UseVisualStyleBackColor = true;
            // 
            // twentyDayInc
            // 
            this.twentyDayInc.AutoSize = true;
            this.twentyDayInc.Location = new System.Drawing.Point(253, 182);
            this.twentyDayInc.Name = "twentyDayInc";
            this.twentyDayInc.Size = new System.Drawing.Size(92, 24);
            this.twentyDayInc.TabIndex = 4;
            this.twentyDayInc.TabStop = true;
            this.twentyDayInc.Text = "20 Days";
            this.twentyDayInc.UseVisualStyleBackColor = true;
            // 
            // thirtyDayInc
            // 
            this.thirtyDayInc.AutoSize = true;
            this.thirtyDayInc.Location = new System.Drawing.Point(253, 236);
            this.thirtyDayInc.Name = "thirtyDayInc";
            this.thirtyDayInc.Size = new System.Drawing.Size(92, 24);
            this.thirtyDayInc.TabIndex = 5;
            this.thirtyDayInc.TabStop = true;
            this.thirtyDayInc.Text = "30 Days";
            this.thirtyDayInc.UseVisualStyleBackColor = true;
            // 
            // fNameBox
            // 
            this.fNameBox.Location = new System.Drawing.Point(119, 66);
            this.fNameBox.Name = "fNameBox";
            this.fNameBox.Size = new System.Drawing.Size(100, 26);
            this.fNameBox.TabIndex = 6;
            this.fNameBox.TextChanged += new System.EventHandler(this.fNameBox_TextChanged);
            // 
            // lNameBox
            // 
            this.lNameBox.Location = new System.Drawing.Point(119, 120);
            this.lNameBox.Name = "lNameBox";
            this.lNameBox.Size = new System.Drawing.Size(100, 26);
            this.lNameBox.TabIndex = 7;
            // 
            // editUserDataBtn
            // 
            this.editUserDataBtn.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.editUserDataBtn.Location = new System.Drawing.Point(119, 290);
            this.editUserDataBtn.Name = "editUserDataBtn";
            this.editUserDataBtn.Size = new System.Drawing.Size(100, 42);
            this.editUserDataBtn.TabIndex = 8;
            this.editUserDataBtn.Text = "Save";
            this.editUserDataBtn.UseVisualStyleBackColor = false;
            this.editUserDataBtn.Click += new System.EventHandler(this.editUserDataBtn_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(12, 21);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(367, 29);
            this.Title.TabIndex = 9;
            this.Title.Text = "Automated Schedule Searcher";
            // 
            // About
            // 
            this.About.Location = new System.Drawing.Point(197, 359);
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(75, 45);
            this.About.TabIndex = 10;
            this.About.Text = "About";
            this.About.UseVisualStyleBackColor = true;
            this.About.Click += new System.EventHandler(this.About_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "First Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Last Name";
            // 
            // fiveDayInc
            // 
            this.fiveDayInc.AutoSize = true;
            this.fiveDayInc.Location = new System.Drawing.Point(253, 70);
            this.fiveDayInc.Name = "fiveDayInc";
            this.fiveDayInc.Size = new System.Drawing.Size(83, 24);
            this.fiveDayInc.TabIndex = 15;
            this.fiveDayInc.TabStop = true;
            this.fiveDayInc.Text = "5 Days";
            this.fiveDayInc.UseVisualStyleBackColor = true;
            this.fiveDayInc.CheckedChanged += new System.EventHandler(this.fiveDayInc_CheckedChanged);
            // 
            // AutomatedScheduler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(396, 429);
            this.Controls.Add(this.fiveDayInc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.About);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.editUserDataBtn);
            this.Controls.Add(this.lNameBox);
            this.Controls.Add(this.fNameBox);
            this.Controls.Add(this.thirtyDayInc);
            this.Controls.Add(this.twentyDayInc);
            this.Controls.Add(this.tenDayInc);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.getDataBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AutomatedScheduler";
            this.Text = "Automated Scheduler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getDataBtn;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.RadioButton tenDayInc;
        private System.Windows.Forms.RadioButton twentyDayInc;
        private System.Windows.Forms.RadioButton thirtyDayInc;
        private System.Windows.Forms.TextBox fNameBox;
        private System.Windows.Forms.TextBox lNameBox;
        private System.Windows.Forms.Button editUserDataBtn;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button About;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton fiveDayInc;
    }
}

