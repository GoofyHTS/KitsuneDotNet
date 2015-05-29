/*
 * Created by SharpDevelop.
 * User: Goofy
 * Date: 5/13/2015
 * Time: 8:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace KitsuneDotNet.Attributes
{
	/// <summary>
	/// Description of tHeader.
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = true)]
	public class RequestHeader : Attribute
	{
		public string Name {get; set;}
		public string Value {get; set;}
		
		public RequestHeader(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}
	}
}
