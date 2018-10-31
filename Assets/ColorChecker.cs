using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChecker : MonoBehaviour {


    public string next_level;
    private int level_started = 0;
    private int current_time;
    public ProgressBar Pb;
    public GameObject popup;
    public GameObject settingPop;



    // Use this for initialization
    void Start () {
        ColorSprite.objColored += CheckColorStatus;
        level_started = 1;
        current_time = 0;
        levelStarted();
    }

    public void showsettingPop() {
        settingPop.SetActive(true);    
    }
    private void levelStarted()
    {
        // Start a timer every 1 second
        StartCoroutine("UpdateLevelTime");
        Pb.BarValue = 0;
        GameObject obj = GameObject.Find("HighScore");
        obj.GetComponent<Text>().text = PlayerPrefs.GetInt(gameObject.GetComponent<PiecesInfo>().level_name).ToString();

        

    }
    IEnumerator UpdateLevelTime()
    {
        while (level_started == 1)
        {
            current_time++;
            GameObject score = GameObject.Find("Score");
            int total_score = (gameObject.GetComponent<PiecesInfo>().piece_Count) * 10 + (100- current_time);
            score.GetComponent<Text>().text = total_score.ToString();
            Pb.BarValue += 1;
            yield return new WaitForSeconds(1f);
        }
       

    }
    public void CheckColorStatus()
    {
        int matched_colors = 0;
        int pieces_count = gameObject.GetComponent<PiecesInfo>().piece_Count;
        for (int i = 1; i <= pieces_count; ++i)
        {
            string source_game_obj_name = "Piece_" + i;
            string target_game_obj_name = "Colored_Piece_" + i;
            GameObject source_obj = GameObject.Find(source_game_obj_name);
            GameObject target_obj = GameObject.Find(target_game_obj_name);
           // Debug.Log("Pieces Info" + i);
          //  Debug.Log(source_obj);
            //Debug.Log(target_obj);

            Color32 c1 = source_obj.GetComponent<SpriteRenderer>().color;
          
            Color32 c2 = target_obj.GetComponent<SpriteRenderer>().color;
           // Debug.Log(c1);
           //s Debug.Log(c2);

            if ((Mathf.Abs(c1.g - c2.g) < 3) && (Mathf.Abs(c1.b - c2.b) < 3) && (Mathf.Abs(c1.r - c2.r) < 3)){
                 
                Debug.Log("Match Found");
                matched_colors ++;
            }
            else{
                Debug.Log("Match Not Found");
                break;
            }
        }
        if(matched_colors == pieces_count){
            Debug.Log("DONE COLORED");
            if (next_level.Length == 0)
            {

                Debug.Log("Level load requested for " + name);
#pragma warning disable CS0618 // Type or member is obsolete
                Application.LoadLevel("Menu");
#pragma warning restore CS0618 // Type or member is obsolete
            }
            else
            {                //Load the prefab specified in the next level
                             // gameObject.SetActive(false);
                             ///GameObject next = GameObject.Find(next_level);
                // next.SetActive(true);
              
                ColorSprite.objColored -= CheckColorStatus;
                levelCompleted();
              

            }
        }
    }

    void levelCompleted()
    {
        int bonus = 100 - (int)Pb.BarValue;
        int total_score = (gameObject.GetComponent<PiecesInfo>().piece_Count) * 10 + bonus;
        level_started = 0;
        if(PlayerPrefs.GetInt(gameObject.GetComponent<PiecesInfo>().level_name) == 0)
            PlayerPrefs.SetInt(gameObject.GetComponent<PiecesInfo>().level_name, total_score);
        if (PlayerPrefs.GetInt(gameObject.GetComponent<PiecesInfo>().level_name) < total_score)
            PlayerPrefs.SetInt(gameObject.GetComponent<PiecesInfo>().level_name, total_score);
        
        popup.SetActive(true);
        UpdatePopupScore(PlayerPrefs.GetInt(gameObject.GetComponent<PiecesInfo>().level_name), total_score);

        //Save current score as high score if more than high score
    }
    void UpdatePopupScore(int high_score, int current_score)
    {
        Debug.Log("Updating Popup Score");
        GameObject obj = GameObject.Find("CurrentScoreText");
        obj.GetComponent<Text>().text = current_score.ToString();

        GameObject obj1 = GameObject.Find("HighScoreText");
        obj1.GetComponent<Text>().text = high_score.ToString();
    }

    public void onLoadNextLevel() {
        GameObject adsManager = GameObject.Find("AdsManager");
        UnityAdsExample ads = adsManager.GetComponent<UnityAdsExample>();
        ads.ShowRewardedAd();
        gameObject.SetActive(false);

        GameObject next = (GameObject)Instantiate(Resources.Load("Prefabs/" + next_level));

    }
    public void ReloadLevel()
    {
        gameObject.SetActive(false);

        GameObject next = (GameObject)Instantiate(Resources.Load("Prefabs/" + gameObject.GetComponent<PiecesInfo>().level_name));
    }

    public void hideSettingsDialog()
    {
        GameObject settings = GameObject.Find("SettingsColorActivityDialog");
        settings.SetActive(false);
    }
}
