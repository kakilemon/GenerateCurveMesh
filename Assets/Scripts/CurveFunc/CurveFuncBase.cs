using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CurveFuncBase : MonoBehaviour
{
    /// <summary>
    /// �Ȑ��̎�
    /// </summary>
    /// <param name="t">0����1�܂ł̒l���Ƃ�}��ϐ�</param>
    /// <param name="time">�V�[���J�n����̌o�ߎ���</param>
    public abstract Vector3 CureveFunc(float t, float time);
}
