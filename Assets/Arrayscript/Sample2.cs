using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //�񎟌��z��
        //�z��͒���ɓ����f�[�^����ׂ�����
        //������������

        //���������ɂP�����₵�����̂��Q�����z��
        //������������
        //������������
        //������������

        // �v�f����ׂ邾���Ƃ����_�͒ʏ�̔z��Ɠ���
        // �f�[�^�A�N�Z�X�Ɋe�����̃C���f�b�N�X�𕪗��ł���̂�
        // �\�̂悤�ȍ\���̃f�[�^��\�����₷��

        // 2�����z��^�̕ϐ��錾
        // �v�f�^[,] �ϐ���;
        int[,] iAry = new int[5,3]//�v�f�^�Ɨv�f���͏������q���琄���\
        {
            {0,1,2},
            {10,11,12},
            {20,21,22},
            {30,31,32},
            {40,41,42}
        };

        //Length�͔z��S�̗̂v�f���i5��3 = 15�j
        Debug.Log(iAry.Length);

        //�z��̎��������擾
        Debug.Log(iAry.Rank);

        // 2�����z��̐���
        // new �v�f�^[1�����ڂ̗v�f��, 2�����ڂ̗v�f��];
        //iAry = new int[5, 3];

        //�񎟌��ւ̃A�N�Z�X�̂���1�����Ɠ���
        //�������ƂɃJ���}�ŋ�؂�ŗv�f�ԍ����w�肷��
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

        //foreach���g�����Ƃ��ł���
        foreach (var i in iAry)
        {
            Debug.Log(i);
        }
    }
}
