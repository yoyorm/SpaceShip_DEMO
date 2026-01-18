using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
/// <summary>
/// 子弹数据集合
/// </summary>
public class BulletData
{
    public List<BulletInfo> bulletInfoList;
}

public class BulletInfo
{
    [XmlAttribute]
    public int type;    //子弹移动规则
    [XmlAttribute]
    public float forwardSpeed;
    [XmlAttribute]
    public float rightSpeed;
    [XmlAttribute]
    public float roundSpeed;
    [XmlAttribute]
    public string resName;  //子弹特效资源路径
    [XmlAttribute]
    public string deadEffRes;   //子弹销毁特效路径
    [XmlAttribute]
    public float lifeTime;      //子弹生命周期
}
