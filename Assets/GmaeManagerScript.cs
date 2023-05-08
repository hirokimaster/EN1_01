using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GmaeManagerScript : MonoBehaviour
{
    // 追加
    public GameObject playerPrefab;
    public GameObject boxPrefab;

    // レベルデザイン用の配列
    int[,]map;

    // ゲーム管理用の配列
    GameObject[,] field;


    //クラスの中、メソッドの外に書くこと！
     /*void PrintArray()
     {
        // 使わない

         string debugText = "";

         for (int i = 0; i < map.Length; i++)
         {
             debugText += map[i].ToString() + ",";
         }

         Debug.Log(debugText);
     }*/


     //返り値の型に注意
     Vector2Int GetPlayerIndex()
     {
        // mapからfieldに変えて二次元配列に対応させた
         for(int i = 0; i < field.GetLength(0); i++)
         {
            for(int j = 0; j < field.GetLength(1); j++)
            {
                // まずnullチェックしてnullならcontinueを使い
                // ループを続ける。null出なかったらtagチェック
                if (field[i,j] == null)
                {
                    continue;

                }
                
                // それぞれVector2Int型で返す
                else if (field[i,j].tag == "Player")
                {
                    return new Vector2Int(j, i);
                }

            }
            
         }
         // それぞれの値をそれぞれ-1としたVector2Int型で返す
         return new Vector2Int(-1,-1);
     }

     //返り値の方に注意
     bool MoveNumber(string tag,Vector2Int moveFrom,Vector2Int moveTo)
     {
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0))
        {
             //動けない条件を先に書き、リターンする。早期リターン
             //移動先が範囲外なら移動不可
             return false;
        }

        // 追加
        if(moveTo.x < 0 || moveTo.x >= field.GetLength(1))
        {
            return false;
        }

        //移動先に2（箱）がいたら
        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            //どの方向へ移動するか
            Vector2Int velocity = moveTo - moveFrom;
            //プレイヤーの移動先から、さらに先に2（箱）を移動
            //箱の移動処理
            bool success = MoveNumber(tag, moveTo, moveTo + velocity);
            //もし箱が移動失敗したら、プレイヤーも失敗
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



        //配列の実態の作成と初期化
        // 追加と変更
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
                    // GameObject instance = // 書き換え
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
        // 移動処理
        // 右
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            // 移動処理関数化
            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(1,0));
          
        }

        // 左
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(-1, 0));
        }

        // 上
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(0, -1));
        }

        // 下
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(0, 1));
        }


    }

}
