using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public UIButton closeBtn;
    public UIScrollView svlist;
    //用于存储排名数据的链表
    private List<RankItem> rankItems=new List<RankItem>(); 
    public override void Init()
    {
        closeBtn.onClick.Add(new EventDelegate(()=>HideMe()));
        //GameDataMgr.Instance.AddRankData("yoyorm",80);
        
        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //更新排行榜信息
        //先获取本地排行榜数据
        List<RankInfo> list= GameDataMgr.Instance.rankData.ranklist;
        for (int i = 0; i < list.Count; i++)
        {
            if (rankItems.Count > i)        //面板已有信息条目就之间更新组件
            {
                rankItems[i].InitInfo(i+1, list[i].name, list[i].time);
            }
            else                            //没有则加载实例化一个新的组件
            {
                GameObject obj=Instantiate(Resources.Load<GameObject>("UI/RankItem"));
                obj.transform.SetParent(svlist.transform,false);    
                obj.transform.localPosition=new Vector3(3,85-i*70,0);        //修改位置
                RankItem item=obj.GetComponent<RankItem>();
                item.InitInfo(i+1, list[i].name, list[i].time);             //设置脚本数据
                rankItems.Add(item);
                
            }
        }

    }
}
