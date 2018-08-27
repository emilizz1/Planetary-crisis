using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    [Tooltip("s")] [SerializeField] float timeToDestruction = 5f;

	void Start ()
    {
        Destroy(gameObject, timeToDestruction);
	}
}
