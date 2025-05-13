using System.Windows;

namespace ArtAppWPF;

public class Message {
	public static void Error(string theme, string text) {
		MessageBox.Show(text, theme, MessageBoxButton.OK, MessageBoxImage.Error);
	}
}
