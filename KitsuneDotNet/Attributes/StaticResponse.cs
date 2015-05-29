/*
 * Created by SharpDevelop.
 * User: Goofy
 * Date: 5/29/2015
 * Time: 9:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace KitsuneDotNet.Attributes
{
	/// <summary>
	/// Description of StaticResponse.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class StaticResponse : Attribute
	{
		public string Value {get; set;}
		
		public StaticResponse(string value)
		{
			this.Value = value;
		}
	}
}
