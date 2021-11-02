using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

public class SonsPool : MonoBehaviour
{
    public static SonsPool Instance;

    [SerializeField] Mixers mixers;
    [HideInInspector] public GameObject prefab;
    public Queue<GameObject> pool = new Queue<GameObject>();

    static GameObject _tmp;

    private void Awake()
    {
        Instance = this;
        if (!mixers.MixerActualitzat) mixers.Actualitzar();
        Instanciar();
    }

    [ContextMenu("Instanciar")]
    void Instanciar()
    {
        pool = new Queue<GameObject>();
        for (int p = 0; p < 2; p++)
        {
            Debug.Log($"Instanciar {p}");
            pool.Enqueue(AfegirObjecte(prefab));
        }
    }

    GameObject AfegirObjecte(GameObject _prefab)
    {
        GameObject _tmp = Instantiate(_prefab, this.transform);
        _tmp.SetActive(false);

        return _tmp;
    }

    public static GameObject Get(float temps = 0)
    {
        _tmp = null;

        if (SonsPool.Instance.pool.Peek().activeSelf)
        {
            _tmp = SonsPool.Instance.AfegirObjecte(SonsPool.Instance.prefab);
        }
        else
        {
            _tmp = SonsPool.Instance.pool.Dequeue();
        }
        _tmp.SetActive(true);
        SonsPool.Instance.pool.Enqueue(_tmp);
        if(temps > 0)
        {
            _tmp.SetActive(false, temps);
        }
        return _tmp;
    }
}
