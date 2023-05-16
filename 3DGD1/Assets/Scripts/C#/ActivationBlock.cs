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
}
