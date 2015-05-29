/*
 * Created by SharpDevelop.
 * User: Goofy
 * Date: 5/18/2015
 * Time: 11:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.RegularExpressions;
using KitsuneDotNet.Attributes;

namespace KitsuneDotNet
{
	/// <summary>
	/// Description of RestMethod.
	/// </summary>
	public class RestMethodMetadata
	{
		public enum HttpTypes
		{
			Get,
			Post
		};
		
		public string Name {get; private set;}
		public HttpTypes HttpType {get; private set;}
		public string Url {get; private set;}
		public Dictionary<string, string> RequestHeaders {get; private set;}
		public Dictionary<string, RestMethodParamMetadata> Parameters {get; set;}
		public string StaticResponse {get; set;}
		
		public RestMethodMetadata(MethodInfo methodInfo)
		{
			this.RequestHeaders = new Dictionary<string, string>();
			this.Parameters = new Dictionary<string, RestMethodParamMetadata>();
			
			GetMethodMetaData(methodInfo);
		}
		
		private void GetMethodMetaData(MethodInfo methodInfo)
		{
			if (methodInfo != null)
			{
				this.Name = methodInfo.Name;
				
				foreach(object attributeObj in methodInfo.GetCustomAttributes())
				{
					var getAttribute = attributeObj as Get;
					if (getAttribute != null)
					{
						this.HttpType = HttpTypes.Get;
						this.Url = getAttribute.Url;
					}
					
					var postAttribute = attributeObj as Post;
					if (postAttribute != null)
					{
						this.HttpType = HttpTypes.Post;
						this.Url = postAttribute.Url;
					}
					
					var requestHeadersAttributes = attributeObj as RequestHeader;
					if (requestHeadersAttributes != null)
					{
						this.RequestHeaders.Add(requestHeadersAttributes.Name, requestHeadersAttributes.Value);
					}
					
					var staticResponseAttribute = attributeObj as StaticResponse;
					if (staticResponseAttribute != null)
					{
						this.StaticResponse = staticResponseAttribute.Value;
					}
				}
				
				
				foreach(ParameterInfo parameterInfo in methodInfo.GetParameters())
		        {					
					string parameterName = parameterInfo.Name;
					Type parameterType = parameterInfo.ParameterType;
					int parameterIndex = parameterInfo.Position;
					RestMethodParamMetadata restMethodParamMetadata = new RestMethodParamMetadata(parameterName, parameterType, parameterIndex);
					this.Parameters.Add(parameterName, restMethodParamMetadata);
		        }
			}
		}		
	}
}
