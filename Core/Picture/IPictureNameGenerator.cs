namespace Core.Picture;

public interface IPictureNameGenerator : IWithSerialization {
	string Path { get; set; }
	int Index { get; }

	string CreatePictureName(in string pictureExtension);
}
