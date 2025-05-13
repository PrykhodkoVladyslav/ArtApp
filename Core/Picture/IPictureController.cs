namespace Core.Picture;

public interface IPictureController : IWithSerialization {
	string RegExPattern { get; set; }
	string Source { get; set; }

	event EventHandler<ChangePictureEventArgs>? PictureChanged;

	event EventHandler<EventArgs>? OnWebException;

	void LoadNext();
	void LoadPrev();
}
