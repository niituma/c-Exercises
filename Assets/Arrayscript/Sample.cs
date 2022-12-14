using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    void Start()
    {
        int i = 100;

        //+演算子による文字列結合
        Debug.Log($"i=" + i);
        //$から始まる文字列補間
        Debug.Log($"i={i}");
        //上の文字列補間の実体はstring.Format()と同義
        Debug.Log(string.Format("i={0}", i));

        int[] iAry;
        iAry = new int[3];

        iAry[0] = 10;
        iAry[1] = 20;
        iAry[2] = 30;

        for(int j = 0; j < iAry.Length; j++)
        {
            Debug.Log($"iAry[{j}] = {iAry[j]}");
        }
    }
}
