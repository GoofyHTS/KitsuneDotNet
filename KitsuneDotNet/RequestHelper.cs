/*
 * Created by SharpDevelop.
 * User: Goofy
 * Date: 5/21/2015
 * Time: 9:36 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace KitsuneDotNet
{
	/// <summary>
	/// Description of RequestHelper.
	/// </summary>
	public static class RequestHelper
	{
		private static readonly Dictionary<string, int> placeholderParameterIndex = new Dictionary<string, int>();
		
		public static RequestMetadata GetRequestMetadata(RestInterfaceMetadata restInterfaceMetadata, string methodName, object[] parameters, object post_data)
		{
			RestMethodMetadata restMethodMetadata = restInterfaceMetadata.GetMethodMetadata(methodName);
			
			StringBuilder staticResponse = new StringBuilder(restMethodMetadata.StaticResponse);
			
			RestMethodMetadata.HttpTypes httpType = restMethodMetadata.HttpType;
			
			LinkPlaceHolderWithParameter(restMethodMetadata.Url, restMethodMetadata);
			LinkPlaceHolderWithParameter(staticResponse.ToString(), restMethodMetadata);
			
			var url = new StringBuilder();
			if (restInterfaceMetadata.BaseUrl != null)
				url.Append(restInterfaceMetadata.BaseUrl);
			url.Append(restMethodMetadata.Url);
			
			foreach(var placeHolderParameter in placeholderParameterIndex)
			{
				url.Replace(placeHolderParameter.Key, Convert.ToString(parameters[placeHolderParameter.Value]));
				staticResponse.Replace(placeHolderParameter.Key, Convert.ToString(parameters[placeHolderParameter.Value]));
			}
			
			ReplaceGlobalParameters(url, restInterfaceMetadata.GlobalParameters);
			
			string urlString = url.ToString();
			
			string json_post_data = JsonConvert.SerializeObject(post_data);
			
			Dictionary<string, string> requestHeaders = restInterfaceMetadata.GetMethodRequestHeaders(methodName);
			var newRequestHeaders = new Dictionary<string, string>();
			foreach(var header in requestHeaders)
			{
				LinkPlaceHolderWithParameter(header.Value, restMethodMetadata);
				var requestHeaderValue = new StringBuilder(header.Value);
				foreach(var placeHolderParameter in placeholderParameterIndex)
				{
					requestHeaderValue.Replace(placeHolderParameter.Key, Convert.ToString(parameters[placeHolderParameter.Value]));
				}
				
				ReplaceGlobalParameters(requestHeaderValue, restInterfaceMetadata.GlobalParameters);
				
				newRequestHeaders.Add(header.Key, requestHeaderValue.ToString());
			}
						
			ReplaceGlobalParameters(staticResponse, restInterfaceMetadata.GlobalParameters);
						
			RequestMetadata requestMetadata = new RequestMetadata(urlString, httpType, newRequestHeaders, json_post_data, staticResponse.ToString());
			
			return requestMetadata;
		}
		
		private static void ReplaceGlobalParameters(StringBuilder value, Dictionary<string, string> globalParameters) 
		{
			if (globalParameters != null)
			{
				foreach(var globalParam in globalParameters)
				{
					var globParamPlaceHolder = new StringBuilder("{");
					globParamPlaceHolder.Append(globalParam.Key);
					globParamPlaceHolder.Append("}");
					value.Replace(globParamPlaceHolder.ToString(), globalParam.Value);
				}
			}
		}
		
		private static void LinkPlaceHolderWithParameter(String value, RestMethodMetadata restMethodMetadata)
		{			
			if (value != null)
			{
				Regex regex = new Regex(@"\{([^\}]+)\}");
				foreach(Match match in regex.Matches(value))
				{
					string placeholderCurls = match.Groups[0].Value;
					string placeholder = match.Groups[1].Value;
					foreach(string parameterName in restMethodMetadata.Parameters.Keys)
					{
						if (parameterName.Equals(placeholder))
						{
							if (!placeholderParameterIndex.ContainsKey(placeholderCurls))
							{
								RestMethodParamMetadata restMethodParamMetadata = restMethodMetadata.Parameters[parameterName];
								placeholderParameterIndex.Add(placeholderCurls, restMethodParamMetadata.Index);
							}
						}					
					}
				}
			}			
		}
	}
}
