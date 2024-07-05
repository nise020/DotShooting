using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagorDataList : MonoBehaviour
{
    public enum LayerTags
    {
        Player,
        Mob,
        Heal,
        Bullet,
        Speed,
        SwordScaleUP,
        SwordUgaid,
        Sword,

    }

   public class Tags
    {
        public static string GetTags(LayerTags value) 
        {
            return value.ToString();
        }
    }
}
