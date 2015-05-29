/*
 * Created by SharpDevelop.
 * User: Goofy
 * Date: 5/23/2015
 * Time: 9:28 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KitsuneDotNet
{
	/// <summary>
	/// Description of RequestMetadata.
	/// </summary>
	public class RequestMetadata
	{
		public string Url {get; private set;}
		public RestMethodMetadata.HttpTypes HttpType {get; private set;}
		public Dictionary<string, string> RequestHeaders {get; private set;}
		public string PostData {get; private set;}
		public string StaticResponse {get; set;}
		
		public RequestMetadata(string url, RestMethodMetadata.HttpTypes httpType, Dictionary<string, string> requestHeaders, string postData, string staticResponse)
		{
			this.Url = url;
			this.HttpType = httpType;
			this.RequestHeaders = requestHeaders;
			this.PostData = postData;
			this.StaticResponse = staticResponse;
		}
	}
}
