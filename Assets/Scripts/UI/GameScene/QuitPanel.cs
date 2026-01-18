using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : BasePanel<QuitPanel>
{
    public UIButton quitBtn;
    public UIButton closeBtn;
    public override void Init()
    {
        quitBtn.onClick.Add(new EventDelegate(() =>
            {
                HideMe();
                SceneManager.LoadScene("BeginScene");
            }
        ));
        closeBtn.onClick.Add(new EventDelegate(() =>
            {
                HideMe();
            }
        ));
        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        Time.timeScale = 0;
    }

    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }
}
