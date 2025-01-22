using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Pool _pool;
    [SerializeField] private float _spawnRate = 0.3f;

    private Coroutine _startTimerCoroutine;

    private void Start()
    {
        _startTimerCoroutine = StartCoroutine(SpawnCube());
    }

    private void OnDisable()
    {
        StopCoroutine(_startTimerCoroutine);
    }

    public IEnumerator SpawnCube()
    {
        while (enabled)
        {
            Cube cube = _pool.CubesPool.Get();
            cube.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            cube.transform.position = GetPosition();
            cube.TimerEnded += ReturnCube;

            yield return new WaitForSeconds(_spawnRate);
        }
    }

    private void ReturnCube(Cube cube)
    {
        cube.TimerEnded -= ReturnCube;
        _pool.CubesPool.Release(cube);
    }

    private Vector3 GetPosition()
    {
        float minimumX = 5f;
        float maximumX = 15f;
        float minimumZ = 14f;
        float maximumZ = 21f;
        int positionY = 10;

        Vector3 startingPosition = new Vector3(Random.Range(minimumX, maximumX), positionY, Random.Range(minimumZ, maximumZ));

        return startingPosition;
    }
}