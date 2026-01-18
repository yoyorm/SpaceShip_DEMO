using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : BasePanel<GameOverPanel>
{
    public UIButton okBtn;
    public UILabel timeLabel;
    public UILabel inputLabel;

    private int endTime;
    public override void Init()
    {
        
        okBtn.onClick.Add(new EventDelegate(() =>
        {
            //记录当前玩家成绩到排行榜
            HideMe();
            GameDataMgr.Instance.AddRankData(inputLabel.text,endTime);
            SceneManager.LoadScene("BeginScene");
        }));
        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        Time.timeScale = 0;
        endTime =(int)GamePanel.Instance.nowTime;
        timeLabel.text = GamePanel.Instance.GetTime();
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
