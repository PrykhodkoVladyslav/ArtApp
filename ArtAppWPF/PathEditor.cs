using System;

namespace ArtAppWPF {
	public static class PathEditor {
		public static string GetFullPathByRelationPath(string relationPath) =>
			$"{AppDomain.CurrentDomain.BaseDirectory}\\{relationPath}";
	}
}
