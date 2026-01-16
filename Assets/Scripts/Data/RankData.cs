using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

//排行榜数据

public class RankData
{
    public List<RankInfo> ranklist= new List<RankInfo>();
}

//单条数据
public class RankInfo
{
    [XmlAttribute]
    public string name;
    [XmlAttribute]
    public int time;
}
