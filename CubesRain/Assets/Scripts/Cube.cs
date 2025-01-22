using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{

    public event Action<Cube> TimerEnded;

    public bool HasTouchedPlatform { get; private set; } = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform _) && HasTouchedPlatform == false)
        {
            //Debug.Log("Касание");

            HasTouchedPlatform = true;
            StartCoroutine(SwitchOff());
        }
    }

    public IEnumerator SwitchOff()
    {
        //Debug.Log("Корутина куба запущена");

        yield return new WaitForSeconds(5);
        TimerEnded?.Invoke(this);
    }
}
