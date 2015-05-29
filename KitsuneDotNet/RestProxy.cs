/*
 * Created by SharpDevelop.
 * User: Goofy
 * Date: 5/13/2015
 * Time: 9:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using KitsuneDotNet.Attributes;

namespace KitsuneDotNet
{
	/// <summary>
	/// Description of RestProxy.
	/// </summary>
	public class RestProxy : RealProxy
	{
		private RestInterfaceMetadata restInterfaceMetadata;
		
		private RestProxy(Type type, string baseUrl, Dictionary<string, string> globalParameters) : base(type)
		{			
			this.restInterfaceMetadata = new RestInterfaceMetadata(type, baseUrl, globalParameters);
		}
		
		public static T Create<T>(string baseUrl)
		{
			return (T)new RestProxy(typeof(T), baseUrl, null).GetTransparentProxy();
		}
		public static T Create<T>(string baseUrl, Dictionary<string, string> globalParameters)
		{
			return (T)new RestProxy(typeof(T), baseUrl, globalParameters).GetTransparentProxy();
		}
		
		public override IMessage Invoke(IMessage msg)
		{
			var methodCall = (IMethodCallMessage)msg;			
            
			RequestMetadata requestMetadata = RequestHelper.GetRequestMetadata(this.restInterfaceMetadata, methodCall.MethodName, methodCall.Args, null);
			string value = HttpHelper.SendRequest(requestMetadata);

            return new ReturnMessage(value, null, 0, methodCall.LogicalCallContext, methodCall);
		}
	}
}
