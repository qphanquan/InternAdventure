using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBase : MonoBehaviour
{
    public virtual void OnInit(params object[] args)
    {

    }
    public virtual void Show()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
