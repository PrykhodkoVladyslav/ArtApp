using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtApp {
	public class ErrorLinkHistoryMoveException : Exception {
		public ErrorLinkHistoryMoveException() : this("Error LinkHistory move exception") { }
		public ErrorLinkHistoryMoveException(string message) : base(message) { }
	}

	public class PictureNotFoundInPictureLibraryException : Exception {
		public PictureNotFoundInPictureLibraryException() : this("Picture not found in PictureLibrary exception") { }
		public PictureNotFoundInPictureLibraryException(string message) : base(message) { }
	}

	public class PictureIsAvailableInPictureLibraryException : Exception {
		public PictureIsAvailableInPictureLibraryException() : this("Picture is available in PictureLibrary exception") { }
		public PictureIsAvailableInPictureLibraryException(string message) : base(message) { }
	}

	public class ApiThereAlreadyIsException : Exception {
		public ApiThereAlreadyIsException() : this("API there already is exception") { }
		public ApiThereAlreadyIsException(string message) : base(message) { }
	}

	public class ApiNotFoundException : Exception {
		public ApiNotFoundException() : this("API with this name not found exception") { }
		public ApiNotFoundException(string message) : base(message) { }
	}

	public class SubApiThereAlreadyIsException : Exception {
		public SubApiThereAlreadyIsException() : this("SubAPI there already is exception") { }
		public SubApiThereAlreadyIsException(string message) : base(message) { }
	}
}
