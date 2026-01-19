using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// 开火点数据集合
/// </summary>
public class FireData 
{
    public List<FireInfo> fireInfoList;
    
}
/// <summary>
/// 单个开火点数据
/// </summary>
public class FireInfo
{
    [XmlAttribute]
    public int id;
    [XmlAttribute]
    public int type;    //开火点类型
    [XmlAttribute]
    public int num;     //子弹数量
    [XmlAttribute]
    public float cd;    //每颗子弹间隔事件
    [XmlAttribute]
    public string ids;  //子弹id 1-10随机
    [XmlAttribute]
    public float delay;
}
