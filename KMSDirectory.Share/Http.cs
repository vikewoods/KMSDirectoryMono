using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KMSDirectory.Share
{
	public class Http
	{
		public Http()
		{
		}

		public static int Request(string url, List<Employee> lstEmployee)
		{
			lstEmployee.Clear();

			// Now handle the result from the WebClient
			var request = (HttpWebRequest)WebRequest.Create (url);
			request.ContentType = "application/json";
			request.Method = "GET";
			request.Timeout = 600000;
			
			using (var response = (HttpWebResponse) request.GetResponse ()) {
				if (response.StatusCode != HttpStatusCode.OK) {
					return 0;
				} else {
					using (var reader = new StreamReader(response.GetResponseStream ())) {
						//JsonValue root = JsonValue.Load (streamReader);
						//List<Employee> questions = ParseJsonAndLoadQuestions ((JsonObject)root);
						
						var content = reader.ReadToEnd ();
						
						if (string.IsNullOrWhiteSpace (content)) {
							return 0;
						} else {
							lstEmployee = JsonConvert.DeserializeObject<List<Employee>> (content);
							//var deserializer = new DataContract DataContractJsonSerializer(); // Xamarin's api

							return 1;
						}
						
						return 0;
					}
				}

				return 0;
			}
		}
	}
}