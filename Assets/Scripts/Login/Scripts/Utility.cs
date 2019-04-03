using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net;
using System.IO;


#if UNITY_IOS
using UnityEngine.iOS;
#endif
using UnityEngine.EventSystems;


public class Utility  
{
	
	public static bool internetAvialble = true;


	public static string DataPathWithDirectory(string dir = null)
	{
		string rootPath = Application.persistentDataPath;

		rootPath = rootPath + "/" + "FabLabDoc";

		if(dir != null && dir.Length >= 0)
			rootPath = rootPath +  "/" + dir;

		if (!Directory.Exists (rootPath)) {
			Directory.CreateDirectory(rootPath);
		}

		return rootPath;
	}
}
