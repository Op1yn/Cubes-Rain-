using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Pool _pool;

    [SerializeField] private float _spawnRate = 3.0f;
    private Coroutine _coroutine;

    private void Start()
    {
        _coroutine = StartCoroutine(SpawnCube());
    }

    public IEnumerator SpawnCube()//������ ������ ��������� ���� �� �����������. ��� �� ����������� � ����������� _poolCapasity � _poolMaximumSize, � ����������� �� ��������� �������������� ����. ����� ��� ������ � �� ���� 5 �������
    {
        while (enabled)
        {
            //Debug.Log("while � �������� ��������");

            Cube cube = _pool.ObjectsPool.Get();
            cube.transform.position = GetPosition();
            cube.TimerEnded += ReturnCube;

            yield return new WaitForSeconds(_spawnRate);
        }
    }

    private void ReturnCube(Cube cube)
    {
        Debug.Log("������� ���� � ��������");

        cube.TimerEnded -= ReturnCube;

        _pool.ObjectsPool.Release(cube);
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