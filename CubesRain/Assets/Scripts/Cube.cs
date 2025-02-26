using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
    private Coroutine _shutdownTimerCoroutine;
    private Rigidbody _rigidbody;

    public event Action<Cube> TimerEnded;
    public bool HasTouchedPlatform { get; private set; } = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        ResetRotation();
        ResetPhysics();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform _) && HasTouchedPlatform == false)
        {
            HasTouchedPlatform = true;
            SetColor(Color.red);
            _shutdownTimerCoroutine = StartCoroutine(SwitchOff());
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_shutdownTimerCoroutine);
    }

    public IEnumerator SwitchOff()
    {
        yield return new WaitForSeconds(GetTime());
        TimerEnded?.Invoke(this);
    }

    public void SetHasTouchedPlatform(bool hasTouchedPlatform)
    {
        HasTouchedPlatform = hasTouchedPlatform;
    }

    private int GetTime()
    {
        int minimumTime = 2;
        int maximumTime = 5;

        return UnityEngine.Random.Range(minimumTime, maximumTime);
    }

    private void ResetPhysics()
    {
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
    }

    private void ResetRotation()
    {
        transform.rotation = Quaternion.identity;
    }

    public void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
}
