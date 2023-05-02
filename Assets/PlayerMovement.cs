using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float MovementSpeed = 5;
    [SerializeField] float RotateSpeed = 100;
    float XInput = 0;
    float YInput = 0;
    float Mouse_x = 0;
    float Mouse_Y = 0;
    public delegate void DeliverPlayer(PlayerMovement Player);
    public static event DeliverPlayer OnDeliverPlayerInfo;
    Animator PlayerAnim;
    void Start()
    {
        OnDeliverPlayerInfo?.Invoke(this);
        PlayerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        XInput = Input.GetAxis("Horizontal");
        YInput = Input.GetAxis("Vertical");
        Mouse_x = Input.GetAxis("Mouse X");
        PlayerAnim.SetFloat("Xmove", XInput);
        PlayerAnim.SetFloat("Ymove", YInput);
        //Mouse_Y= Input.GetAxis("Mouse Y");

    }
    
    private void FixedUpdate()
    {
        transform.position += (transform.right * XInput + transform.forward * YInput) * Time.deltaTime * MovementSpeed;
        transform.eulerAngles += Vector3.up * Mouse_x * Time.deltaTime * RotateSpeed;
    }
}
