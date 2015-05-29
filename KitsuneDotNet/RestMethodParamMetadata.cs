/*
 * Created by SharpDevelop.
 * User: Goofy
 * Date: 5/20/2015
 * Time: 12:03 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace KitsuneDotNet
{
	/// <summary>
	/// Description of RestMethodParamMetadata.
	/// </summary>
	public class RestMethodParamMetadata
	{
		public string Name {get; private set;}
		public Type ParamType {get; private set;}
		public int Index {get; private set;}
		
		public RestMethodParamMetadata(string name, Type paramType, int index)
		{
			this.Name = name;
			this.ParamType = paramType;
			this.Index = index;
		}
	}
}
