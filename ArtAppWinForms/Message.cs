namespace ArtAppWinForms;

public class Message {
	public static void Error(string theme, string text) {
		MessageBox.Show(text, theme, MessageBoxButtons.OK, MessageBoxIcon.Error);
	}
}
