using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthenticationManager : MonoBehaviour
{

    public InputField userName, password;
    public InputField userName_reg, password_reg, confirmPwd, firName, lastName, SchoolName;//, forName, surName, schoolName, country, state, city;

    public GameObject signContent, signUpContent, congratsContent, loginSuccessContent;

    public GameObject SigninPanel;

    public Text reg_stutus,sign_status;

    private void Start()
    {
        if (SigninPanel.GetComponent<KeepLoggedIn>().checkedbutton == enabled)
        {
            userName.text = PlayerPrefs.GetString("UserName");
            password.text = PlayerPrefs.GetString("Password");
        }
        else{
            userName.text = string.Empty;
            password.text = string.Empty;
        }
    }

    public void SignIn()
    {

        if (ValidateCredetialsFromLocalDB(userName.text, password.text))
        {
            if (SigninPanel.GetComponent<KeepLoggedIn>().checkedbutton == enabled)
            {
                PlayerPrefs.SetString("UserName", userName.text.ToString());
                PlayerPrefs.SetString("Password", password.text.ToString());
            }
            signContent.SetActive(false);
            loginSuccessContent.SetActive(true);
            userName.text = string.Empty;
            password.text = string.Empty;
            Debug.Log("successFully logged in");
            Application.LoadLevel(1);
        }
        else
        {
            sign_status.text = "Wrong Credentials";
            Debug.Log("wrong credentials");
        }
    }
    public void Home()
    {

        signContent.SetActive(true);
        loginSuccessContent.SetActive(false);
        signUpContent.SetActive(false);
    }

    public void SignUp()
    {
        userName.text = string.Empty;
        password.text = string.Empty;
        signContent.SetActive(false);
        signUpContent.SetActive(true);
    }
    public void Register()
    {
        // Regstration_API();


        UserLocalDB localDB = ServerAPIHandler.Instance.userLocalDB;

        UserInfo userInfo = new UserInfo();
        userInfo.userName = userName_reg.text;
        userInfo.password = password_reg.text;
        userInfo.firName = firName.text;
        userInfo.lastName = lastName.text;
        userInfo.SchoolName = SchoolName.text;

        if (password_reg.text.Equals(confirmPwd.text))
        {

            if (localDB.userInfoList == null)
                localDB.userInfoList = new List<UserInfo>();

            bool isUserAlreadyAvailable = false;
            for (int i = 0; i < localDB.userInfoList.Count; i++)
            {
                if (localDB.userInfoList[i].userName.Equals(userInfo.userName))
                {
                    isUserAlreadyAvailable = true;
                    continue;
                }
            }

            if (!isUserAlreadyAvailable)
            {
                localDB.userInfoList.Add(userInfo);

                UserLocalDB.SaveToFile(localDB);

                signUpContent.SetActive(false);
                congratsContent.SetActive(true);

                userName_reg.text = string.Empty;
                 password_reg.text = string.Empty;
                confirmPwd.text = string.Empty;
               firName.text = string.Empty;
               lastName.text = string.Empty; ;
                 SchoolName.text = string.Empty; ;
                Debug.Log("registration successfull");
            }
            else
            {
                reg_stutus.text = "User Available";
                Debug.Log("user already available");
            }
        }
        else
        {
            reg_stutus.text = "Password not matching";

            Debug.Log("password missMatch");     
        }

    }

    public void OnClickOpenSignContent()
    {
        signContent.SetActive(true);
        congratsContent.SetActive(false);
    }

    bool ValidateCredetialsFromLocalDB(string _userName, string _passWord)
    {
        UserLocalDB localDB = ServerAPIHandler.Instance.userLocalDB;
        if (localDB.userInfoList != null)
        {
            for (int i = 0; i < localDB.userInfoList.Count; i++)
            {
                if (localDB.userInfoList[i].userName.Equals(_userName) && localDB.userInfoList[i].password.Equals(_passWord))
                {
                    return true;
                }
            }
        }


        return false;
    }


    #region API CALL
    void LoginValidationAPI()
    {
        QueryData serverRequestInfo = new QueryData
        {
            requestUpdateType = SERVER_UPDATE_REQUEST_TYPE.LOGIN
        };


        serverRequestInfo.keyValuePairs.Add(ServerConstants.USER_NAME, "srikanth");
        serverRequestInfo.keyValuePairs.Add(ServerConstants.PASSWORD, "srikanth");



        serverRequestInfo.statusResponseString.Add(ServerConstants.VALIDATION_RESULT);

        ServerAPIHandler.Instance.serverStateManager.UpdateGameState(serverRequestInfo, (object callBack) =>
        {
            //if (callBack == null || callBack.GetType().Equals(typeof(bool)))
            //{
            //    DisplayPopUP_InternetCheck();
            //}


        });
    }

    void Regstration_API(string username = null,
                         string password = null,
                         string forname = null,
                         string surname = null,
                         string schoolName = null,
                         string country = null,
                         string state = null,
                         string city = null)
    {
        QueryData serverRequestInfo = new QueryData
        {
            requestUpdateType = SERVER_UPDATE_REQUEST_TYPE.RIGISTERER
        };


        serverRequestInfo.keyValuePairs.Add(ServerConstants.USER_NAME, "mail.vsrikanth@gmail.com");
        serverRequestInfo.keyValuePairs.Add(ServerConstants.PASSWORD, "sri@123");
        serverRequestInfo.keyValuePairs.Add(ServerConstants.FORENAME, "srikanth");
        serverRequestInfo.keyValuePairs.Add(ServerConstants.SURNAME, "vallepu");
        serverRequestInfo.keyValuePairs.Add(ServerConstants.SCHOOL_NAME, "cmr");
        serverRequestInfo.keyValuePairs.Add(ServerConstants.COUNTRY, 100);
        serverRequestInfo.keyValuePairs.Add(ServerConstants.STATE, 50);
        serverRequestInfo.keyValuePairs.Add(ServerConstants.CITY, 10);

        serverRequestInfo.webFormData = new WWWForm();

        serverRequestInfo.webFormData.AddField(ServerConstants.USER_NAME, "mail.vsrikanth@gmail.com");
        serverRequestInfo.webFormData.AddField(ServerConstants.PASSWORD, "sri@123");
        serverRequestInfo.webFormData.AddField(ServerConstants.FORENAME, "srikanth");
        serverRequestInfo.webFormData.AddField(ServerConstants.SURNAME, "vallepu");
        serverRequestInfo.webFormData.AddField(ServerConstants.SCHOOL_NAME, "cmr");
        serverRequestInfo.webFormData.AddField(ServerConstants.COUNTRY, 100);
        serverRequestInfo.webFormData.AddField(ServerConstants.STATE, 50);
        serverRequestInfo.webFormData.AddField(ServerConstants.CITY, 10);




        // serverRequestInfo.statusResponseString.Add(ServerConstants.VALIDATION_RESULT);

        ServerAPIHandler.Instance.serverStateManager.UpdateGameState(serverRequestInfo, (object callBack) =>
        {
            //if (callBack == null || callBack.GetType().Equals(typeof(bool)))
            //{
            //    DisplayPopUP_InternetCheck();
            //}


        });
    }
    #endregion
}
