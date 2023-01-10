using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArtApp {
	public interface IWithSerialization {
		void Save(string path);
		void Save(FileStream fs);
		void Save(BinaryWriter bw);

		void Load(string path);
		void Load(FileStream fs);
		void Load(BinaryReader br);
	}

	public partial class PictureController {
		public void Save(string path) {
			using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write)) {
				Save(fs);
			}
		}
		public void Save(FileStream fs) {
			using (BinaryWriter bw = new BinaryWriter(fs)) {
				Save(bw);
			}
		}
		public void Save(BinaryWriter bw) {
			linkHistory.Save(bw);
			library.Save(bw);
			bw.Write(source);
		}

		public void Load(string path) {
			using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
				Load(fs);
			}
		}
		public void Load(FileStream fs) {
			using (BinaryReader br = new BinaryReader(fs)) {
				Load(br);
			}
		}
		public void Load(BinaryReader br) {
			linkHistory.Load(br);
			library.Load(br);
			source = br.ReadString();
		}
	}

	public partial class LinkHistory {
		public void Save(string path) {
			using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write)) {
				Save(fs);
			}
		}
		public void Save(FileStream fs) {
			using (BinaryWriter bw = new BinaryWriter(fs)) {
				Save(bw);
			}
		}
		public void Save(BinaryWriter bw) {
			bw.Write(urlList.Count);
			foreach (string url in urlList) {
				bw.Write(url);
			}
			bw.Write(index);
		}

		public void Load(string path) {
			using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
				Load(fs);
			}
		}
		public void Load(FileStream fs) {
			using (BinaryReader br = new BinaryReader(fs)) {
				Load(br);
			}
		}
		public void Load(BinaryReader br) {
			int urlListCount = br.ReadInt32();
			urlList.Clear();
			for (int i = 0; i < urlListCount; i++) {
				urlList.Add(br.ReadString());
			}
			index = br.ReadInt32();
		}
	}

	public partial class PictureLibrary {
		public void Save(string path) {
			using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write)) {
				Save(fs);
			}
		}
		public void Save(FileStream fs) {
			using (BinaryWriter bw = new BinaryWriter(fs)) {
				Save(bw);
			}
		}
		public void Save(BinaryWriter bw) {
			bw.Write(pictures.Count);
			foreach (KeyValuePair<string, string> pair in pictures) {
				bw.Write(pair.Key);
				bw.Write(pair.Value);
			}
			nameGenerator.Save(bw);
		}

		public void Load(string path) {
			using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
				Load(fs);
			}
		}
		public void Load(FileStream fs) {
			using (BinaryReader br = new BinaryReader(fs)) {
				Load(br);
			}
		}
		public void Load(BinaryReader br) {
			int picturesCount = br.ReadInt32();
			pictures.Clear();
			for (int i = 0; i < picturesCount; i++) {
				string key = br.ReadString();
				string value = br.ReadString();

				pictures.Add(key, value);
			}
			nameGenerator.Load(br);
		}
	}

	public partial class PictureNameGenerator {
		public void Save(string path) {
			using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write)) {
				Save(fs);
			}
		}
		public void Save(FileStream fs) {
			using (BinaryWriter bw = new BinaryWriter(fs)) {
				Save(bw);
			}
		}
		public void Save(BinaryWriter bw) {
			bw.Write(path);
			bw.Write(index);
		}

		public void Load(string path) {
			using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
				Load(fs);
			}
		}
		public void Load(FileStream fs) {
			using (BinaryReader br = new BinaryReader(fs)) {
				Load(br);
			}
		}
		public void Load(BinaryReader br) {
			path = br.ReadString();
			index = br.ReadInt32();
		}
	}
}
