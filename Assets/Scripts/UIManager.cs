using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject[] persimmonIcons;
    [SerializeField] TMP_Text songpyeonCountTxt;
    [SerializeField] int songpyeonCount = 5;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetStart()
    {
        foreach(var arr in persimmonIcons)
        {
            arr.SetActive(true);
        }
        songpyeonCountTxt.text = songpyeonCount + "";
    }


    public void ReduceSongPyeon()
    {
        songpyeonCount--;
        if(songpyeonCount == 0)
        {
            //게임 오버 

        }
        else
        {
            songpyeonCountTxt.text = songpyeonCount + "";
        }
    }

    

}
