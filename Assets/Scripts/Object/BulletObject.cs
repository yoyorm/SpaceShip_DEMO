
using System;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    private BulletInfo info;

    
    public void InitInfo(BulletInfo info)
    {
        this.info = info;
       Invoke("DealyDestroy",info.lifeTime);
    }

    private void DealyDestroy()
    {
        Destroy(gameObject);
    }

    // private void Start()
    // {
    //     InitInfo(GameDataMgr.Instance.bulletData.bulletInfoList[16]);
    // }

    /// <summary>
    /// 销毁子弹
    /// </summary>
    public void Dead()
    {
        //创建死亡特效
        GameObject effobj = Instantiate(Resources.Load<GameObject>(info.deadEffRes));
        effobj.transform.position = transform.position;
        Destroy(gameObject);
        Destroy(effobj, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerObject player = other.GetComponent<PlayerObject>();
            player.Wound();
            Dead();
        }
    }
    private float time;//用于曲线运动的计时
    private void Update()
    {
        //共同朝自己面朝向移动
        this.transform.Translate(Vector3.forward * (Time.deltaTime * info.forwardSpeed));
        //处理其他移动逻辑
        //1为 直线运动
        //2为 曲线
        //3为 右抛物线   改变旋转角度
        //4为 左抛物线
        //5为 跟踪子弹
        switch (info.type)
        {
            case 2:
                time+= Time.deltaTime;
                this.transform.Translate(Vector3.right * (Time.deltaTime * Mathf.Sin(time*info.roundSpeed)*info.rightSpeed));
                break;
            case 3:
                this.transform.rotation*=Quaternion.AngleAxis(info.roundSpeed*Time.deltaTime, Vector3.up);
                break;
            case 4:
                this.transform.rotation*=Quaternion.AngleAxis(-info.roundSpeed*Time.deltaTime, Vector3.up);
                break;
            case 5:
                this.transform.rotation=Quaternion.Slerp(this.transform.rotation,
                    Quaternion.LookRotation(PlayerObject.Instance.transform.position-transform.position),
                    info.roundSpeed*Time.deltaTime);
                break;

                
        }
    }
}
