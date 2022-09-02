using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    // ��������Z���̃v���t�@�u�ł��B
    [SerializeField]
    private GameObject CellPrefab;

    // �t�B�[���h�ɑ��݂��邷�ׂẴZ���ł��B
    public  TestCell[,] Cells { get; private set; }

    // �t�B�[���h�̏�����(���e�ʒu)���s���܂��B
    public void Initialize(int row, int col, int bombCount)
    {
        this.Cells = new TestCell[row, col];

        // �Z���𐶐����܂��B
        for (int r = 0; r < this.Cells.GetLength(0); r++)
        {
            for (int c = 0; c < this.Cells.GetLength(1); c++)
            {
                // �ЂƂ܂����e�̂Ȃ��Z���Ƃ��Đ������܂��B
                this.Cells[r, c] = GenerateCell(r, c, false);
            }
        }

        // �t�B�[���h�T�C�Y�𒲐����܂��B
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(col * TestCell.Size, row * TestCell.Size);
    }

    // �Z����Ֆʂɏ������E�������܂��B
    private TestCell GenerateCell(int r, int c, bool isBomb)
    {
        // �Q�[���I�u�W�F�N�g����ʂɔz�u���܂��B
        var go = Instantiate(CellPrefab, this.gameObject.GetComponent<RectTransform>());
        go.GetComponent<RectTransform>().anchoredPosition = new Vector2(TestCell.Size / 2 + TestCell.Size * c, TestCell.Size / 2 + TestCell.Size * r);

        // �Z���N���X�����������܂��B
        var cell = go.GetComponent<TestCell>();

        return cell;
    }
}
