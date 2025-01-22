using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private int _poolCapasity = 5;
    [SerializeField] private int _poolMaximumSize = 5;

    public ObjectPool<Cube> ObjectsPool { get; private set; }

    private void Awake()
    {
        InstantiatePool();
    }

    private void InstantiatePool()
    {
        ObjectsPool = new ObjectPool<Cube>(
            createFunc: () => InstantiateCube(_cube),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => ActionOnRelease(cube),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapasity,
            maxSize: _poolMaximumSize);
    }

    public Cube InstantiateCube(Cube cube)
    {
        Debug.Log("Создание куба в пуле");

       return Instantiate(cube);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.rotation = Quaternion.identity;
        cube.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.gameObject.SetActive(true);
    }

    public void ActionOnRelease(Cube cube)
    {
        Debug.Log("возврат куба в пуле");

        //cube.TimerEnded -= ActionOnRelease;
        cube.gameObject.SetActive(false);
        
    }    
}
