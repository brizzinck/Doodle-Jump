using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    public void DoEneble(bool eneble)
    {
        transform.gameObject.SetActive(eneble);
    }
}
