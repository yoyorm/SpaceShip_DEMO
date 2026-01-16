
using UnityEngine;
//面板基类

public abstract class BasePanel<T> : MonoBehaviour where T : class  //抽象类防止直接使用
{
    private static T instance;  //泛型保证后续类的都可以使用
    public static T Instance { get{return instance; } }

    protected virtual void Awake()  
    {
        instance = this as T;
    }

    void Start()
    {
        Init();     //父类强行调用初始化方法，子类也必须实现抽象的初始化函数
    }

    public abstract void Init();       //初始化

    public virtual void ShowMe()       //显示隐藏功能
    {
        this.gameObject.SetActive(true);
    }

    public virtual void HideMe()
    {
        this.gameObject.SetActive(false);
    }
}
