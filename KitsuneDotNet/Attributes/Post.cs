/*
 * Created by SharpDevelop.
 * User: Goofy
 * Date: 5/13/2015
 * Time: 9:07 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace KitsuneDotNet.Attributes
{
	/// <summary>
	/// Description of Post.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class Post : Attribute
	{
		public string Url {get; set;}
		public string JsonObject {get; set;}
		
		public Post(string url, string jsonObject)
		{
			this.Url = url;
			this.JsonObject = jsonObject;
		}
	}
}
