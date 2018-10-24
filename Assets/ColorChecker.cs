using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChecker : MonoBehaviour {


    public string next_level;
    private int level_started = 0;
    private int current_time;
    // Use this for initialization
	void Start () {
        ColorSprite.objColored += CheckColorStatus;
        level_started = 1;
        current_time = 0;
        levelStarted();
    }

    private void levelStarted()
    {
        // Start a timer every 1 second
        StartCoroutine("UpdateLevelTime");

       

    }
    IEnumerator UpdateLevelTime()
    {
        while (level_started == 1)
        {
            current_time++;
            GameObject score = GameObject.Find("Score");
             score.GetComponent<Text>().text = current_time.ToString();
            yield return new WaitForSeconds(.1f);
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

                LevelManager.LoadLevel("Menu");
            }
            else
            {                //Load the prefab specified in the next level
                             // gameObject.SetActive(false);
                             ///GameObject next = GameObject.Find(next_level);
                // next.SetActive(true);
                gameObject.SetActive(false);
                
                ColorSprite.objColored -= CheckColorStatus;
                levelCompleted();
                Debug.Log("Instantiating " + next_level);
                GameObject next = (GameObject)Instantiate(Resources.Load("Prefabs/" + next_level));

            }
        }
    }

    void levelCompleted()
    {
        level_started = 0;
        //Save current score as high score if more than high score
    }
}
