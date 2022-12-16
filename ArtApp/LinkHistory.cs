using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArtApp {
	public partial class LinkHistory : IWithSerialization {
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

		//public string Curent() {
		//	return urlList[index];
		//}

		public bool Contains(string url) {
			return urlList.Contains(url);
		}
	}
}
