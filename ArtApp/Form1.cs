using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtApp {
	public partial class Form1 : Form {
		PictureController picture;
		ApiCollection apiCollection;

		public Form1() {
			InitializeComponent();

			picture = new PictureController();
			picture.ChangeUrl += ChangeUrl;
			picture.ChangePicturePath += ChangePicturePath;

			apiCollection = new ApiCollection();

			SetAPIs();
			FillComboBoxSource();

			Form1_SizeChanged(this, new EventArgs());

			try {
				picture.Load("save.dat");
			}
			catch (System.IO.FileNotFoundException) { }

			comboBoxSource.SelectedIndex = 0;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
			picture.Save("save.dat");
		}

		private void Form1_SizeChanged(object sender, EventArgs e) {
			pictureBox.Size = new Size(Width - pictureBox.Location.X * 2, Height - 15 - (pictureBox.Location.Y) * 2);
		}

		private void buttonPrev_Click(object sender, EventArgs e) {
			picture.LoadPrev();
		}

		private void buttonNext_Click(object sender, EventArgs e) {
			picture.LoadNext();
		}

		private void comboBoxSource_SelectedIndexChanged(object sender, EventArgs e) {
			SubApi subApi = apiCollection[comboBoxSource.SelectedIndex];

			picture.Source = subApi.Url;
			picture.RegExPattern = subApi.Pattern;
		}


		protected void SetAPIs() {
			apiCollection.AddApi("waifu.pics", "\"url\":\"([^\"]*)\"");
			apiCollection.AddSubApi("waifu.pics", "Neko", "https://api.waifu.pics/sfw/neko");
			apiCollection.AddSubApi("waifu.pics", "NSFW Neko", "https://api.waifu.pics/nsfw/neko");

			apiCollection.AddApi("waifu.im", "\"url\":\"([^\"]*)\"");
			apiCollection.AddSubApi("waifu.im", "Maid", "https://api.waifu.im/search/?included_tags=maid");
			apiCollection.AddSubApi("waifu.im", "Waifu", "https://api.waifu.im/search/?included_tags=waifu");
			apiCollection.AddSubApi("waifu.im", "Marin Kitagawa", "https://api.waifu.im/search/?included_tags=marin-kitagawa");
			apiCollection.AddSubApi("waifu.im", "Mori Calliope", "https://api.waifu.im/search/?included_tags=mori-calliope");
			apiCollection.AddSubApi("waifu.im", "Raiden Shogun", "https://api.waifu.im/search/?included_tags=raiden-shogun");
			apiCollection.AddSubApi("waifu.im", "Oppai", "https://api.waifu.im/search/?included_tags=oppai");
			apiCollection.AddSubApi("waifu.im", "Selfies", "https://api.waifu.im/search/?included_tags=selfies");
			apiCollection.AddSubApi("waifu.im", "Uniform", "https://api.waifu.im/search/?included_tags=uniform");
		}

		protected void FillComboBoxSource() {
			comboBoxSource.Items.Clear();
			foreach (SubApi item in apiCollection.SubApis) {
				comboBoxSource.Items.Add(string.Format("{0} ({1})", item.SubApiName, item.ApiName));
			}
		}

		// Вручну створені обробники подій
		protected void ChangeUrl(object sender, UrlChangeEventArgs e) {
			textBoxPath.Text = e.NewUrl;
		}

		protected void ChangePicturePath(object sender, ChangePicturePathEventArgs e) {
			pictureBox.ImageLocation = e.NewPath;
		}
	}



	public class Message {
		public static void Error(string theme, string text) {
			MessageBox.Show(text, theme, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
