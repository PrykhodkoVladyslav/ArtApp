using Core.Api;
using Core.Picture;
using System.Xml;

namespace ArtAppWinForms;

public partial class Form1 : Form {
	protected readonly string saveXmlFilePath = "save.xml";

	protected IPictureController picture;
	protected IApiCollection apiCollection;

	public Form1() {
		InitializeComponent();

		picture = new PictureController();
		picture.PictureChanged += ChangePicture;
		picture.OnWebException += OnWebException;

		apiCollection = new ApiCollection();

		SetAPIs();
		FillComboBoxSource();

		Form1_SizeChanged(this, new EventArgs());

		try {
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(saveXmlFilePath);
			picture.LoadDataFromXmlNode(xmlDocument.FirstChild);
		}
		catch (FileNotFoundException) { }

		ComboBoxSource.SelectedIndex = 0;
	}

	private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
		if (picture is PictureController pictureController && pictureController.IsProcessingInAnotherThreads) {
			Message.Error("Помилка", "Процес перегортання ще не виконано до кінця, зачекайте завершення");
			e.Cancel = true;
			return;
		}

		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.AppendChild(picture.CreateXMLNode(xmlDocument));
		xmlDocument.Save(saveXmlFilePath);
	}

	private void Form1_SizeChanged(object sender, EventArgs e) {
		PictureBox.Size = new Size(Width - PictureBox.Location.X * 2, Height - 15 - (PictureBox.Location.Y) * 2);
	}
	private void ButtonPrev_Click(object sender, EventArgs e) {
		picture.LoadPrev();
	}

	private void ButtonNext_Click(object sender, EventArgs e) {
		picture.LoadNext();
	}

	private void BomboBoxSource_SelectedIndexChanged(object sender, EventArgs e) {
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

	protected void ChangePicture(object? sender, ChangePictureEventArgs e) {
		PictureBox.Invoke(
			(MethodInvoker)delegate {
				PictureBox.ImageLocation = e.NewPath;
			}
		);
		TextBoxPath.Invoke(
			(MethodInvoker)delegate {
				TextBoxPath.Text = e.NewUrl;
			}
		);
	}

	private void OnWebException(object? sender, EventArgs e) {
		Message.Error("Помилка", "Помилка мережі. Не вдалося завантажити зображення!");
	}
}
