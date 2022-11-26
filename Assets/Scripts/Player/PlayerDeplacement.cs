using UnityEngine;

public class PlayerDeplacement : MonoBehaviour
{
    public CharacterController characterController;

    [SerializeField] private float Speed = 10f;
    [SerializeField] private float turnSmooth = 1f;
    float turnSmoothVelocity;

    private Vector3 Direction;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) *  Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmooth, turnSmoothVelocity);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            characterController.Move(direction * Speed * Time.deltaTime);
        }
    }
}
