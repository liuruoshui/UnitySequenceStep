using UnityEngine;

namespace PigeonKingGames.Utils.Mouse
{
    public class CustomCursor : FollowMouseOrthographic
    {
        void Start()
        {
            // 隐藏系统光标（可选）
            Cursor.visible = false;
        }
    }
}