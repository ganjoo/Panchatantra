using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FBLogin : MonoBehaviour {

    private string LastResponse;
    private string Status;
	// Use this for initialization
	void Start () {
        FB.Init(this.OnInitComplete, this.OnHideUnity);
        this.Status = "FB.Init() called with " + FB.AppId;
    }
	

    public void CallFBLogin()
    {
        FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends" }, this.HandleResult);
    }
    protected void HandleResult(IResult result)
    {
        if (result == null)
        {
            this.LastResponse = "Null Response\n";
            Debug.Log(this.LastResponse);
            return;
        }

      

        // Some platforms return the empty string instead of null.
        if (!string.IsNullOrEmpty(result.Error))
        {
            this.Status = "Error - Check log for details";
            this.LastResponse = "Error Response:\n" + result.Error;
        }
        else if (result.Cancelled)
        {
            this.Status = "Cancelled - Check log for details";
            this.LastResponse = "Cancelled Response:\n" + result.RawResult;
        }
        else if (!string.IsNullOrEmpty(result.RawResult))
        {
            this.Status = "Success - Check log for details";
            this.LastResponse = "Success Response:\n" + result.RawResult;
           

            FB.API("/me?fields=id,name,email", HttpMethod.GET, GetFacebookInfo, new Dictionary<string, string>() { });
           

        }
        else
        {
            this.LastResponse = "Empty Response\n";
        }

        Debug.Log(result.ToString());
    }
    public void GetFacebookInfo(IResult result)
    {
        if (result.Error == null)
        {
            Debug.Log(result.ResultDictionary["id"].ToString());
            Debug.Log(result.ResultDictionary["name"].ToString());
            Debug.Log(result.ResultDictionary["email"].ToString());
            PlayerStats.email = result.ResultDictionary["email"].ToString();
            PlayerStats.name = result.ResultDictionary["name"].ToString();

            //Write fb id, email and name in local db
            PlayerPrefs.SetString("fb_id", result.ResultDictionary["id"].ToString());
            PlayerPrefs.SetString("fb_email", result.ResultDictionary["email"].ToString());
            PlayerPrefs.SetString("fb_name", result.ResultDictionary["name"].ToString());

#pragma warning disable CS0618 // Type or member is obsolete
            Application.LoadLevel("Menu");
#pragma warning restore CS0618 // Type or member is obsolete
        }
        else
        {
            Debug.Log(result.Error);
        }
    }
    private void OnInitComplete()
    {
        this.Status = "Success - Check log for details";
        this.LastResponse = "Success Response: OnInitComplete Called\n";
        string logMessage = string.Format(
            "OnInitCompleteCalled IsLoggedIn='{0}' IsInitialized='{1}'",
            FB.IsLoggedIn,
            FB.IsInitialized);
        Debug.Log(logMessage);
        if (AccessToken.CurrentAccessToken != null)
        {
            Debug.Log(AccessToken.CurrentAccessToken.ToString());
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        this.Status = "Success - Check log for details";
        this.LastResponse = string.Format("Success Response: OnHideUnity Called {0}\n", isGameShown);
        Debug.Log("Is game shown: " + isGameShown);
    }
}
