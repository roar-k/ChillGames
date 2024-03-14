using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class MakeWordList : MonoBehaviour
{
    private string[] newWordList;
    public string newListOne;
    public string newListTwo;
    public string newListThree;
    public string newListFour;
    public string newListFive;

    public void Start() {
        LoadData();
        NewListOne();
        NewListTwo();
        NewListThree();
        NewListFour();
        NewListFive();
    }
    public void LoadData() {
        TextAsset textFile = Resources.Load("common6") as TextAsset;
        newWordList = textFile.text.Split(" ");
    }

    public void NewListOne() {
        for (int i = 7500; i < 9000; i++) {
            newListOne += newWordList[i].ToString();
            newListOne += '\n';
        }
    }

    public void NewListTwo() {
        for (int i = 9000; i < 10500; i++) {
            newListTwo += newWordList[i].ToString();
            newListTwo += '\n';
        }
    }

    public void NewListThree() {
        for (int i = 10500; i < 12000; i++) {
            newListThree += newWordList[i].ToString();
            newListThree += '\n';
        }
    }

    public void NewListFour() {
        for (int i = 12000; i < 13500; i++) {
            newListFour += newWordList[i].ToString();
            newListFour += '\n';
        }
    }

    public void NewListFive() {
        for (int i = 13500; i < newWordList.Length; i++) {
            newListFive += newWordList[i].ToString();
            newListFive += '\n';
        }
    }

}
