using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    void Start()
    {
        int i = 100;

        //+���Z�q�ɂ�镶���񌋍�
        Debug.Log($"i=" + i);
        //$����n�܂镶������
        Debug.Log($"i={i}");
        //��̕������Ԃ̎��̂�string.Format()�Ɠ��`
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
