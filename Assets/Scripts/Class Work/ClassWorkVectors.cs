using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ClassWorkVectors : MonoBehaviour
{
    [SerializeField] TMP_InputField vector1X;
    [SerializeField] TMP_InputField vector1Y;
    [SerializeField] TMP_InputField vector2X;
    [SerializeField] TMP_InputField vector2Y;

    [SerializeField] TMP_InputField equivalenVectorX;
    [SerializeField] TMP_InputField equivalenVectorY;

    [SerializeField] GameObject YouWon;
    [SerializeField] GameObject YouWroung;

    Vector2 vector1;
    Vector2 vector2;

    Vector2 equivalenVector;

    private void Start()
    {
        int[] nums = new int[4];
        int randNum;
        for (int i = 0; i < 4; i++)
        {
            randNum = UnityEngine.Random.Range(0, 10);
            nums[i] = randNum;
        }
        vector1X.text = "" + nums[0];
        vector1Y.text = "" + nums[1];
        vector2X.text = "" + nums[2];
        vector2Y.text = "" + nums[3];

        vector1 = new Vector2(int.Parse(vector1X.text), int.Parse(vector1Y.text));
        vector2 = new Vector2(int.Parse(vector2X.text), int.Parse(vector2Y.text));
    }


    public void TellIfValidate()
    {
        if (vector1 + vector2 == new Vector2(int.Parse(equivalenVectorX.text), int.Parse(equivalenVectorY.text)))
        {
            YouWon.SetActive(true);
            YouWroung.SetActive(false);
        }
        else
        {
            YouWon.SetActive(false);
            YouWroung.SetActive(true);
        }
    }


}
