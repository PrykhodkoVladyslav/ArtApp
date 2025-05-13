namespace Core.Picture;

public interface IPictureLibrary : IWithSerialization {
	string GetLocalPathByUrl(string url);
}
