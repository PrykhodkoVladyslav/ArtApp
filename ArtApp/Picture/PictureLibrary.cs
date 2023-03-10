using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Security.Policy;
using System.Reflection;
using System.Xml.Linq;

namespace ArtApp {
	public interface IPictureLibrary : IWithSerialization {
		string GetLocalPathByUrl(string url);
	}

	public partial class PictureLibrary : IPictureLibrary {
		protected Dictionary<string, string> pictures = new Dictionary<string, string>();
		protected PictureNameGenerator nameGenerator = new PictureNameGenerator();

		public PictureLibrary() { }

		public string GetLocalPathByUrl(string url) {
			if (!Contains(url)) {
				Download(url);
			}

			if (!IsValidPicturePath(pictures[url])) {
				Redownload(url);
			}

			return pictures[url];
		}

		protected bool Contains(string url) {
			return pictures.ContainsKey(url);
		}

		protected bool IsValidPicturePath(string url) {
			return File.Exists(url);
		}

		protected void Redownload(in string url) {
			pictures.Remove(url);
			Download(url);
		}

		protected void Download(in string url) {
			CreatePictureDirectoryIfNotFound();

			byte[] picture = new WebClient().DownloadData(url);
			string name = nameGenerator.CreatePictureName(GetImageFormatFromUrl(url));
			SaveByteArrayToFile(picture, name);

			pictures.Add(url, name);
		}

		protected void SaveByteArrayToFile(byte[] picture, string name) {
			using (FileStream fs = new FileStream(name, FileMode.Create, FileAccess.Write)) {
				fs.Write(picture, 0, picture.Length);
			}
		}

		protected string GetImageFormatFromUrl(in string url) {
			int index = url.LastIndexOf('.');
			return url.Substring(index, url.Length - index);
		}

		protected void CreatePictureDirectoryIfNotFound() {
			DirectoryInfo directory = new DirectoryInfo(Environment.CurrentDirectory + "\\" + nameGenerator.Path);
			if (!directory.Exists)
				directory.Create();
		}
	}

	public class PictureNotFoundInPictureLibraryException : Exception {
		public PictureNotFoundInPictureLibraryException() : this("Picture not found in PictureLibrary exception") { }
		public PictureNotFoundInPictureLibraryException(string message) : base(message) { }
	}

	public class PictureIsAvailableInPictureLibraryException : Exception {
		public PictureIsAvailableInPictureLibraryException() : this("Picture is available in PictureLibrary exception") { }
		public PictureIsAvailableInPictureLibraryException(string message) : base(message) { }
	}
}
