using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    private void Awake()
    {
        string path = GameDataMgr.Instance.GetNowSelHeroInfo().resName;
        GameObject player = Instantiate(Resources.Load<GameObject>(path));
        player.AddComponent<PlayerObject>();
        player.layer = LayerMask.NameToLayer("player");
        player.tag = "Player";
    }
}
