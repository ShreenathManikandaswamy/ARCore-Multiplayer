using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using MiniJSON;
using System.Security.Cryptography;

public class webRequestHandler : MonoBehaviour {

    // Don't declare local variables
    public IEnumerator Request(string url,
                               WEB_REQUEST_TYPE webRequestMethodType,
                               WWWForm wWWForm = null,
                               string query = null,
                               Action<WebRequestInfo> callBackWithData = null,
                               string userName = null)
    {

        //if (!InternetValidator.Instance.IsInternetAvailable)
        //{
        //    WebRequestInfo info = new WebRequestInfo
        //    {
        //        isInterNetConnectionAvailable = false,
        //        isSuccess = false
        //    };
        //    callBackWithData(info);
        //}
        //else
        {
            switch (webRequestMethodType)
            {
                case WEB_REQUEST_TYPE.POST:
                    {
                        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(url, wWWForm))
                        {
                            // unityWebRequest.timeout = (int)ServerConstants.REQUEST_TIMEOUT;

                           // unityWebRequest.chunkedTransfer = true;
                            yield return unityWebRequest.SendWebRequest();

                            WebRequestInfo info = new WebRequestInfo();

                           // InternetValidator.Instance.IsInternetAvailable = !unityWebRequest.isNetworkError;

                            if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
                            {
                                info.errorDescription = unityWebRequest.error;
                                info.isSuccess = false;
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(unityWebRequest.error) && unityWebRequest.isDone)
                                {
                                    info.callBackData = unityWebRequest.downloadHandler.data;

                                    //string responseText = Encoding.UTF8.GetString(info.callBackData);

                                    info.isSuccess = true;
                                }
                                else
                                {
                                    info.errorDescription = unityWebRequest.error;
                                    info.isSuccess = false;
                                }
                            }
                            yield return new WaitForEndOfFrame();
                            callBackWithData(info);
                        }
                    }
                    break;

                case WEB_REQUEST_TYPE.GET:
                    {
                        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url))
                        {
                            unityWebRequest.timeout = (int)ServerConstants.REQUEST_TIMEOUT;

                            yield return unityWebRequest.SendWebRequest();

                            //Utilities.DebugLogColor("Data progress : " + unityWebRequest.downloadProgress, "red");

                            WebRequestInfo info = new WebRequestInfo();

                          //  InternetValidator.Instance.IsInternetAvailable = !unityWebRequest.isNetworkError;

                            if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
                            {
                                info.errorDescription = unityWebRequest.error;
                                info.isSuccess = false;
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(unityWebRequest.error) && unityWebRequest.isDone)
                                {
                                    if (string.IsNullOrEmpty(unityWebRequest.downloadHandler.text))
                                    {
                                        info.errorDescription = "Content not available";
                                        info.isSuccess = false;
                                    }
                                    else
                                    {
                                        info.isSuccess = true;
                                        info.callBackData = unityWebRequest.downloadHandler.data;

                                        //string responseText = Encoding.UTF8.GetString(info.callBackData);

                                    }
                                }
                                else
                                {
                                    info.errorDescription = unityWebRequest.error;
                                    info.isSuccess = false;
                                }
                            }
                            yield return new WaitForEndOfFrame();
                            callBackWithData(info);
                        }
                    }
                    break;

                case WEB_REQUEST_TYPE.GRAPH_QUERY:
                    {
                        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(url, UnityWebRequest.kHttpVerbPOST))
                        {
                            var fullQuery = new GraphQLQuery()
                            {
                                query = query,
                            };

                            string json = JsonUtility.ToJson(fullQuery);

                            byte[] payload = Encoding.UTF8.GetBytes(json);
                            UploadHandler headerData = new UploadHandlerRaw(payload);

                            unityWebRequest.uploadHandler = headerData;
                            //unityWebRequest.SetRequestHeader("Content-Type", "application/json");

                            //    unityWebRequest.SetRequestHeader("username", "example");
                            //unityWebRequest.SetRequestHeader("code", "");

                        //    unityWebRequest.SetRequestHeader("buildtype", ServerGameStateConstants.HEADER_BUILD_TYPE); //1 - Production, 0 - Sandbox

                            //string nonce = GetNonce();
                            //unityWebRequest.SetRequestHeader("nonce", nonce);

                            //string payLoadString = Encoding.UTF8.GetString(payload);
                            //string signature = CalculateMD5Hash(nonce + payLoadString);
                            //unityWebRequest.SetRequestHeader("signature", signature);

                            unityWebRequest.timeout = (int)ServerConstants.REQUEST_TIMEOUT;

                            yield return unityWebRequest.SendWebRequest();

                            WebRequestInfo info = new WebRequestInfo();

                           // InternetValidator.Instance.IsInternetAvailable = !unityWebRequest.isNetworkError;

                            if (unityWebRequest.responseCode == 504) // 504 Gateway Timeout error, Unable to reach server.
                            {
                                info.isServerDown = true;
                                callBackWithData(info);
                                yield break;
                            }

                            if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
                            {
                                info.errorDescription = unityWebRequest.error;
                                info.isSuccess = false;
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(unityWebRequest.error) && unityWebRequest.isDone)
                                {
                                    string callbackDataInString = System.Text.Encoding.UTF8.GetString(unityWebRequest.downloadHandler.data);
                                    Dictionary<string, object> response = Json.Deserialize(callbackDataInString) as Dictionary<string, object>;
                                    if (response.ContainsKey("data"))
                                    {
                                        foreach (KeyValuePair<string, object> author in response)
                                        {
                                            if (author.Key.Contains("errors"))
                                            {
                                                info.errorDescription = "Data Not Available";
                                                info.isSuccess = false;
                                                break;
                                            }
                                            //  if (string.IsNullOrEmpty(author.Value.ToString()) || author.Value == null)
                                            //{
                                            //    MainGameHandler.Instance.gameSessionManager.DisplayPopUP_withButtons( HCConstants.NO_INTERNET_CONNECTION, HCConstants.Alert, HCConstants.Alert_No_internet);
                                            //}


                                            info.responseObj = author.Value;
                                            foreach (KeyValuePair<string, object> responceKeypair in author.Value as Dictionary<string, object>)
                                            {

                                                string responceValueString = Json.Serialize(responceKeypair.Value as Dictionary<string, object>);
                                                if (!string.IsNullOrEmpty(responceValueString))
                                                {
                                                    info.responceDataString = responceValueString;

                                                    //MainGameHandler.Instance.copyUrl.addURL(url, query, wWWForm, info.responceDataString);

                                                    info.isSuccess = true;
                                                }
                                                else
                                                {
                                                    info.errorDescription = "Data Not Available";
                                                    info.isSuccess = false;
                                                }
                                            }


                                        }


                                    }
                                    else
                                    {
                                        info.errorDescription = "Data Not Available";
                                        info.isSuccess = false;
                                    }
                                }
                                else
                                {
                                    info.errorDescription = unityWebRequest.error;
                                    info.isSuccess = false;
                                }
                            }
                            yield return new WaitForEndOfFrame();
                            callBackWithData(info);
                        }

                    }
                    break;
            }
        }
    }
    public string GetNonce()
    {
        string uuid = Guid.NewGuid().ToString();
        return uuid;
    }

    public string CalculateMD5Hash(string inputString)
    {
        StringBuilder hash = new StringBuilder();
        MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
        byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(inputString));

        for (int i = 0; i < bytes.Length; i++)
        {
            hash.Append(bytes[i].ToString("x2")); //lowerCase; X2 if uppercase desired
        }
        return hash.ToString();
    }
}

public class WebRequestInfo
{
    public bool isSuccess = false;
    public bool isInterNetConnectionAvailable = true;
    public byte[] callBackData = null;
    public string errorDescription = String.Empty;
    public string responceDataString = null;
    public object responseObj = null;
    public bool isServerDown = false;
}

[System.Serializable]
public class GraphQLQuery
{
    public string query;
}
