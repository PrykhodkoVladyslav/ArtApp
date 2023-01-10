using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ArtApp {
	public partial class PictureController : IWithSerialization {
		protected LinkHistory linkHistory;
		protected PictureLibrary library;

		protected PictureBox pictureBox;

		public delegate void ChangeUrlHandler(object sender, UrlEventArgs e);
		public event ChangeUrlHandler ChangeUrl;

		protected string source;
		protected string regExPattern;

		// Конструктори
		public PictureController(PictureBox pictureBox) {
			this.linkHistory = new LinkHistory();
			this.library = new PictureLibrary();

			this.pictureBox = pictureBox;

			this.source = "";
			this.regExPattern = "";
		}

		// Властивості
		public string Source {
			get { return source; }
			set { source = value; }
		}

		public string RegExPattern {
			get { return regExPattern; }
			set { regExPattern = value; }
		}


		// Методи
		protected void LoadPicture(string url) {
			pictureBox.ImageLocation = library.GetPathByUrl(url);

			ChangeUrl?.Invoke(this, new UrlEventArgs(url));
		}

		protected void LoadPictureFromApi() {
			string url = WebLoad.GetPictureUrl(source, regExPattern);
			LoadPicture(url);
			linkHistory.Add(url);
		}

		public void LoadPrev() {
			try {
				if (linkHistory.CanPrev()) {
					LoadPicture(linkHistory.Prev());
				}
			}
			catch (System.Net.WebException) {
				Message.Error("Помилка", "Помилка мережі. Не вдалося завантажити зображення!");
			}
			//catch (Exception) {

			//}
		}

		public void LoadNext() {
			try {
				if (linkHistory.CanNext()) {
					LoadPicture(linkHistory.Next());
				}
				else {
					LoadPictureFromApi();
				}
			}
			catch (System.Net.WebException) {
				Message.Error("Помилка", "Помилка мережі. Не вдалося завантажити зображення!");
			}
			//catch (Exception) {

			//}
		}
	}

	public class UrlEventArgs : EventArgs {
		protected string newUrl;

		public UrlEventArgs(string newUrl) {
			this.newUrl = newUrl;
		}

		public string NewUrl {
			get { return newUrl; }
		}
	}
}