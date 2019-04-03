using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using MiniJSON;
using System.Net;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization;

public class ServerStateManager : MonoBehaviour
{
    public void UpdateGameState(QueryData queryData, Action<object> actionCallBack = null)
    {

        string query = ConstructQuery(queryData);
      

        StartCoroutine(ServerAPIHandler.Instance.webRequestHandler.Request(ServerConstants.SERVER_GAME_STATE_URL, WEB_REQUEST_TYPE.GRAPH_QUERY, queryData.webFormData, query, (WebRequestInfo status) =>
        {


            if (status.isSuccess)
            {
                OnGameStateUpdateSuccess(status.responceDataString, queryData, actionCallBack);
            }
            else
            {

                if (actionCallBack != null)
                {
                    actionCallBack(false);
                }
            }

        }));
    }

    void OnGameStateUpdateSuccess(string responceDataString, QueryData queryData, Action<object> actionCallBack = null)
    {
        bool updateResponceSuccess = false;
        Dictionary<string, object> _responceCode = new Dictionary<string, object>();

        foreach (KeyValuePair<string, object> statusResponceKeypair in Json.Deserialize(responceDataString) as Dictionary<string, object>)
        {
            if (statusResponceKeypair.Key.Equals("status"))
            {
                if (statusResponceKeypair.Value.ToString().Equals("200")) // Update Success
                {
                    updateResponceSuccess = true;
                }
            }
            else if (queryData.statusResponseString.Contains(statusResponceKeypair.Key))
            {
                if (statusResponceKeypair.Value == null)
                {
                    if (actionCallBack != null)
                    {
                        actionCallBack(false);
                    }
                   
                }
                else
                {
                    _responceCode.Add(statusResponceKeypair.Key, statusResponceKeypair.Value);
                }
            }
        }

        if (updateResponceSuccess)
        {

            if (queryData.statusResponseString.Equals(string.Empty))
            {
                if (actionCallBack != null)
                {
                    actionCallBack(true);
                }
            }
            else
            {
                if (actionCallBack != null)
                {
                    actionCallBack(_responceCode);
                }
            }

            //request response validation
            object validationResponse = false;
            _responceCode.TryGetValue((string)ServerConstants.VALIDATION_RESULT, out validationResponse);

            if (validationResponse != null && !(bool)validationResponse)
            {
                if (actionCallBack != null)
                {
                    actionCallBack(false);
                }
            }


        }
        else
        {
            if (actionCallBack != null)
            {
                actionCallBack(false);
            }
        }
    }

   
    string ConstructQuery(QueryData queryData)
    {
        string updateQueryHeader = GetRequestUpdateType(queryData.requestUpdateType);

        Dictionary<string, object> keyValuePairs = queryData.keyValuePairs;

        string updateString = string.Empty;

        if (keyValuePairs.Keys != null)
        {
            foreach (var key in keyValuePairs.Keys)
            {
                object keyValue = keyValuePairs[key];

                if (!updateString.Equals(string.Empty))
                {
                    updateString += ",";
                }

                if (keyValue.GetType().Equals(typeof(int)))
                {
                    updateString += ServerConstants.quotationMark + key + ServerConstants.quotationMark + ":" + (int)keyValue;
                }
                else if (keyValue.GetType().Equals(typeof(bool)))
                {
                    updateString += ServerConstants.quotationMark + key+ServerConstants.quotationMark  + ":" + ((bool)keyValue);
                }
                else
                {
                    updateString += ServerConstants.quotationMark + key+ ServerConstants.quotationMark + ":" + ServerConstants.quotationMark + keyValue + ServerConstants.quotationMark;
                }
            }
        }

        string statsString = "}";//"){status}}";

        //if (queryData.statusResponseString.Count > 0)
        //{
        //    string statusResponce = string.Empty;

        //    for (int i = 0; i < queryData.statusResponseString.Count; i++)
        //    {
        //        if (i != 0)
        //        {
        //            statusResponce += ",";
        //        }
        //        statusResponce += queryData.statusResponseString[i];
        //    }

        //    statsString = "){status," + statusResponce + "}}";
        //}

        return updateQueryHeader + updateString + statsString;
    }

    string GetRequestUpdateType(SERVER_UPDATE_REQUEST_TYPE uPDATE_TYPE)
    {
        string query = string.Empty;

        switch (uPDATE_TYPE)
        {
            case SERVER_UPDATE_REQUEST_TYPE.LOGIN:
                query = ServerConstants.LOGIN_REQUEST_QUERY;
                break;
            case SERVER_UPDATE_REQUEST_TYPE.RIGISTERER:
                query = ServerConstants.REGISTER_REQUEST_QUERY;
                break;
        }

        return query;
    }


}

public class QueryData
{
    public SERVER_UPDATE_REQUEST_TYPE requestUpdateType;
    public Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
    public List<string> statusResponseString = new List<string>();
    public WWWForm webFormData;
}



