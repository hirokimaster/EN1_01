using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GmaeManagerScript : MonoBehaviour
{

    //�z��̐錾
    int[]map;


    //�N���X�̒��A���\�b�h�̊O�ɏ������ƁI
    void PrintArray()
    {
        string debugText = "";

        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ",";
        }

        Debug.Log(debugText);
    }

    //�Ԃ�l�̌^�ɒ���
    int GetPlayerIndex()
    {
        for(int i=0; i<map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    //�Ԃ�l�̕��ɒ���
    bool MoveNumber(int number,int moveFrom,int moveTo)
    {
        if(moveTo < 0 ||  moveTo >= map.Length)
        {
            //�����Ȃ��������ɏ����A���^�[������B�������^�[��
            //�ړ��悪�͈͊O�Ȃ�ړ��s��
            return false;
        }

        //�ړ����2�i���j��������
        if (map[moveTo] == 2)
        {
            //�ǂ̕����ֈړ����邩
            int velocity = moveTo - moveFrom;
            //�v���C���[�̈ړ��悩��A����ɐ��2�i���j���ړ�
            //���̈ړ�����
            bool success = MoveNumber(2, moveTo, moveTo + velocity);
            //���������ړ����s������A�v���C���[�����s
            if (!success)
            {
                return false;
            }
        }

        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }



    // Start is called before the first frame update
    void Start()
    {
        //�z��̎��Ԃ̍쐬�Ə�����
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        PrintArray();
       

    }

    // Update is called once per frame
    void Update()
    {

        //�E�ړ�
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            int playerIndex = GetPlayerIndex();

            //�ړ������֐���
            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();   
        }

    }
}
