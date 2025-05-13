namespace Core.Picture;

public class PictureNotFoundInPictureLibraryException : Exception {
	public PictureNotFoundInPictureLibraryException() : this("Picture not found in PictureLibrary exception") { }
	public PictureNotFoundInPictureLibraryException(string message) : base(message) { }
}
