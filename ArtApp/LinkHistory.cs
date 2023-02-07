using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArtApp {
	public interface ILinkHistory : IWithSerialization {
		void Add(string url);
		string Next();
		string Prev();
		bool CanNext();
		bool CanPrev();
		bool IsCurentExist();
		string Curent();
		bool Contains(string url);
	}

	public partial class LinkHistory : ILinkHistory {
		protected List<string> urlList;
		int index;

		public LinkHistory() {
			urlList = new List<string>();
			index = -1;
		}

		public void Add(string url) {
			urlList.Add(url);
			index++;
		}

		public string Next() {
			if (!CanNext()) {
				throw new ErrorLinkHistoryMoveException();
			}

			return urlList[++index];
		}

		public string Prev() {
			if (!CanPrev()) {
				throw new ErrorLinkHistoryMoveException();
			}

			return urlList[--index];
		}

		public bool CanNext() {
			return index + 1 < urlList.Count;
		}

		public bool CanPrev() {
			return index - 1 >= 0;
		}

		public bool IsCurentExist() {
			return 0 <= index && index < urlList.Count;
		}

		public string Curent() {
			return urlList[index];
		}

		public bool Contains(string url) {
			return urlList.Contains(url);
		}
	}

	public class ErrorLinkHistoryMoveException : Exception {
		public ErrorLinkHistoryMoveException() : this("Error LinkHistory move exception") { }
		public ErrorLinkHistoryMoveException(string message) : base(message) { }
	}
}
