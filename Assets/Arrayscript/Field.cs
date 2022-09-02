using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    // 生成するセルのプレファブです。
    [SerializeField]
    private GameObject CellPrefab;

    // フィールドに存在するすべてのセルです。
    public  TestCell[,] Cells { get; private set; }

    // フィールドの初期化(爆弾位置)を行います。
    public void Initialize(int row, int col, int bombCount)
    {
        this.Cells = new TestCell[row, col];

        // セルを生成します。
        for (int r = 0; r < this.Cells.GetLength(0); r++)
        {
            for (int c = 0; c < this.Cells.GetLength(1); c++)
            {
                // ひとまず爆弾のないセルとして生成します。
                this.Cells[r, c] = GenerateCell(r, c, false);
            }
        }

        // フィールドサイズを調整します。
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(col * TestCell.Size, row * TestCell.Size);
    }

    // セルを盤面に初期化・生成します。
    private TestCell GenerateCell(int r, int c, bool isBomb)
    {
        // ゲームオブジェクトを画面に配置します。
        var go = Instantiate(CellPrefab, this.gameObject.GetComponent<RectTransform>());
        go.GetComponent<RectTransform>().anchoredPosition = new Vector2(TestCell.Size / 2 + TestCell.Size * c, TestCell.Size / 2 + TestCell.Size * r);

        // セルクラスを初期化します。
        var cell = go.GetComponent<TestCell>();

        return cell;
    }
}
