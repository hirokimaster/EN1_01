using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GmaeManagerScript : MonoBehaviour
{
    // �ǉ�
    public GameObject playerPrefab;
    public GameObject boxPrefab;

    // ���x���f�U�C���p�̔z��
    int[,]map;

    // �Q�[���Ǘ��p�̔z��
    GameObject[,] field;


    //�N���X�̒��A���\�b�h�̊O�ɏ������ƁI
     /*void PrintArray()
     {
        // �g��Ȃ�

         string debugText = "";

         for (int i = 0; i < map.Length; i++)
         {
             debugText += map[i].ToString() + ",";
         }

         Debug.Log(debugText);
     }*/


     //�Ԃ�l�̌^�ɒ���
     Vector2Int GetPlayerIndex()
     {
        // map����field�ɕς��ē񎟌��z��ɑΉ�������
         for(int i = 0; i < field.GetLength(0); i++)
         {
            for(int j = 0; j < field.GetLength(1); j++)
            {
                // �܂�null�`�F�b�N����null�Ȃ�continue���g��
                // ���[�v�𑱂���Bnull�o�Ȃ�������tag�`�F�b�N
                if (field[i,j] == null)
                {
                    continue;

                }
                
                // ���ꂼ��Vector2Int�^�ŕԂ�
                else if (field[i,j].tag == "Player")
                {
                    return new Vector2Int(j, i);
                }

            }
            
         }
         // ���ꂼ��̒l�����ꂼ��-1�Ƃ���Vector2Int�^�ŕԂ�
         return new Vector2Int(-1,-1);
     }

     //�Ԃ�l�̕��ɒ���
     bool MoveNumber(string tag,Vector2Int moveFrom,Vector2Int moveTo)
     {
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0))
        {
             //�����Ȃ��������ɏ����A���^�[������B�������^�[��
             //�ړ��悪�͈͊O�Ȃ�ړ��s��
             return false;
        }

        // �ǉ�
        if(moveTo.x < 0 || moveTo.x >= field.GetLength(1))
        {
            return false;
        }

        //�ړ����2�i���j��������
        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            //�ǂ̕����ֈړ����邩
            Vector2Int velocity = moveTo - moveFrom;
            //�v���C���[�̈ړ��悩��A����ɐ��2�i���j���ړ�
            //���̈ړ�����
            bool success = MoveNumber(tag, moveTo, moveTo + velocity);
            //���������ړ����s������A�v���C���[�����s
            if (!success)
            {
                return false;
            }
        }

         field[moveFrom.y, moveFrom.x].transform.position = new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);
         field[moveTo.y,moveTo.x] = field[moveFrom.y,moveFrom.x];
         field[moveFrom.y,moveFrom.x] = null;
         return true;
     }

     

    // Start is called before the first frame update
    void Start()
    {



        //�z��̎��Ԃ̍쐬�Ə�����
        // �ǉ��ƕύX
        map = new int[,]
        {

        {0,0,0,0,1,0,0,0 },
        {0,0,2,0,0,0,0,0 },
        {0,0,0,2,0,2,0,0 },
        {0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0 },

        };

        field = new GameObject[map.GetLength(0),map.GetLength(1)];

        string debugText = "";

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                debugText += map[y, x].ToString() + ",";

                if (map[y, x] == 1)
                {
                    // GameObject instance = // ��������
                    field[y, x] = Instantiate(playerPrefab, new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);
                }
                else if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(boxPrefab, new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);

                }

            }

            debugText += "\n";
        }

        Debug.Log(debugText);

        //PrintArray();
        

    }

    // Update is called once per frame
    void Update()
    {
        // �ړ�����
        // �E
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            // �ړ������֐���
            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(1,0));
          
        }

        // ��
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(-1, 0));
        }

        // ��
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(0, -1));
        }

        // ��
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(0, 1));
        }


    }

}
