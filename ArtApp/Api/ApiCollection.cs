using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtApp;

namespace ArtApp.Api {
	interface IApiCollection {
		SubApi this[int index] { get; }

		List<SubApi> SubApis { get; }

		void AddApi(string name, string pattern);
		void AddSubApi(string apiName, string subApiName, string url);
	}

	class ApiCollection : IApiCollection {
		protected List<Api> apis;

		public ApiCollection() {
			apis = new List<Api>();
		}

		public void AddApi(string name, string pattern) {
			if (apis.Any(api => api.Name.Equals(name))) {
				throw new ApiThereAlreadyIsException();
			}

			apis.Add(
				new Api { Name = name, Pattern = pattern }
			);
		}

		public void AddSubApi(string apiName, string subApiName, string url) {
			if (!apis.Any(x => x.Name.Equals(apiName))) {
				throw new ApiNotFoundException();
			}

			Api api = apis.First(x => x.Name.Equals(apiName));

			if (api.SubApis.Any(x => x.SubApiName.Equals(subApiName))) {
				throw new SubApiThereAlreadyIsException();
			}

			api.Add(subApiName, url);
		}

		public List<SubApi> SubApis {
			get {
				List<SubApi> subApi = new List<SubApi>();
				foreach (var item in apis) {
					subApi.AddRange(item.SubApis);
				}

				return subApi;
			}
		}

		public SubApi this[int index] {
			get { return SubApis[index]; }
		}
	}

	class Api {
		protected string name;
		protected string pattern;
		protected List<SubApi> subApis;

		public Api() {
			this.subApis = new List<SubApi>();
			this.name = "";
			this.pattern = "";
		}

		public string Name {
			get { return name; }
			set { name = value; }
		}
		public string Pattern {
			get { return pattern; }
			set { pattern = value; }
		}
		public List<SubApi> SubApis {
			get { return subApis; }
		}

		public void Add(string name, string url) {
			subApis.Add(
				new SubApi(this) {
					SubApiName = name,
					Url = url
				}
			);
		}
	}

	class SubApi {
		protected string name;
		protected Api parent;
		protected string url;

		public SubApi(Api parent) {
			this.name = "";
			this.parent = parent;
			this.url = "";
		}

		public string SubApiName {
			get { return name; }
			set { name = value; }
		}

		public string ApiName {
			get { return parent.Name; }
		}

		public string Pattern {
			get { return parent.Pattern; }
		}

		public string Url {
			get { return url; }
			set { url = value; }
		}
	}

	public abstract class ApiException : Exception {
		public ApiException() : this("API exception") { }
		public ApiException(string message) : base(message) { }
	}

	public class ApiThereAlreadyIsException : ApiException {
		public ApiThereAlreadyIsException() : this("API there already is exception") { }
		public ApiThereAlreadyIsException(string message) : base(message) { }
	}

	public class ApiNotFoundException : ApiException {
		public ApiNotFoundException() : this("API with this name not found exception") { }
		public ApiNotFoundException(string message) : base(message) { }
	}

	public class SubApiThereAlreadyIsException : ApiException {
		public SubApiThereAlreadyIsException() : this("SubAPI there already is exception") { }
		public SubApiThereAlreadyIsException(string message) : base(message) { }
	}
}
