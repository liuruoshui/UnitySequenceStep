using UnityEngine;

namespace PigeonKingGames.Utils.Mouse
{
    public class CustomCursor : FollowMouseOrthographic
    {
        void Start()
        {
            // ����ϵͳ��꣨��ѡ��
            Cursor.visible = false;
        }
    }
}