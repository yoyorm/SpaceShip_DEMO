using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();

    public static GameDataMgr Instance
    {
        get { return instance; }
    }

    public MusicData musicData;
    public RankData rankData;
    public RoleData roleData;
    public int nowSelHeroIndex = 0;     //当前选择的角色编号
    //子弹数据
    public BulletData bulletData;
    public FireData fireData;

    private GameDataMgr()   //加载以初始化数据
    {
        
        musicData = XmlDataMgr.Instance.LoadData(typeof(MusicData), "MusicData") as MusicData;
        rankData = XmlDataMgr.Instance.LoadData(typeof(RankData), "RankData") as RankData;
        bulletData = XmlDataMgr.Instance.LoadData(typeof(BulletData), "BulletData") as BulletData;
        roleData = XmlDataMgr.Instance.LoadData(typeof(RoleData), "RoleData") as RoleData;
        fireData = XmlDataMgr.Instance.LoadData(typeof(FireData), "FireData") as FireData;
        
        
    }

    #region 音乐数据相关方法

    public void SaveMusicData()
    {
        XmlDataMgr.Instance.SaveData(musicData, "MusicData");
    }

    public void SetMusicIsOpen(bool isOpen)
    {
        musicData.musicOpen = isOpen;
        BKMusic.Instance.SetBKMusicIsOpen(isOpen);

    }

    public void SetMusicVolume(float volume)
    {
        musicData.musicVolume = volume;
        BKMusic.Instance.SetBKMusicVolume(volume);
    }

    public void SetSoundIsOpen(bool isOpen)
    {
        musicData.soundOpen = isOpen;
        //需补充修改音乐代码
    }

    public void SetSoundVolume(float volume)
    {
        musicData.soundVolume = volume;
        //需补充修改音乐代码
    }

    #endregion

    #region 排行榜数据方法
    /// <summary>
    /// 添加一条排行榜数据
    /// </summary>
    /// <param name="name">玩家名称</param>
    /// <param name="time">通关时间</param>
    public void AddRankData(string name, int time)
    {
        RankInfo rankInfo = new RankInfo();
        rankInfo.name = name;
        rankInfo.time = time;
        rankData.ranklist.Add(rankInfo);
        //排序
        rankData.ranklist.Sort((a, b) =>
        {
            if (a.time > b.time)
                return -1;
            else
                return 1;

        });
        //移除大于二十条的数据
        if (rankData.ranklist.Count > 20)
        {
            rankData.ranklist.RemoveAt(20);
        }
        XmlDataMgr.Instance.SaveData(rankData, "RankData");
    }
    
    #endregion

    #region 玩家数据方法
    /// <summary>
    /// 提供当前选择英雄的数据
    /// </summary>
    /// <returns></returns>
    public RoleInfo GetNowSelHeroInfo()
    {
        return roleData.roleList[nowSelHeroIndex];
    }
    

    #endregion
    
   
}
