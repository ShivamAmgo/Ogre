using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bakchodi : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] kaartos kratos;
    void Start()
    {
        
    }

    public void Hit()
    {
        kratos.Playanimation();
    }
}
