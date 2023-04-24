using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GmaeManagerScript : MonoBehaviour
{

    //配列の宣言
    int[]map;


    //クラスの中、メソッドの外に書くこと！
    void PrintArray()
    {
        string debugText = "";

        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ",";
        }

        Debug.Log(debugText);
    }

    //返り値の型に注意
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

    //返り値の方に注意
    bool MoveNumber(int number,int moveFrom,int moveTo)
    {
        if(moveTo < 0 ||  moveTo >= map.Length)
        {
            //動けない条件を先に書き、リターンする。早期リターン
            //移動先が範囲外なら移動不可
            return false;
        }

        //移動先に2（箱）がいたら
        if (map[moveTo] == 2)
        {
            //どの方向へ移動するか
            int velocity = moveTo - moveFrom;
            //プレイヤーの移動先から、さらに先に2（箱）を移動
            //箱の移動処理
            bool success = MoveNumber(2, moveTo, moveTo + velocity);
            //もし箱が移動失敗したら、プレイヤーも失敗
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
        //配列の実態の作成と初期化
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        PrintArray();
       

    }

    // Update is called once per frame
    void Update()
    {

        //右移動
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            int playerIndex = GetPlayerIndex();

            //移動処理関数化
            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();   
        }

    }
}
