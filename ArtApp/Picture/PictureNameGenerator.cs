namespace ArtApp {
	public interface IPictureNameGenerator : IWithSerialization {
		string Path { get; set; }
		int Index { get; }

		string CreatePictureName(in string pictureExtension);
	}

	public partial class PictureNameGenerator : IPictureNameGenerator {
		protected string path = "picture\\";
		protected int index = 0;

		public PictureNameGenerator() { }

		public string Path {
			get { return path; }
			set { path = value; }
		}
		public int Index {
			get { return index; }
		}

		public string CreatePictureName(in string pictureExtension) {
			string name = path + index.ToString() + pictureExtension;
			index++;
			return name;
		}
	}
}
