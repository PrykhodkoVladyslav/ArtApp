using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using ArtApp.Web;
using System.ComponentModel;

namespace ArtApp {
	public interface IPictureController : IWithSerialization {
		string RegExPattern { get; set; }
		string Source { get; set; }

		event PictureController.ChangePicturePathHandler ChangePicturePath;
		event PictureController.ChangeUrlHandler ChangeUrl;

		void LoadNext();
		void LoadPrev();
	}

	public partial class PictureController : IPictureController {
		protected ILinkHistory linkHistory;
		protected IPictureLibrary library;

		protected string source;
		protected string regExPattern;

		public delegate void ChangeUrlHandler(object sender, UrlChangeEventArgs e);
		public event ChangeUrlHandler ChangeUrl;

		public delegate void ChangePicturePathHandler(object sender, ChangePicturePathEventArgs e);
		public event ChangePicturePathHandler ChangePicturePath;

		protected delegate bool Can();
		protected delegate string Move();

		// Конструктори
		public PictureController() {
			this.linkHistory = new LinkHistory();
			this.library = new PictureLibrary();

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
		protected void ChangePicture(string url) {
			ChangePicturePath?.Invoke(this, new ChangePicturePathEventArgs(library.GetLocalPathByUrl(url)));

			ChangeUrl?.Invoke(this, new UrlChangeEventArgs(url));
		}

		protected void LoadPictureFromApi() {
			string url = WebLoad.GetPictureUrlFromApi(source, regExPattern);
			ChangePicture(url);
			linkHistory.Add(url);
		}

		public void LoadPrev() {
			LoadPicture(linkHistory.CanPrev, linkHistory.Prev);
		}

		public void LoadNext() {
			LoadPicture(linkHistory.CanNext, linkHistory.Next);
		}

		protected void LoadPicture(Can canChange, Move moveMethod) {
			// Закривання програми до завершення процесу викликає вийнятки які треба вирішити
			new Thread(
				() => {
					lock (this) {
						LoadPictureWithExceptionHandling(canChange, moveMethod);
					}
				}
			).Start();
		}

		protected void LoadPictureWithExceptionHandling(Can canChange, Move moveMethod) {
			try {
				LoadPictureMethod(canChange, moveMethod);
			}
			catch (System.Net.WebException) {
				Message.Error("Помилка", "Помилка мережі. Не вдалося завантажити зображення!");
			}
			//catch (Exception) {

			//}
		}

		protected void LoadPictureMethod(Can canChange, Move moveMethod) {
			if (canChange()) {
				ChangePicture(moveMethod());
			}
			else {
				LoadPictureFromApi();
			}
		}
	}
	public class UrlChangeEventArgs : EventArgs {
		protected string newUrl;

		public UrlChangeEventArgs(string newUrl) {
			this.newUrl = newUrl;
		}

		public string NewUrl {
			get { return newUrl; }
		}
	}

	public class ChangePicturePathEventArgs : EventArgs {
		protected string newPath;

		public ChangePicturePathEventArgs(string newPath) {
			this.newPath = newPath;
		}

		public string NewPath {
			get { return newPath; }
		}
	}
}