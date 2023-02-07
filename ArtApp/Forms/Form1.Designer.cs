
namespace ArtApp {
	partial class Form1 {
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent() {
			this.buttonPrev = new System.Windows.Forms.Button();
			this.textBoxPath = new System.Windows.Forms.TextBox();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.comboBoxSource = new System.Windows.Forms.ComboBox();
			this.buttonNext = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonPrev
			// 
			this.buttonPrev.Location = new System.Drawing.Point(13, 13);
			this.buttonPrev.Name = "buttonPrev";
			this.buttonPrev.Size = new System.Drawing.Size(75, 23);
			this.buttonPrev.TabIndex = 0;
			this.buttonPrev.Text = "<";
			this.buttonPrev.UseVisualStyleBackColor = true;
			this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
			// 
			// textBoxPath
			// 
			this.textBoxPath.Location = new System.Drawing.Point(440, 12);
			this.textBoxPath.Name = "textBoxPath";
			this.textBoxPath.ReadOnly = true;
			this.textBoxPath.Size = new System.Drawing.Size(632, 22);
			this.textBoxPath.TabIndex = 2;
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(10, 40);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(200, 200);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox.TabIndex = 3;
			this.pictureBox.TabStop = false;
			// 
			// comboBoxSource
			// 
			this.comboBoxSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSource.FormattingEnabled = true;
			this.comboBoxSource.Location = new System.Drawing.Point(186, 10);
			this.comboBoxSource.Name = "comboBoxSource";
			this.comboBoxSource.Size = new System.Drawing.Size(239, 24);
			this.comboBoxSource.TabIndex = 4;
			this.comboBoxSource.SelectedIndexChanged += new System.EventHandler(this.comboBoxSource_SelectedIndexChanged);
			// 
			// buttonNext
			// 
			this.buttonNext.Location = new System.Drawing.Point(95, 12);
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.Size = new System.Drawing.Size(75, 23);
			this.buttonNext.TabIndex = 5;
			this.buttonNext.Text = ">";
			this.buttonNext.UseVisualStyleBackColor = true;
			this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1087, 717);
			this.Controls.Add(this.buttonNext);
			this.Controls.Add(this.comboBoxSource);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.textBoxPath);
			this.Controls.Add(this.buttonPrev);
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "Form1";
			this.Text = "ArtApp";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonPrev;
		private System.Windows.Forms.TextBox textBoxPath;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.ComboBox comboBoxSource;
		private System.Windows.Forms.Button buttonNext;
	}
}

