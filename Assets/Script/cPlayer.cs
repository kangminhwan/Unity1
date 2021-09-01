using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPlayer : MonoBehaviour
{
    public TrailRenderer trail;
   
    public Animator Anim;


  
    // 스피드 조정 변수
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;
    private float applySpeed;

    // 점프 정도
    [SerializeField]
    private float jumpForce;

    // 상태 변수
    private bool isRun = false;
    private bool isGround = true;
    private bool isCrouch = false;

    // 앉았을 때 얼마나 앉을지 결정하는 변수
    [SerializeField]
    private float crouchPosY;
    private float originPosX;
    private float originPosY;
    private float applyCrouchPosY;

    // 민감도
    [SerializeField]
    private float lookSensitivity;

    // 카메라 한계
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX;

    // 필요한 컴포넌트
    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;
    private CapsuleCollider capsuleCollider;


    void Start()
    {
        // 컴포넌트 할당
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();

        // 초기화
        applySpeed = walkSpeed;

        originPosX = theCamera.transform.localPosition.x;
        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;
    }

    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        Move();
        CameraRotation();
        CharacterRotation();
        TryAttack();
    }
    private void Attack()
    {
        Anim.SetTrigger("Slash");
    }
    private void TryAttack()
    {
        if (Input.GetMouseButtonDown(0))
            Attack();
    }
    // 지면 체크
    private void IsGround()
    {
        if (isGround == false)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
         
        }

    }

    // 점프 시도
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    // 점프
    private void Jump()
    {
        Anim.SetTrigger("Jump");
        myRigid.velocity = transform.up * jumpForce;
    }

    // 달리기 시도
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    // 달리기
    private void Running()
    {

       
        isRun = true;
        applySpeed = runSpeed;
    }

    // 달리기 취소
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }



 

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        Anim.SetFloat("Speed", _moveDirZ* applySpeed);
        Anim.SetFloat("side", _moveDirX * 50);
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;

        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 1.0f, 0f);
    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }


    public void TrailOn()
    {
        trail.gameObject.SetActive(true);
    }

    public void TrailOff()
    { trail.gameObject.SetActive(false); }
}