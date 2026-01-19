using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance{get{return instance;}}
    private AudioSource audioSource;
    void Awake()
    {
        instance = this;                        //切换场景可以重新赋值，实现bgm切换
        audioSource = GetComponent<AudioSource>();
        
        //初始化音乐组件
        SetBKMusicIsOpen(GameDataMgr.Instance.musicData.musicOpen);
        SetBKMusicVolume(GameDataMgr.Instance.musicData.musicVolume);
        
        DontDestroyOnLoad(gameObject);
    }
    
    public void SetBKMusicIsOpen(bool isOpen)
    {
        audioSource.mute =!isOpen;
    }
    public void SetBKMusicVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
