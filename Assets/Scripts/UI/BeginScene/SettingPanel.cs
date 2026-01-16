using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BasePanel<SettingPanel>
{
   public UIButton closeBtn;
   public UISlider sliderMusic;
   public UISlider sliderSound;
   public UIToggle toggleMusic;
   public UIToggle toggleSound;
   public override void Init()
   {
      closeBtn.onClick.Add(new EventDelegate(()=>HideMe()));   //关闭设置面板
      sliderMusic.onChange.Add(new EventDelegate(() =>
      {
         GameDataMgr.Instance.SetMusicVolume(sliderMusic.value);
      }));   //修改音量和数据
      sliderSound.onChange.Add(new EventDelegate(() =>
      {
         GameDataMgr.Instance.SetSoundVolume(sliderSound.value);
      }));   //修改音效和数据
      toggleMusic.onChange.Add(new EventDelegate(() =>
      {
         GameDataMgr.Instance.SetMusicIsOpen(toggleMusic.value);
      }));   //开关音量
      toggleSound.onChange.Add(new EventDelegate(() =>
      {
         GameDataMgr.Instance.SetSoundIsOpen(toggleSound.value);
      }));   //开关音效
      HideMe();
   }

   
   public override void ShowMe()
   {
      base.ShowMe();
      MusicData musicData = GameDataMgr.Instance.musicData;
      toggleMusic.value = musicData.musicOpen;
      toggleSound.value = musicData.soundOpen;
      sliderMusic.value = musicData.musicVolume;
      sliderSound.value = musicData.soundVolume;
   }

   public override void HideMe()
   {
      base.HideMe();
      GameDataMgr.Instance.SaveMusicData();  //保存音量数据
      
   }
}
