
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
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.textBoxPath = new System.Windows.Forms.TextBox();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.comboBoxSource = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Location = new System.Drawing.Point(13, 13);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
			this.buttonUpdate.TabIndex = 0;
			this.buttonUpdate.Text = "Update";
			this.buttonUpdate.UseVisualStyleBackColor = true;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// textBoxPath
			// 
			this.textBoxPath.Location = new System.Drawing.Point(340, 12);
			this.textBoxPath.Name = "textBoxPath";
			this.textBoxPath.ReadOnly = true;
			this.textBoxPath.Size = new System.Drawing.Size(641, 22);
			this.textBoxPath.TabIndex = 2;
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(10, 40);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(1062, 662);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox.TabIndex = 3;
			this.pictureBox.TabStop = false;
			// 
			// comboBoxSource
			// 
			this.comboBoxSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSource.FormattingEnabled = true;
			this.comboBoxSource.Location = new System.Drawing.Point(95, 10);
			this.comboBoxSource.Name = "comboBoxSource";
			this.comboBoxSource.Size = new System.Drawing.Size(239, 24);
			this.comboBoxSource.TabIndex = 4;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1087, 717);
			this.Controls.Add(this.comboBoxSource);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.textBoxPath);
			this.Controls.Add(this.buttonUpdate);
			this.Name = "Form1";
			this.Text = "ArtApp";
			this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonUpdate;
		private System.Windows.Forms.TextBox textBoxPath;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.ComboBox comboBoxSource;
	}
}

