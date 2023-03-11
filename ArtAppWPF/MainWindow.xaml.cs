using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;

using ArtApp;
using ArtApp.Api;

namespace ArtAppWPF {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		protected readonly string saveXmlFilePath = "save.xml";

		protected IPictureController picture;
		protected IApiCollection apiCollection;

		public MainWindow() {
			InitializeComponent();

			picture = new PictureController();
			picture.PictureChanged += ChangePicture;

			apiCollection = new ApiCollection();

			SetAPIs();
			FillComboBoxSource();

			try {
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(saveXmlFilePath);
				picture.LoadDataFromXmlNode(xmlDocument.FirstChild);
			}
			catch (System.IO.FileNotFoundException) { }

			ComboBoxSource.SelectedIndex = 0;
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if (picture is PictureController pictureController && pictureController.IsProcessingInAnotherThreads) {
				Message.Error("Помилка", "Процес перегортання ще не виконано до кінця, зачекайте завершення");
				e.Cancel = true;
				return;
			}

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.AppendChild(picture.CreateXMLNode(xmlDocument));
			xmlDocument.Save(saveXmlFilePath);
		}

		private void ButtonPrev_Click(object sender, RoutedEventArgs e) {
			picture.LoadPrev();
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e) {
			picture.LoadNext();
		}

		private void ComboBoxSource_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			SubApi subApi = apiCollection[ComboBoxSource.SelectedIndex];

			picture.Source = subApi.Url;
			picture.RegExPattern = subApi.Pattern;
		}

		// Методи
		protected void SetAPIs() {
			apiCollection.AddApi("waifu.pics", "\"url\":\"([^\"]*)\"");
			apiCollection.AddSubApi("waifu.pics", "Neko", "https://api.waifu.pics/sfw/neko");
			apiCollection.AddSubApi("waifu.pics", "NSFW Neko", "https://api.waifu.pics/nsfw/neko");

			apiCollection.AddApi("waifu.im", "\"url\":\"([^\"]*)\"");
			apiCollection.AddSubApi("waifu.im", "Maid", "https://api.waifu.im/search/?included_tags=maid");
			apiCollection.AddSubApi("waifu.im", "Waifu", "https://api.waifu.im/search/?included_tags=waifu");
			apiCollection.AddSubApi("waifu.im", "Marin Kitagawa", "https://api.waifu.im/search/?included_tags=marin-kitagawa");
			apiCollection.AddSubApi("waifu.im", "Mori Calliope", "https://api.waifu.im/search/?included_tags=mori-calliope");
			apiCollection.AddSubApi("waifu.im", "Raiden Shogun", "https://api.waifu.im/search/?included_tags=raiden-shogun");
			apiCollection.AddSubApi("waifu.im", "Oppai", "https://api.waifu.im/search/?included_tags=oppai");
			apiCollection.AddSubApi("waifu.im", "Selfies", "https://api.waifu.im/search/?included_tags=selfies");
			apiCollection.AddSubApi("waifu.im", "Uniform", "https://api.waifu.im/search/?included_tags=uniform");
		}

		protected void FillComboBoxSource() {
			ComboBoxSource.Items.Clear();
			foreach (SubApi item in apiCollection.SubApis) {
				ComboBoxSource.Items.Add($"{item.SubApiName} ({item.ApiName})");
			}
		}

		// Вручну створені обробники подій
		protected void ChangePicture(object sender, ChangePictureEventArgs e) {
			Application.Current.Dispatcher.Invoke(
				() => ChangePicture(e.NewPath, e.NewUrl)
			);
		}

		protected void ChangePicture(string imagePath, string imageUrl) {
			Art.Source = new BitmapImage(new Uri(PathEditor.GetFullPathByRelationPath(imagePath)));
			TextBlockUrl.Text = imageUrl;
		}
	}



	public class Message {
		public static void Error(string theme, string text) {
			MessageBox.Show(text, theme, MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}
