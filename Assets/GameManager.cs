using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Play();
    }

    // �Q�[�����J�n���܂�
    private void Play()
    {
        var field = GameObject.Find("Field").GetComponent<Field>();

        // �s���A�񐔁A���e�̌����w�肵�ăt�B�[���h�����������܂��B
        field.Initialize(9, 9, 10);
    }
}
