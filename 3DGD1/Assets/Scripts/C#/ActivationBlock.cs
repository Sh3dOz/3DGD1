using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationBlock : MonoBehaviour
{
    public GameObject tileToActivate;

    public void Activate()
    {
        tileToActivate.SetActive(true);
    }

    public void Deactivate()
    {
        tileToActivate.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CubeRoll>())
        {
            Activate();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<CubeRoll>())
        {
            Deactivate();
        }
    }
}
