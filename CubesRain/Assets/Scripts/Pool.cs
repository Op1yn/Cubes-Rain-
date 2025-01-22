using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private int _poolCapasity = 5;
    [SerializeField] private int _poolMaximumSize = 10;

    public ObjectPool<Cube> CubesPool { get; private set; }

    private void Awake()
    {
        InstantiatePool();
    }

    private void InstantiatePool()
    {
        CubesPool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cube),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => ActionOnRelease(cube),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapasity,
            maxSize: _poolMaximumSize);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.gameObject.SetActive(true);
    }

    public void ActionOnRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);
        cube.SetHasTouchedPlatform(false);
    }
}
