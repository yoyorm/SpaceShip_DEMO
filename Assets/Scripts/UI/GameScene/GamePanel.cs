using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePanel : BasePanel<GamePanel>
{
    public UIButton closeBtn;
    public UILabel timeCount;
    public List<GameObject> hpList;
    
    public float nowTime=0;
    
    public override void Init()
    {
        //退出按钮
        closeBtn.onClick.Add(new EventDelegate(() =>
        {
            QuitPanel.Instance.ShowMe();
            
        }));
        
       
    }

    private void Update()   //更新游戏时间
    {
        nowTime += Time.deltaTime;
         string str = "";
        if ((int)nowTime / 3600 > 0)
        {
            str += (int)nowTime / 3600 + "h";
        }

        if ((int)nowTime % 3600 / 60 > 0 || str != "")
        {
            str += (int)nowTime % 3600 / 60+"m";
        }
        str+= (int)(nowTime % 60) +"s";
        timeCount.text =str;
    }
    
    public string GetTime()
    {
        return timeCount.text;
    }
    /// <summary>
    /// 用于玩家修改当前血量UI
    /// </summary>
    /// <param name="hp">玩家血量</param>
    public void ChangeHp(int hp)
    {
        for (int i = 0; i < hpList.Count; i++)
        {
            hpList[i].SetActive(i<hp);
        }
    }
}
