using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    void Start()
    {
        int i = 100;

        //+‰‰Zq‚É‚æ‚é•¶š—ñŒ‹‡
        Debug.Log($"i=" + i);
        //$‚©‚çn‚Ü‚é•¶š—ñ•âŠÔ
        Debug.Log($"i={i}");
        //ã‚Ì•¶š—ñ•âŠÔ‚ÌÀ‘Ì‚Ístring.Format()‚Æ“¯‹`
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
