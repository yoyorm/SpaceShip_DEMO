using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePanel : BasePanel<ChoosePanel>
{
    public UIButton closeBtn;
    public UIButton leftBtn;
    public UIButton rightBtn;
    public UIButton startBtn;
    public Transform heroPos;
    public List<GameObject> hpObjs;
    public List<GameObject> speedObjs;
    public List<GameObject> volumeObjs;
    
    private GameObject airPlaneObj;
    
    public override void Init()
    {
        closeBtn.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
            BeginPanel.Instance.ShowMe();
        }));
        startBtn.onClick.Add(new EventDelegate(() =>
        {
            //进入游戏
            SceneManager.LoadScene("GameScene");
        }));
        leftBtn.onClick.Add(new EventDelegate(() =>
            {
                GameDataMgr.Instance.nowSelHeroIndex--;
                if (GameDataMgr.Instance.nowSelHeroIndex < 0)
                {
                    GameDataMgr.Instance.nowSelHeroIndex = GameDataMgr.Instance.roleData.roleList.Count - 1;
                }
                ChangeNowHero();
            }
        ));
        rightBtn.onClick.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.nowSelHeroIndex++;
            if (GameDataMgr.Instance.nowSelHeroIndex >GameDataMgr.Instance.roleData.roleList.Count - 1)
            {
                GameDataMgr.Instance.nowSelHeroIndex = 0;
            }
            ChangeNowHero();
        }));
        HideMe();
    }
    /// <summary>
    /// 切换当前选择的飞船
    /// </summary>
    private void ChangeNowHero()
    {
        //获得数据
        RoleInfo info =GameDataMgr.Instance.GetNowSelHeroInfo();
        //更新模型
        DestroyObj();
        airPlaneObj = Instantiate(Resources.Load<GameObject>(info.resName));
        airPlaneObj.transform.SetParent(heroPos.transform,false);
        airPlaneObj.transform.localPosition = Vector3.zero;
        airPlaneObj.transform.localScale =Vector3.one *info.scale;
        airPlaneObj.transform.localRotation = Quaternion.identity;
        airPlaneObj.layer = LayerMask.NameToLayer("UI");
        //更新模型
        for (int i = 0; i < 10; i++)
        {
            hpObjs[i].SetActive(i<info.hp);
            speedObjs[i].SetActive(i<info.speed);
            volumeObjs[i].SetActive(i<info.volume);
        }
    }

    private void DestroyObj()
    {
        if(airPlaneObj!=null)
            Destroy(airPlaneObj);
        airPlaneObj = null;
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //每次打开都从第一个模型开始
        GameDataMgr.Instance.nowSelHeroIndex = 0;
        ChangeNowHero();
    }

    public override void HideMe()
    {
        base.HideMe();
        //隐藏的时候删除
        Destroy(airPlaneObj);
    }
    
    private bool isSelect = false;//鼠标是否选中
    private void Update()
    {
        //用sin实现上下浮动
        heroPos.Translate(Vector3.up* (Mathf.Sin(Time.time) * 0.0002f), Space.World);
        //射线检测让飞机可以转动
        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), 1000,1<<LayerMask.NameToLayer("UI")))
            {
                isSelect = true;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            isSelect = false;
        }
        //如果选择并且拖动鼠标
        if (isSelect && Input.GetMouseButton(0))
        {
            heroPos.rotation *=Quaternion.AngleAxis(-Input.GetAxis("Mouse X")*15, Vector3.up);
        }
    }
}
