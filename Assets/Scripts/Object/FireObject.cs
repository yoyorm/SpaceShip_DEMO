using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum E_Pos_Type      //表示开火点类型
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight,
    Right,
    Left,
    Top,
    Bottom
}
public class FireObject : MonoBehaviour
{
    public  E_Pos_Type type;
    private Vector3 screenPos;  //屏幕点
    private Vector3 initDir;    //作为散弹发射的初始方向
    private List<FireInfo> list;
    private FireInfo fireInfo;
    
    //临时存储 防止修改原数据
    private int nowNum;
    private float nowCD;
    private float nowDelay;
    private BulletInfo nowBulletInfo;
    //散射子弹间隔角度
    private float changeAngle;
    //用于记录散弹的上一次方向
    private Vector3 nowDir;
    
    void Start()
    {
        //print(Camera.main.WorldToScreenPoint(PlayerObject.Instance.transform.position));
    }
    
    private void Update()
    {
        UpdatePos();
        ResetFireInfo();
        UpdateFire();
    }

    /// <summary>
    /// 根据屏幕分辨率确定发射点位置
    /// </summary>
    private void UpdatePos()        
    {
        screenPos.z = 200;  //设置摄像机到 玩家平面 的距离
        switch (type)
        {
            case E_Pos_Type.Left:
                screenPos.x = 0;
                screenPos.y = Screen.height/2;
                initDir = Vector3.right;
                break;
            case E_Pos_Type.Top:
                screenPos.x = Screen.width/2;
                screenPos.y = Screen.height;
                initDir = Vector3.right;
                break;
            case E_Pos_Type.Right:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height/2;
                initDir = Vector3.left;
                break;
            case E_Pos_Type.Bottom:
                screenPos.x = Screen.width/2;
                screenPos.y = 0;
                initDir = Vector3.right;
                break;
            case E_Pos_Type.BottomLeft:
                screenPos.x = 0;
                screenPos.y = 0;
                initDir = Vector3.right;
                break;
            case E_Pos_Type.BottomRight:
                screenPos.x = Screen.width;
                screenPos.y = 0;
                initDir = Vector3.left;
                break;
            case E_Pos_Type.TopRight:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height;
                initDir = Vector3.left;
                break;
            case E_Pos_Type.TopLeft:
                screenPos.x = 0;
                screenPos.y = Screen.height;
                initDir = Vector3.right;
                break;
        }
        transform.position = Camera.main.ScreenToWorldPoint(screenPos);
    }
    /// <summary>
    /// 重置要发射的炮台数据
    /// </summary>
    private void ResetFireInfo()
    {
        if (nowCD != 0 && nowNum != 0)
            return;
        //组件休息
        if (fireInfo != null)
        {
            nowDelay -=Time.deltaTime;
            if (nowDelay > 0)
                return;
        }
        
        
        //随机抽取
        list = GameDataMgr.Instance.fireData.fireInfoList;
        fireInfo = list[Random.Range(0, list.Count)];
        nowNum = fireInfo.num;
        nowDelay = fireInfo.delay;
        nowCD = fireInfo.delay;
        //通过发火点取出要使用的子弹信息
        string[] strs = fireInfo.ids.Split(',');
        int beginID = int.Parse(strs[0]);
        int endID = int.Parse(strs[1]);
        int randomBulletID = Random.Range(beginID, endID+1);
        nowBulletInfo = GameDataMgr.Instance.bulletData.bulletInfoList[randomBulletID-1];

        if (fireInfo.type == 2)
        {
            switch (type)
            {
                case E_Pos_Type.BottomLeft:
                case E_Pos_Type.BottomRight:
                case E_Pos_Type.TopRight:
                case E_Pos_Type.TopLeft:
                    changeAngle = 90f/ (nowNum+1);
                    break;
                case E_Pos_Type.Top:
                case E_Pos_Type.Left:
                case E_Pos_Type.Right:
                case E_Pos_Type.Bottom:
                    changeAngle = 180f/ (nowNum+1);
                    break;
                    
            }
        }
    }
    //检测开火
    private void UpdateFire()
    {
        if(nowCD == 0 && nowNum == 0)
            return;
        //Cd计时
        nowCD -= Time.deltaTime;
        if(nowCD> 0)
            return;
        
        GameObject bullet;
        BulletObject bulletObj;
        
        
        switch (fireInfo.type)
        {
            //一个子弹
            case 1:
                
                //实例化子弹
                bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                //添加子弹脚本
                bulletObj = bullet.AddComponent<BulletObject>();
                //初始化子弹数据
                bulletObj.InitInfo(nowBulletInfo);
                //设置位置和朝向
                bullet.transform.position=transform.position;
                bullet.transform.rotation = Quaternion.LookRotation(PlayerObject.Instance.transform.position - transform.position);
                nowNum--;
                nowCD = nowNum == 0 ? 0 : fireInfo.cd;
                break;
            //发射散弹
            case 2:
                if (nowCD == 0)
                {
                    initDir = nowDir;
                    for (int i = 0; i < nowNum; i++)
                    {
                        //实例化子弹
                        bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                        //添加子弹脚本
                        bulletObj = bullet.AddComponent<BulletObject>();
                        //初始化子弹数据
                        bulletObj.InitInfo(nowBulletInfo);
                        //设置位置和朝向
                        bullet.transform.position=transform.position;
                        nowDir = Quaternion.AngleAxis(changeAngle*i,Vector3.up)*initDir;
                        bullet.transform.rotation = Quaternion.LookRotation(nowDir);
                    }
                }
                else
                {
                    //实例化子弹
                    bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                    //添加子弹脚本
                    bulletObj = bullet.AddComponent<BulletObject>();
                    //初始化子弹数据
                    bulletObj.InitInfo(nowBulletInfo);
                    //设置位置和朝向
                    bullet.transform.position=transform.position;
                    nowDir = Quaternion.AngleAxis(changeAngle*(fireInfo.num-nowNum),Vector3.up)*initDir;
                    bullet.transform.rotation = Quaternion.LookRotation(nowDir);
                    nowNum--;
                    nowCD = nowNum == 0 ? 0 : fireInfo.cd;
                }
                break;
        }
    }
}
