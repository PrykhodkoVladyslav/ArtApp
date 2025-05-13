namespace Core;

public interface ILinkHistory : IWithSerialization {
	void AddToEnd(string url);
	string Next();
	string Prev();
	bool CanNext();
	bool CanPrev();
	bool IsCurentExist();
	string Curent();
	bool Contains(string url);
}
