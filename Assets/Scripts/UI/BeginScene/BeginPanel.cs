using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPanel : BasePanel<BeginPanel>
{
    public UIButton StartBtn;
    public UIButton RankBtn;
    public UIButton SettingBtn;
    public UIButton QuitBtn;
    public override void Init()
    {
        StartBtn.onClick.Add(new EventDelegate(() =>
        {
            //进入开始界面
            ChoosePanel.Instance.ShowMe();
            HideMe();
        }));
        RankBtn.onClick.Add(new EventDelegate(() =>
        {
            //显示排行榜
            RankPanel.Instance.ShowMe();
            
        }));
        SettingBtn.onClick.Add(new EventDelegate(() =>
        {
            //进入设置界面
            SettingPanel.Instance.ShowMe();
            
        }));
        QuitBtn.onClick.Add(new EventDelegate(() =>
        {
            //退出游戏
            Application.Quit();
        }));
    }
}
