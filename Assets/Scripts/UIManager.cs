using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject[] persimmonIcons;
    int iconCount = 0;
    [SerializeField] TMP_Text songpyeonCountTxt;
    [SerializeField] int songpyeonCount = 5;
    int curSongpyeonCount;

    void Awake()
    {
        if(persimmonIcons == null)
        {
            persimmonIcons = GameObject.FindGameObjectsWithTag("Icon");
        }
        iconCount = persimmonIcons.Length;
        curSongpyeonCount = songpyeonCount;
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

        iconCount = persimmonIcons.Length;
        curSongpyeonCount = songpyeonCount;

        songpyeonCountTxt.text = curSongpyeonCount + "";
    }


    public bool ReducePersimmonIcons()
    {
        iconCount--;
        if(iconCount == 0)
        {
            return true;
        }

        Debug.Log("Icon Count " + iconCount);
        persimmonIcons[iconCount].SetActive(false);
        return false;
    }

    public bool ReduceSongPyeon()
    {
        curSongpyeonCount--;
        if(curSongpyeonCount <= 0)
        {
            return true;

        }
        else
        {
            songpyeonCountTxt.text = curSongpyeonCount + "";
            return false;
        }
    }

    

}
