using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboMaker : MonoBehaviour
{
    [SerializeField] Animator PlayerAnim;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerAnim.SetTrigger("FrontSlash");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            PlayerAnim.SetTrigger("FrontAxeCombo");
        }
    }
}
