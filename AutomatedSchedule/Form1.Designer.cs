namespace AutomatedSchedule
{
    partial class Form1
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
            this.getDataBtn = new System.Windows.Forms.Button();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.tenDayInc = new System.Windows.Forms.RadioButton();
            this.twentyDayInc = new System.Windows.Forms.RadioButton();
            this.thirtyDayInc = new System.Windows.Forms.RadioButton();
            this.fNameBox = new System.Windows.Forms.TextBox();
            this.lNameBox = new System.Windows.Forms.TextBox();
            this.editUserDataBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // getDataBtn
            // 
            this.getDataBtn.Location = new System.Drawing.Point(472, 322);
            this.getDataBtn.Name = "getDataBtn";
            this.getDataBtn.Size = new System.Drawing.Size(75, 23);
            this.getDataBtn.TabIndex = 0;
            this.getDataBtn.Text = "button1";
            this.getDataBtn.UseVisualStyleBackColor = true;
            this.getDataBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(71, 127);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(100, 26);
            this.usernameBox.TabIndex = 1;
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(71, 181);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(100, 26);
            this.passwordBox.TabIndex = 2;
            // 
            // tenDayInc
            // 
            this.tenDayInc.AutoSize = true;
            this.tenDayInc.Location = new System.Drawing.Point(442, 68);
            this.tenDayInc.Name = "tenDayInc";
            this.tenDayInc.Size = new System.Drawing.Size(126, 24);
            this.tenDayInc.TabIndex = 3;
            this.tenDayInc.TabStop = true;
            this.tenDayInc.Text = "radioButton1";
            this.tenDayInc.UseVisualStyleBackColor = true;
            // 
            // twentyDayInc
            // 
            this.twentyDayInc.AutoSize = true;
            this.twentyDayInc.Location = new System.Drawing.Point(442, 127);
            this.twentyDayInc.Name = "twentyDayInc";
            this.twentyDayInc.Size = new System.Drawing.Size(126, 24);
            this.twentyDayInc.TabIndex = 4;
            this.twentyDayInc.TabStop = true;
            this.twentyDayInc.Text = "radioButton1";
            this.twentyDayInc.UseVisualStyleBackColor = true;
            // 
            // thirtyDayInc
            // 
            this.thirtyDayInc.AutoSize = true;
            this.thirtyDayInc.Location = new System.Drawing.Point(442, 181);
            this.thirtyDayInc.Name = "thirtyDayInc";
            this.thirtyDayInc.Size = new System.Drawing.Size(126, 24);
            this.thirtyDayInc.TabIndex = 5;
            this.thirtyDayInc.TabStop = true;
            this.thirtyDayInc.Text = "radioButton1";
            this.thirtyDayInc.UseVisualStyleBackColor = true;
            // 
            // fNameBox
            // 
            this.fNameBox.Location = new System.Drawing.Point(71, 12);
            this.fNameBox.Name = "fNameBox";
            this.fNameBox.Size = new System.Drawing.Size(100, 26);
            this.fNameBox.TabIndex = 6;
            // 
            // lNameBox
            // 
            this.lNameBox.Location = new System.Drawing.Point(71, 66);
            this.lNameBox.Name = "lNameBox";
            this.lNameBox.Size = new System.Drawing.Size(100, 26);
            this.lNameBox.TabIndex = 7;
            // 
            // editUserDataBtn
            // 
            this.editUserDataBtn.Location = new System.Drawing.Point(81, 347);
            this.editUserDataBtn.Name = "editUserDataBtn";
            this.editUserDataBtn.Size = new System.Drawing.Size(75, 23);
            this.editUserDataBtn.TabIndex = 8;
            this.editUserDataBtn.Text = "button2";
            this.editUserDataBtn.UseVisualStyleBackColor = true;
            this.editUserDataBtn.Click += new System.EventHandler(this.editUserDataBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.editUserDataBtn);
            this.Controls.Add(this.lNameBox);
            this.Controls.Add(this.fNameBox);
            this.Controls.Add(this.thirtyDayInc);
            this.Controls.Add(this.twentyDayInc);
            this.Controls.Add(this.tenDayInc);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.getDataBtn);
            this.Name = "Form1";
            this.Text = "Form1";
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
    }
}

