/*
 * Created by SharpDevelop.
 * User: Goofy
 * Date: 5/21/2015
 * Time: 7:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using KitsuneDotNet.Attributes;

namespace KitsuneDotNet
{
	/// <summary>
	/// Description of RestInterfaceMetadata.
	/// </summary>
	public class RestInterfaceMetadata
	{
		public string BaseUrl {get; private set;}
		private readonly Dictionary<string, string> interfaceRequestHeaders = new Dictionary<string, string>();
		private readonly Dictionary<string, RestMethodMetadata> restMethodMetadata = new Dictionary<string, RestMethodMetadata>();
		public Dictionary<string, string> GlobalParameters {get; private set;}
		
		public RestInterfaceMetadata(Type type, string baseUrl, Dictionary<string, string> globalParameters)
		{
			this.BaseUrl = baseUrl;
			this.GlobalParameters = globalParameters;
			foreach(object obj in type.GetCustomAttributes(true))
			{
				var requestHeader = obj as RequestHeader;
				if (requestHeader != null)
				{
					this.interfaceRequestHeaders.Add(requestHeader.Name, requestHeader.Value);
				}
			}
			foreach(MethodInfo methodInfo in type.GetMethods())
			{
				if (!methodInfo.IsSpecialName)
				{
					this.restMethodMetadata.Add(methodInfo.Name, new RestMethodMetadata(methodInfo));
				}
			}
		}
		
		public RestInterfaceMetadata(Type type) : this(type, null, null)
		{
			
		}
		
		public Dictionary<string, string> GetMethodRequestHeaders(string methodName)
		{
			var requestHeaders = new Dictionary<string, string>();
			foreach(var header in this.restMethodMetadata[methodName].RequestHeaders)
			{
				requestHeaders.Add(header.Key, header.Value);
			}
			foreach(var header in interfaceRequestHeaders)
			{
				if (!requestHeaders.ContainsKey(header.Key))
					requestHeaders.Add(header.Key, header.Value);
			}
			
			return requestHeaders;
		}
		
		public RestMethodMetadata GetMethodMetadata(string methodName)
		{
			return this.restMethodMetadata[methodName];
		}
	}
}
