using UnityEngine;
using System.Collections;
using WyrmTale;
using System.IO;
using System;
using System.Collections.Generic;


[Serializable]
public class UserLocalDB  {


    public List<UserInfo> userInfoList;
  
	public static UserLocalDB LoadFromFile()
	{
		UserLocalDB _result = null;

		string filePath = Utility.DataPathWithDirectory ("LocalDB") + "/UserInfo." + ServerConstants.FILE_FORMAT;

		if(File.Exists(filePath))
		{
			string rawData = File.ReadAllText(filePath);

            _result = JsonUtility.FromJson<UserLocalDB> (rawData);
		}

		if (_result == null)
			_result = new UserLocalDB ();

		return _result;
	}

	public static void SaveToFile(UserLocalDB model)
	{
		string filePath = Utility.DataPathWithDirectory ("LocalDB") + "/UserInfo." + ServerConstants.FILE_FORMAT;

        string rawData = JsonUtility.ToJson(model);
		
		File.WriteAllText(filePath, rawData);

    }
}


[Serializable]
public class GameLocalData
{
    public int bestGlobalRank;
    public int leaguePreviousRank;
    public string previousLeagueID =  String.Empty;
    public string previousLeagueUniqueId = String.Empty;
    public int globalPreviousRank;

    public string currentLanguage;

   
}

[Serializable]
public class UserInfo
{
    public string userName;
    public string password;
    public string firName;
    public string lastName;
    public string SchoolName;
    //public string foreName;
    //public string surName;
    //public string schoolName;
    //public string country;
    //public string state;
    //public string city;
}




