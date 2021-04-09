using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CurveFuncBase : MonoBehaviour
{
    /// <summary>
    /// 曲線の式
    /// </summary>
    /// <param name="t">0から1までの値をとる媒介変数</param>
    /// <param name="time">シーン開始からの経過時間</param>
    public abstract Vector3 CureveFunc(float t, float time);
}
