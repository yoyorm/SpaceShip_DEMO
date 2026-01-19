using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
   private static PlayerObject _instance;
   public static PlayerObject Instance => _instance;
   public int nowHp;
   public int MaxHp;
   public float volume;
   public int speed=100;
   public int roundSpeed=5;
   public bool isDead=false;
   public Quaternion targetQ;    //目标旋转角度
   
   private Vector3 nowPos;
   [SerializeField]
   private float hValue;
   [SerializeField]
   private float vValue;

   private void Awake()
   {
      _instance = this;
   }

   private void Start()
   {
      //读取数据
      RoleInfo role = GameDataMgr.Instance.GetNowSelHeroInfo();
      MaxHp = role.hp;
      nowHp = MaxHp;
      speed = role.speed*10;
      volume = (0.4f+role.volume*0.05f);
      transform.localScale = new Vector3(volume, volume, volume);
      GamePanel.Instance.ChangeHp(nowHp);
   }
   private void Update()
   {
      if (!isDead)
      {
         //获得旋转
         hValue = Input.GetAxisRaw("Horizontal");
         vValue = Input.GetAxisRaw("Vertical");
         if(hValue==0)
            targetQ=Quaternion.identity;
         else
         {
            targetQ=hValue<0? Quaternion.AngleAxis(30, Vector3.forward): Quaternion.AngleAxis(-30, Vector3.forward);
         }
         
         //让飞机旋转
         this.transform.rotation=Quaternion.Slerp(this.transform.rotation, targetQ,Time.deltaTime*roundSpeed);
         
         //移动
         transform.Translate(Vector3.forward* (vValue*speed * Time.deltaTime),Space.World);
         transform.Translate(Vector3.right * (hValue *speed* Time.deltaTime),Space.World);
         
         //保持区域
         AeraLimit();
      }

      if (Input.GetMouseButtonDown(0))
      {
         RaycastHit hitInfo;
         if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000)) ;
         {
            BulletObject bullet = hitInfo.transform.GetComponent<BulletObject>();
            if (bullet != null)
            {
               bullet.Dead();
            }
         }
      }
   }

   public void Dead()      //角色死亡
   {
      isDead=true;
      GameOverPanel.Instance.ShowMe();
   }

   public void Wound()     //受伤扣血
   {
      if (isDead)
         return;
      nowHp--;
      GamePanel.Instance.ChangeHp(nowHp);
      if (nowHp <= 0)
      {
         Dead();
      }
   }

   private void AeraLimit()
   {
      Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
      pos.x=Mathf.Clamp(pos.x, 0, Screen.width);
      pos.y=Mathf.Clamp(pos.y, 0, Screen.height);
      transform.position = Camera.main.ScreenToWorldPoint(pos);
   }
}
