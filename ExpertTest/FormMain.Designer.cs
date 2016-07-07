namespace ExpertTest
{
    partial class FormMain
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonList = new System.Windows.Forms.Button();
            this.buttonBegin = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonBooks = new System.Windows.Forms.Button();
            this.buttonNoAnswer = new System.Windows.Forms.Button();
            this.buttonAnswer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelQuestion = new System.Windows.Forms.Label();
            this.AnswelistBox = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonList);
            this.tabPage1.Controls.Add(this.buttonBegin);
            this.tabPage1.Controls.Add(this.buttonPrevious);
            this.tabPage1.Controls.Add(this.buttonBack);
            this.tabPage1.Controls.Add(this.buttonBooks);
            this.tabPage1.Controls.Add(this.buttonNoAnswer);
            this.tabPage1.Controls.Add(this.buttonAnswer);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.labelQuestion);
            this.tabPage1.Controls.Add(this.AnswelistBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(604, 295);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Тест";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonList
            // 
            this.buttonList.Enabled = false;
            this.buttonList.Location = new System.Drawing.Point(245, 256);
            this.buttonList.Name = "buttonList";
            this.buttonList.Size = new System.Drawing.Size(81, 23);
            this.buttonList.TabIndex = 9;
            this.buttonList.Text = "Список";
            this.buttonList.UseVisualStyleBackColor = true;
            this.buttonList.Visible = false;
            this.buttonList.Click += new System.EventHandler(this.buttonList_Click);
            // 
            // buttonBegin
            // 
            this.buttonBegin.Location = new System.Drawing.Point(494, 6);
            this.buttonBegin.Name = "buttonBegin";
            this.buttonBegin.Size = new System.Drawing.Size(104, 23);
            this.buttonBegin.TabIndex = 8;
            this.buttonBegin.Text = "Розпочати";
            this.buttonBegin.UseVisualStyleBackColor = true;
            this.buttonBegin.Click += new System.EventHandler(this.buttonBegin_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Enabled = false;
            this.buttonPrevious.Location = new System.Drawing.Point(76, 256);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(163, 23);
            this.buttonPrevious.TabIndex = 7;
            this.buttonPrevious.Text = "Повернутися до питання";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Visible = false;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(3, 256);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(67, 23);
            this.buttonBack.TabIndex = 6;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Visible = false;
            // 
            // buttonBooks
            // 
            this.buttonBooks.Enabled = false;
            this.buttonBooks.Location = new System.Drawing.Point(332, 256);
            this.buttonBooks.Name = "buttonBooks";
            this.buttonBooks.Size = new System.Drawing.Size(104, 23);
            this.buttonBooks.TabIndex = 5;
            this.buttonBooks.Text = "Запропоновані";
            this.buttonBooks.UseVisualStyleBackColor = true;
            this.buttonBooks.Click += new System.EventHandler(this.buttonBooks_Click);
            // 
            // buttonNoAnswer
            // 
            this.buttonNoAnswer.Location = new System.Drawing.Point(442, 256);
            this.buttonNoAnswer.Name = "buttonNoAnswer";
            this.buttonNoAnswer.Size = new System.Drawing.Size(75, 23);
            this.buttonNoAnswer.TabIndex = 4;
            this.buttonNoAnswer.Text = "Пропустити";
            this.buttonNoAnswer.UseVisualStyleBackColor = true;
            this.buttonNoAnswer.Click += new System.EventHandler(this.buttonNoAnswer_Click);
            // 
            // buttonAnswer
            // 
            this.buttonAnswer.Location = new System.Drawing.Point(523, 256);
            this.buttonAnswer.Name = "buttonAnswer";
            this.buttonAnswer.Size = new System.Drawing.Size(75, 23);
            this.buttonAnswer.TabIndex = 3;
            this.buttonAnswer.Text = "Відповісти";
            this.buttonAnswer.UseVisualStyleBackColor = true;
            this.buttonAnswer.Click += new System.EventHandler(this.buttonAnswer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Запитання:";
            // 
            // labelQuestion
            // 
            this.labelQuestion.AutoSize = true;
            this.labelQuestion.Location = new System.Drawing.Point(6, 34);
            this.labelQuestion.Name = "labelQuestion";
            this.labelQuestion.Size = new System.Drawing.Size(0, 13);
            this.labelQuestion.TabIndex = 1;
            // 
            // AnswelistBox
            // 
            this.AnswelistBox.FormattingEnabled = true;
            this.AnswelistBox.Location = new System.Drawing.Point(4, 90);
            this.AnswelistBox.Name = "AnswelistBox";
            this.AnswelistBox.Size = new System.Drawing.Size(594, 160);
            this.AnswelistBox.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(2, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(612, 321);
            this.tabControl1.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 336);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormMain";
            this.Text = "СППР";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonList;
        private System.Windows.Forms.Button buttonBegin;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonBooks;
        private System.Windows.Forms.Button buttonNoAnswer;
        private System.Windows.Forms.Button buttonAnswer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelQuestion;
        private System.Windows.Forms.ListBox AnswelistBox;
        private System.Windows.Forms.TabControl tabControl1;
    }
}