using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class MakeWordList : MonoBehaviour
{
    public string[] wordList;

    public string list1;
    public string list2;
    public string list3;
    public string list4;
    public string list5;
    public string list6;
    public string list7;
    public string list8;
    public string list9;
    public string list10;
    public string list11;
    public string list12;
    public string list13;
    public string list14;
    public string list15;

    public void Start() {
        LoadData();
        PrintList();
    }

    public void LoadData() {
        TextAsset textFile = Resources.Load("common") as TextAsset;
        wordList = textFile.text.Split(" ");
    }
  
    public void PrintList() {
        for (int i = 0; i < wordList.Length; i++) {
            if (i < 1000) {
                list1 += wordList[i].ToString();
                list1 += '\n';
            }

            else if (i < 2000) {
                list2 += wordList[i].ToString();
                list2 += '\n';
            }

            else if (i < 3000) {
                list3 += wordList[i].ToString();
                list3 += '\n';
            }

            else if (i < 4000) {
                list4 += wordList[i].ToString();
                list4 += '\n';
            }

            else if (i < 5000) {
                list5 += wordList[i].ToString();
                list5 += '\n';
            }

            else if (i < 6000) {
                list6 += wordList[i].ToString();
                list6 += '\n';
            }

            else if (i < 7000) {
                list7 += wordList[i].ToString();
                list7 += '\n';
            }

            else if (i < 8000) {
                list8 += wordList[i].ToString();
                list8 += '\n';
            }

            else if (i < 9000) {
                list9 += wordList[i].ToString();
                list9 += '\n';
            }

            else if (i < 10000) {
                list10 += wordList[i].ToString();
                list10 += '\n';
            }

            else if (i < 11000) {
                list11 += wordList[i].ToString();
                list11 += '\n';
            }

            else if (i < 12000) {
                list12 += wordList[i].ToString();
                list12 += '\n';
            }

            else if (i < 13000) {
                list13 += wordList[i].ToString();
                list13 += '\n';
            }

            else if (i < 14000) {
                list14 += wordList[i].ToString();
                list14 += '\n';
            }

            else if (i < 15000) {
                list15 += wordList[i].ToString();
                list15 += '\n';
            }
        }
    }
}
