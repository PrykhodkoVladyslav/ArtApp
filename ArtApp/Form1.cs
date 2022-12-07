using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtApp {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();

			comboBoxSource.Items.Add(@"https://api.waifu.pics/sfw/neko");
			comboBoxSource.Items.Add(@"https://api.waifu.pics/nsfw/neko");
			comboBoxSource.SelectedIndex = 0;
		}

		private void buttonUpdate_Click(object sender, EventArgs e) {
			try {
				LoadPicture();
			}
			catch (System.Net.WebException) {
				MessageBox.Show("Помилка мережі. Не вдалося завантажити зображення", "Помилка", MessageBoxButtons.OK);
			}
			catch (Exception) {
				MessageBox.Show("Під час завантаження зображення виникла невідома помилка", "Невідома помилка", MessageBoxButtons.OK);
			}
		}

		private void LoadPicture() {
			string url = comboBoxSource.SelectedItem.ToString();
			HttpWebRequest myHttwebrequest = (HttpWebRequest)HttpWebRequest.Create(url);
			HttpWebResponse myHttpWebresponse = (HttpWebResponse)myHttwebrequest.GetResponse();
			StreamReader strm = new StreamReader(myHttpWebresponse.GetResponseStream());
			string htmlText = strm.ReadToEnd();

			string pattern = "{\"url\":\"(.*)\"}";

			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			Match match = regex.Match(htmlText);

			pictureBox.ImageLocation = textBoxPath.Text = match.Groups[1].Value;
		}

		private void Form1_SizeChanged(object sender, EventArgs e) {
			pictureBox.Size = new Size(Width - pictureBox.Location.X * 2, Height - 30 - (pictureBox.Location.Y) * 2);
		}
	}

	public class LinkHistory {

	}
}
