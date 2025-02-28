using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

public class GUIManager : MonoSingleton<GUIManager>
{
    private ScreenBase[] _screenPrefabs;
    private Dictionary<System.Type, ScreenBase> _screens = new Dictionary<System.Type, ScreenBase>();
    private Dictionary<System.Type, ScreenBase> _cacheScreens = new Dictionary<System.Type, ScreenBase>();
    protected override void Awake()
    {
        base.Awake();
        this.ClearScreens();
    }
    private T LoadPrefabs<T>() where T : ScreenBase
    {
        if (!this._screens.ContainsKey(typeof(T)))
        {
            if (_screenPrefabs == null)
            {
                _screenPrefabs = Resources.LoadAll<ScreenBase>("GUI/Screens");
            }

            for (int i = 0; i < _screenPrefabs.Length; i++)
            {
                if (_screenPrefabs[i] is T)
                {
                    this._screens.Add(typeof(T), _screenPrefabs[i]);
                    break;
                }
            }
        }

        return this._screens[typeof(T)] as T;
    }

    private T GetPrefab<T>() where T : ScreenBase
    {
        if (this._screens.ContainsKey(typeof(T)))
        {
            return this._screens[typeof(T)] as T;
        }

        return LoadPrefabs<T>() as T;
    }

    public T CreateScreen<T>() where T : ScreenBase
    {
        ScreenBase screen = Instantiate(GetPrefab<T>(), this.transform);
        this._cacheScreens[typeof(T)] = screen;
        return screen as T;
    }

    private bool CheckScreen<T>() where T : ScreenBase
    {
        System.Type type = typeof(T);
        return this._cacheScreens.ContainsKey(type) && this._cacheScreens[type] != null;
    }

    private T GetScreen<T>() where T : ScreenBase
    {
        if (!CheckScreen<T>())
        {
            return CreateScreen<T>();
        }

        return this._cacheScreens[typeof(T)] as T;
    }

    public bool CheckScreenShowed<T>() where T : ScreenBase
    {
        if (CheckScreen<T>() && CheckScreenShowedFromCache<T>())
        {
            return true;
        }

        return false;
    }

    private bool CheckScreenShowedFromCache<T>() where T : ScreenBase
    {
        System.Type type = typeof(T);
        return this._cacheScreens[type].gameObject.activeSelf;
    }

    public T ShowScreen<T>(params object[] paras) where T : ScreenBase
    {
        ScreenBase screen = this.GetScreen<T>();
        screen.OnInit(paras);
        screen.Show();
        return screen as T;
    }

    public void HideScreen<T>() where T : ScreenBase
    {
        if (this.CheckScreenShowed<T>())
        {
            this.GetScreen<T>().Hide();
        }
        else
        {
            Debug.LogError("Error Hide Screen");
        }
    }

    private void ClearScreens()
    {
        for (var i = this.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(this.transform.GetChild(i).gameObject);
        }

        this._cacheScreens.Clear();
    }

}
