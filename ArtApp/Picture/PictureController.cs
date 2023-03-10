using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using ArtApp.Web;

namespace ArtApp {
	public interface IPictureController : IWithSerialization {
		string RegExPattern { get; set; }
		string Source { get; set; }

		event PictureController.ChangePictureHandler PictureChanged;

		void LoadNext();
		void LoadPrev();
	}

	public partial class PictureController : IPictureController {
		protected ILinkHistory linkHistory = new LinkHistory();
		protected IPictureLibrary library = new PictureLibrary();

		protected string source = "";
		protected string regExPattern = "";

		protected int threadsCounter = 0;

		public delegate void ChangePictureHandler(object sender, ChangePictureEventArgs e);
		public event ChangePictureHandler PictureChanged;

		protected delegate void MoveMethod();

		// Конструктори
		public PictureController() { }

		// Властивості
		public string Source {
			get { return source; }
			set { source = value ?? throw new ArgumentNullException("source"); }
		}

		public string RegExPattern {
			get { return regExPattern; }
			set { regExPattern = value ?? throw new ArgumentNullException("regExPattern"); }
		}


		// Методи
		protected void ChangePicture(string url) {
			PictureChanged?.Invoke(this, new ChangePictureEventArgs(url, library.GetLocalPathByUrl(url)));
		}

		protected void LoadPictureFromApi() {
			string url = WebLoad.GetPictureUrlFromApi(source, regExPattern);
			ChangePicture(url);
			linkHistory.AddToEnd(url);
		}

		public void LoadPrev() => LoadPicture(LoadPrevPictureMethod);
		public void LoadNext() => LoadPicture(LoadNextPictureMethod);

		protected void LoadPicture(MoveMethod moveMethod) {
			// Закривання програми до завершення процесу викликає вийнятки які треба вирішити
			new Thread(
				() => {
					LoadPictureUsingAnotherThread(moveMethod);
				}
			).Start();
		}

		protected void LoadPictureUsingAnotherThread(MoveMethod moveMethod) {
			Interlocked.Increment(ref threadsCounter);
			try {
				LoadPictureWithExceptionHandling(moveMethod);
			}
			finally {
				Interlocked.Decrement(ref threadsCounter);
			}
		}

		protected void LoadPictureWithExceptionHandling(MoveMethod moveMethod) {
			try {
				lock (this) { moveMethod(); }
			}
			catch (System.Net.WebException) {
				Message.Error("Помилка", "Помилка мережі. Не вдалося завантажити зображення!");
			}
		}

		protected void LoadPrevPictureMethod() {
			if (linkHistory.CanPrev()) {
				ChangePicture(linkHistory.Prev());
			}
		}

		protected void LoadNextPictureMethod() {
			if (linkHistory.CanNext()) {
				ChangePicture(linkHistory.Next());
			}
			else {
				LoadPictureFromApi();
			}
		}
	}

	public class ChangePictureEventArgs : EventArgs {
		protected string newUrl;
		protected string newPath;

		public ChangePictureEventArgs(string newUrl, string newPath) {
			this.newUrl = newUrl;
			this.newPath = newPath;
		}

		public string NewUrl {
			get { return newUrl; }
		}

		public string NewPath {
			get { return newPath; }
		}
	}
}