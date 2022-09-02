using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //二次元配列
        //配列は直列に同じデータを並べたもの
        //□□□□□□

        //これをさらに１軸増やしたものが２次元配列
        //□□□□□□
        //□□□□□□
        //□□□□□□

        // 要素を並べるだけという点は通常の配列と同じ
        // データアクセスに各次元のインデックスを分離できるので
        // 表のような構造のデータを表現しやすい

        // 2次元配列型の変数宣言
        // 要素型[,] 変数名;
        int[,] iAry = new int[5,3]//要素型と要素数は初期化子から推測可能
        {
            {0,1,2},
            {10,11,12},
            {20,21,22},
            {30,31,32},
            {40,41,42}
        };

        //Lengthは配列全体の要素数（5＊3 = 15）
        Debug.Log(iAry.Length);

        //配列の次元数を取得
        Debug.Log(iAry.Rank);

        // 2次元配列の生成
        // new 要素型[1次元目の要素数, 2次元目の要素数];
        //iAry = new int[5, 3];

        //二次元へのアクセスのやり方1次元と同じ
        //次元ごとにカンマで区切りで要素番号を指定する
        //iAry[0, 0] = 0;
        //iAry[0, 1] = 1;
        //iAry[0, 2] = 2;
        //iAry[1, 0] = 4;
        //iAry[1, 1] = 5;
        //iAry[1, 2] = 6;
        //iAry[2, 0] = 7;
        //iAry[2, 1] = 8;
        //iAry[2, 2] = 9;
        //iAry[3, 0] = 10;
        //iAry[3, 1] = 30;
        //iAry[3, 2] = 40;
        //iAry[4, 0] = 32;
        //iAry[4, 1] = 56;
        //iAry[4, 2] = 99;

        for (int i = 0; i < iAry.GetLength(0); ++i)
        {
            for (int j = 0; j < iAry.GetLength(1); ++j)
            {
                Debug.Log(iAry[i, j]);
            }
        }

        //foreachを使うこともできる
        foreach (var i in iAry)
        {
            Debug.Log(i);
        }
    }
}
