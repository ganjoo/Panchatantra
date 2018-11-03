
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private bool isProcessing = false;

    private string shareText = "Which Hollywood Movie does this PICTURE represent?\n";
    private string gameLink = "Download the game on play store at " + "\nhttps://play.google.com/store/apps/details?id=com.TGC.guessthemovie&pcampaignid=GPC_shareGame";
    private string subject = "Rebus Guess The Movie Game";
    private string imageName = "share"; // without the extension, for iinstance, MyPic 

    public GameObject settingsObj;
    public void LoadLevel(string name)
    {
        
        Debug.Log("Level load requested for " + name);
        SceneManager.LoadScene(name);
       // InitializeAds ads = gameObject.GetComponent<InitializeAds>();
//        ads.showInterstitialAd();

    }
    private void Start()

    {
        GameObject text_obj = GameObject.Find("Player_Info_Text");
        if(text_obj != null )
            text_obj.GetComponent<UnityEngine.UI.Text>().text ="Hello " + PlayerStats.name;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width * 0.8f && Input.mousePosition.y < Screen.height * 0.2f)
        {
         
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Level load requested for " + name);
            #pragma warning disable CS0618 // Type or member is obsolete
                        Application.LoadLevel("Menu");
            #pragma warning restore CS0618 // Type or member is obsolete

            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
    public void shareImage()
    {
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");
        if (result == AndroidRuntimePermissions.Permission.Granted)
            Debug.Log("We have permission to access external storage!");
        else
            Debug.Log("Permission state: " + result);

        // Requesting WRITE_EXTERNAL_STORAGE and CAMERA permissions simultaneously
        //AndroidRuntimePermissions.Permission[] result = AndroidRuntimePermissions.RequestPermissions( "android.permission.WRITE_EXTERNAL_STORAGE", "android.permission.CAMERA" );
        //if( result[0] == AndroidRuntimePermissions.Permission.Granted && result[1] == AndroidRuntimePermissions.Permission.Granted )
        //	Debug.Log( "We have all the permissions!" );
        //else
        //	Debug.Log( "Some permission(s) are not granted..." );
        if (!isProcessing)
            StartCoroutine(ShareScreenshot());

    }

    private IEnumerator ShareScreenshot()
    {
        isProcessing = true;
        yield return new WaitForEndOfFrame();

        Texture2D screenTexture = new Texture2D(1080, 1080, TextureFormat.RGB24, true);
        screenTexture.Apply();

        byte[] dataToSave = Resources.Load<TextAsset>(imageName).bytes;

        string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
        Debug.Log(destination);
        File.WriteAllBytes(destination, dataToSave);

        if (!Application.isEditor)
        {

            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareText + gameLink);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            currentActivity.Call("startActivity", intentObject);

        }

        isProcessing = false;

    }


    public void showSettings() {
        settingsObj.SetActive(true);
    }

    public void EnterAsGuest() {

        PlayerStats.name = "Guest";
        LoadLevel("Menu");

    }
}