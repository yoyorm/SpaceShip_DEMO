using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankItem :MonoBehaviour
{
    public UILabel labrank;
    public UILabel labname;
    public UILabel labtime;

    /// <summary>
    /// 根据单条排行榜数据，对组件进行初始化赋值
    /// </summary>
    /// <param name="rank">排名</param>
    /// <param name="name">名字</param>
    /// <param name="time">时间</param>
    public void InitInfo(int rank, string name, int time)
    {
        labrank.text = rank.ToString();
        labname.text = name;
        string str = "";
        if (time / 3600 > 0)
        {
            str += time / 3600 + "h";
        }

        if (time % 3600 / 60 > 0 || str != "")
        {
            str += time % 3600 / 60+"m";
        }
        str+=time %60 +"s";
        labtime.text = str;
        
    }
    
}
