namespace Core.Picture;

public class PictureIsAvailableInPictureLibraryException : Exception {
	public PictureIsAvailableInPictureLibraryException() : this("Picture is available in PictureLibrary exception") { }
	public PictureIsAvailableInPictureLibraryException(string message) : base(message) { }
}
