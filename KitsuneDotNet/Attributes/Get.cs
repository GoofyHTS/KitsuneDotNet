/*
 * Created by SharpDevelop.
 * User: Goofy
 * Date: 5/13/2015
 * Time: 8:47 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace KitsuneDotNet.Attributes
{
	/// <summary>
	/// Description of Get.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]	
	public class Get : Attribute
	{
		public string Url {get; set;}		
		
		public Get(string url)
		{
			this.Url = url;
		}
	}
}
