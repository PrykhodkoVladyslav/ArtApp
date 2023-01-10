using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ArtApp {
	public class WebLoad {
		private static string LoadHTML(string url) {
			HttpWebRequest myHttwebrequest = (HttpWebRequest)HttpWebRequest.Create(url);
			HttpWebResponse myHttpWebresponse = (HttpWebResponse)myHttwebrequest.GetResponse();
			StreamReader strm = new StreamReader(myHttpWebresponse.GetResponseStream());
			return strm.ReadToEnd();
		}

		public static string GetPictureUrl(string source, string regExPattern) {
			string htmlText = WebLoad.LoadHTML(source);

			Regex regex = new Regex(regExPattern, RegexOptions.IgnoreCase);
			Match match = regex.Match(htmlText);

			return match.Groups[1].Value;
		}
	}
}
