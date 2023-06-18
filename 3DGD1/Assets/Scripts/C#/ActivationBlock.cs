using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationBlock : MonoBehaviour
{
    public GameObject tileToActivate;
    public AudioSource SFX;
    public AudioClip platformSound;
    public GameObject platformEffect;

    public void Activate()
    {
        SFX.PlayOneShot(platformSound);
        Instantiate(platformEffect, tileToActivate.transform.position, Quaternion.identity);
        tileToActivate.SetActive(true);
    }

    public void Deactivate()
    {
        tileToActivate.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CubeRoll>() || collision.gameObject.GetComponent<PushBlock>())
        {
            Activate();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<CubeRoll>() || collision.gameObject.GetComponent<PushBlock>())
        {
            Deactivate();
        }
    }
}
