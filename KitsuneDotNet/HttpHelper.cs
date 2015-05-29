/*
 * Created by SharpDevelop.
 * User: Goofy
 * Date: 5/21/2015
 * Time: 8:26 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using KitsuneDotNet.Attributes;

namespace KitsuneDotNet
{
	/// <summary>
	/// Description of HttpRequest.
	/// </summary>
	public static class HttpHelper
	{
		
		public static string SendRequest(RequestMetadata requestMetadata)
		{
			if (requestMetadata.StaticResponse != null && requestMetadata.StaticResponse.Length > 0)
				return requestMetadata.StaticResponse;
			
			WebRequest webRequest = WebRequest.Create(requestMetadata.Url);
			webRequest.ContentType = "application/json";
			
			foreach(var header in requestMetadata.RequestHeaders)
			{
				if (header.Value != null)
					webRequest.Headers.Add(header.Key, header.Value);
			}
			
			if (requestMetadata.HttpType == RestMethodMetadata.HttpTypes.Get)
			{
				Console.WriteLine("Sending GET to " + requestMetadata.Url);
				WriteOutRequestHeaders(requestMetadata.RequestHeaders);
				webRequest.Method = "GET";
			}
			if (requestMetadata.HttpType == RestMethodMetadata.HttpTypes.Post)
			{
				Console.WriteLine("Sending POST to " + requestMetadata.Url);
				WriteOutRequestHeaders(requestMetadata.RequestHeaders);
				webRequest.Method = "POST";
				
				byte[] byteArray = Encoding.UTF8.GetBytes(requestMetadata.PostData);
				webRequest.ContentLength = byteArray.Length;
				using(Stream dataStream = webRequest.GetRequestStream())
				{	
					dataStream.Write(byteArray, 0, byteArray.Length);
				}
			}			
			
			string value;
			using(var webResponse = webRequest.GetResponse())
			{
				using(var objReader = new StreamReader(webResponse.GetResponseStream()))
				{
					value = objReader.ReadToEnd();
				}
			}

			return value;
		}
		
		private static void WriteOutRequestHeaders(Dictionary<string, string> requestHeaders)
		{
			Console.WriteLine("Request Headers:");
			foreach(var header in requestHeaders)
			{
				Console.WriteLine(header.Key + "=" + header.Value);
			}
			Console.WriteLine();
		}
	}
}
