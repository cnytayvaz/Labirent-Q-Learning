namespace Labirent_Q_Learning
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
            this.uploadButton = new System.Windows.Forms.Button();
            this.startComboBox = new System.Windows.Forms.ComboBox();
            this.targetComboBox = new System.Windows.Forms.ComboBox();
            this.iterationTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.rMatrixButton = new System.Windows.Forms.Button();
            this.qMatrixButton = new System.Windows.Forms.Button();
            this.drawMazeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uploadButton
            // 
            this.uploadButton.Location = new System.Drawing.Point(12, 12);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(106, 23);
            this.uploadButton.TabIndex = 0;
            this.uploadButton.Text = "Dosya Yükle";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // startComboBox
            // 
            this.startComboBox.FormattingEnabled = true;
            this.startComboBox.Location = new System.Drawing.Point(12, 42);
            this.startComboBox.Name = "startComboBox";
            this.startComboBox.Size = new System.Drawing.Size(106, 21);
            this.startComboBox.TabIndex = 1;
            this.startComboBox.Text = "Başlangıç";
            // 
            // targetComboBox
            // 
            this.targetComboBox.FormattingEnabled = true;
            this.targetComboBox.Location = new System.Drawing.Point(12, 70);
            this.targetComboBox.Name = "targetComboBox";
            this.targetComboBox.Size = new System.Drawing.Size(106, 21);
            this.targetComboBox.TabIndex = 2;
            this.targetComboBox.Text = "Hedef";
            // 
            // iterationTextBox
            // 
            this.iterationTextBox.Location = new System.Drawing.Point(67, 97);
            this.iterationTextBox.Name = "iterationTextBox";
            this.iterationTextBox.Size = new System.Drawing.Size(51, 20);
            this.iterationTextBox.TabIndex = 3;
            this.iterationTextBox.Text = "3000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "İterasyon";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 123);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(106, 23);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "İşleme Başla";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Visible = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 238);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(105, 23);
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(124, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // rMatrixButton
            // 
            this.rMatrixButton.Location = new System.Drawing.Point(12, 152);
            this.rMatrixButton.Name = "rMatrixButton";
            this.rMatrixButton.Size = new System.Drawing.Size(105, 23);
            this.rMatrixButton.TabIndex = 9;
            this.rMatrixButton.Text = "R Matrisi";
            this.rMatrixButton.UseVisualStyleBackColor = true;
            this.rMatrixButton.Visible = false;
            this.rMatrixButton.Click += new System.EventHandler(this.rMatrixButton_Click);
            // 
            // qMatrixButton
            // 
            this.qMatrixButton.Location = new System.Drawing.Point(12, 180);
            this.qMatrixButton.Name = "qMatrixButton";
            this.qMatrixButton.Size = new System.Drawing.Size(105, 23);
            this.qMatrixButton.TabIndex = 10;
            this.qMatrixButton.Text = "Q Matrisi";
            this.qMatrixButton.UseVisualStyleBackColor = true;
            this.qMatrixButton.Visible = false;
            this.qMatrixButton.Click += new System.EventHandler(this.qMatrixButton_Click);
            // 
            // drawMazeButton
            // 
            this.drawMazeButton.Location = new System.Drawing.Point(12, 209);
            this.drawMazeButton.Name = "drawMazeButton";
            this.drawMazeButton.Size = new System.Drawing.Size(105, 23);
            this.drawMazeButton.TabIndex = 11;
            this.drawMazeButton.Text = "Labirenti Çiz";
            this.drawMazeButton.UseVisualStyleBackColor = true;
            this.drawMazeButton.Visible = false;
            this.drawMazeButton.Click += new System.EventHandler(this.drawMazeButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 411);
            this.Controls.Add(this.drawMazeButton);
            this.Controls.Add(this.qMatrixButton);
            this.Controls.Add(this.rMatrixButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.iterationTextBox);
            this.Controls.Add(this.targetComboBox);
            this.Controls.Add(this.startComboBox);
            this.Controls.Add(this.uploadButton);
            this.Name = "Form1";
            this.Text = "Labirent-Q-Learning";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.ComboBox startComboBox;
        private System.Windows.Forms.ComboBox targetComboBox;
        private System.Windows.Forms.TextBox iterationTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button rMatrixButton;
        private System.Windows.Forms.Button qMatrixButton;
        private System.Windows.Forms.Button drawMazeButton;
    }
}

