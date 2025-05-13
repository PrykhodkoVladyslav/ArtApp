namespace ArtAppWinForms;

partial class Form1 {
	/// <summary>
	///  Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing) {
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	///  Required method for Designer support - do not modify
	///  the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
		ButtonPrev = new Button();
		ButtonNext = new Button();
		ComboBoxSource = new ComboBox();
		TextBoxPath = new TextBox();
		PictureBox = new PictureBox();
		((System.ComponentModel.ISupportInitialize)PictureBox).BeginInit();
		SuspendLayout();
		// 
		// ButtonPrev
		// 
		ButtonPrev.Location = new Point(12, 12);
		ButtonPrev.Name = "ButtonPrev";
		ButtonPrev.Size = new Size(69, 29);
		ButtonPrev.TabIndex = 0;
		ButtonPrev.Text = "<";
		ButtonPrev.UseVisualStyleBackColor = true;
		ButtonPrev.Click += ButtonPrev_Click;
		// 
		// ButtonNext
		// 
		ButtonNext.Location = new Point(87, 12);
		ButtonNext.Name = "ButtonNext";
		ButtonNext.Size = new Size(69, 29);
		ButtonNext.TabIndex = 1;
		ButtonNext.Text = ">";
		ButtonNext.UseVisualStyleBackColor = true;
		ButtonNext.Click += ButtonNext_Click;
		// 
		// ComboBoxSource
		// 
		ComboBoxSource.DropDownStyle = ComboBoxStyle.DropDownList;
		ComboBoxSource.FormattingEnabled = true;
		ComboBoxSource.Location = new Point(162, 13);
		ComboBoxSource.Name = "ComboBoxSource";
		ComboBoxSource.Size = new Size(221, 28);
		ComboBoxSource.TabIndex = 2;
		ComboBoxSource.SelectedIndexChanged += BomboBoxSource_SelectedIndexChanged;
		// 
		// TextBoxPath
		// 
		TextBoxPath.Location = new Point(389, 14);
		TextBoxPath.Name = "TextBoxPath";
		TextBoxPath.ReadOnly = true;
		TextBoxPath.Size = new Size(399, 27);
		TextBoxPath.TabIndex = 3;
		// 
		// PictureBox
		// 
		PictureBox.Location = new Point(12, 47);
		PictureBox.Name = "PictureBox";
		PictureBox.Size = new Size(200, 200);
		PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
		PictureBox.TabIndex = 4;
		PictureBox.TabStop = false;
		// 
		// Form1
		// 
		AutoScaleDimensions = new SizeF(8F, 20F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Controls.Add(PictureBox);
		Controls.Add(TextBoxPath);
		Controls.Add(ComboBoxSource);
		Controls.Add(ButtonNext);
		Controls.Add(ButtonPrev);
		Name = "Form1";
		Text = "ArtApp";
		FormClosing += Form1_FormClosing;
		SizeChanged += Form1_SizeChanged;
		((System.ComponentModel.ISupportInitialize)PictureBox).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Button ButtonPrev;
	private Button ButtonNext;
	private ComboBox ComboBoxSource;
	private TextBox TextBoxPath;
	private PictureBox PictureBox;
}
