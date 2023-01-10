using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace ArtApp {
	public interface IPictureLibrary {
		string GetPathByUrl(string url);
	}

	public partial class PictureLibrary : IPictureLibrary, IWithSerialization {
		protected Dictionary<string, string> pictures;
		protected string path;
		protected int index;

		public PictureLibrary() {
			pictures = new Dictionary<string, string>();
			path = "picture\\";
			index = 0;
		}

		public string GetPathByUrl(string url) {
			if (!Contains(url)) {
				Download(url);
			}

			if (!IsValidLocalPath(pictures[url])) {
				Redownload(url);
			}

			return pictures[url];
		}

		protected bool Contains(string url) {
			return pictures.ContainsKey(url);
		}

		protected bool IsValidLocalPath(string url) {
			try {
				using (new FileStream(url, FileMode.Open, FileAccess.Read)) { }
				return true;
			}
			catch (System.IO.FileNotFoundException) {
				return false;
			}
		}

		protected void Redownload(in string url) {
			pictures.Remove(url);
			Download(url);
		}

		protected void Download(in string url) {
			CreatePictureDirectoryIfNotFound();

			byte[] picture = new WebClient().DownloadData(url);
			string name = path + index.ToString() + GetImageFormatFromUrl(url);
			using (FileStream fs = new FileStream(name, FileMode.Create, FileAccess.Write)) {
				fs.Write(picture, 0, picture.Length);
			}

			pictures.Add(url, name);
			index++;
		}

		protected string GetImageFormatFromUrl(in string url) {
			int index = url.LastIndexOf('.');
			return url.Substring(index, url.Length - index);
		}

		protected void CreatePictureDirectoryIfNotFound() {
			DirectoryInfo directory = new DirectoryInfo(Environment.CurrentDirectory + "\\" + path);
			if (!directory.Exists)
				directory.Create();
		}
	}
}
