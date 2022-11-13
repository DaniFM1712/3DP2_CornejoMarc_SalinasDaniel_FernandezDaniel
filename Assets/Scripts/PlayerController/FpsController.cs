using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Mouse Debug")]

    [SerializeField]
    public KeyCode angleLockKey = KeyCode.I; //para bloquear el angulo de rotación (literalmente no rota)

    [SerializeField]
    public KeyCode mouseLockKey = KeyCode.O; //para esconder el ratón(solo se esconde si le das click a la pantalla) y lo pone en el centro de la pantalla

    private bool angleLocked = false;

    float yaw = 0;
    float pitch = 0;
    Vector3 direction = new Vector3(0, 0, 0);

    [Header("Rotation")]
    [SerializeField] float yawSpeed = 5.0f;
    [SerializeField] float pitchSpeed = 5.0f;
    [SerializeField] bool invertPitch;
    [SerializeField] bool invertYaw;
    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;
    [SerializeField] Transform pitchController;

    [Header("Planar Movement")]
    [SerializeField] CharacterController characterController;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] KeyCode runKey = KeyCode.LeftShift;
    [SerializeField] KeyCode forwardKey = KeyCode.W;
    [SerializeField] KeyCode leftKey = KeyCode.A;
    [SerializeField] KeyCode backwardsKey = KeyCode.S;
    [SerializeField] KeyCode rightKey = KeyCode.D;
    float currSpeed;

    [Header("Vertical Movement")]
    float verticalSpeed = 0;
    float gravity;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    //[SerializeField] float jumpSpeed = 20.0f;
    float jumpSpeed;
    [SerializeField] float maxHeightJump;
    [SerializeField] float jumpTime;
    [SerializeField]bool onGround;
    bool onCeiling;

    private void Awake()
    {
        RecalculateOrientation();
        gravity = (-2 * maxHeightJump) / (jumpTime * jumpTime);
        jumpSpeed = -gravity * jumpTime;
        Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (!angleLocked)
            rotate();
        move();
    }
    // Update is called once per frame
    void Update()
    {
        inputUpdate();
        updateLockKeyState();
    }

    void updateLockKeyState()
    {
        if (Input.GetKeyDown(angleLockKey))
            angleLocked = !angleLocked;

        if (Input.GetKeyDown(mouseLockKey))
            Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void rotate()
    {
        float xMouse = Input.GetAxis("Mouse X");
        yaw += xMouse * yawSpeed * (invertYaw ? -1 : 1);

        float yMouse = Input.GetAxis("Mouse Y");
        pitch -= yMouse * pitchSpeed * (invertPitch ? -1 : 1); //si invert pitch activated, -1 else 1

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch); //cualquier valor te lo filtra entre dos valores
        

        transform.rotation = Quaternion.Euler(0,yaw,0); //esto se hace para que el personaje cuando mire arriba y abajo no se incline
                                                        //y solo sea la cámara
        pitchController.localRotation = Quaternion.Euler(pitch,0,0); //se hace la local rotarion, porque pilla la del padre
    }

    void move()
    {
        verticalSpeed += gravity * Time.deltaTime; //se calcula la gravedad

        //aqui normalizamos para ver la dirección en la que nos queremos mover y luego la multiplicamos por la velocidad para saber cuanto nos tenemos que mover exactamente
        //de otra forma, nos moveríamos más de lo necesario.
        Vector3 movement = direction.normalized * Time.deltaTime * currSpeed;
        
        movement.y = verticalSpeed * Time.deltaTime;

        CollisionFlags colFlags = characterController.Move(movement); //esto devuelve unas collisionFlags
        onGround = (colFlags & CollisionFlags.Below) != 0; // si el CollisionFlags.Below(una especie de bit que se encuentra justo debajo del personaje) es 1, el onGround se pondrá a true
        onCeiling = (colFlags & CollisionFlags.Above) != 0; //lo mismo que lo de antes pero en el de arriba

        if (onGround || onCeiling) verticalSpeed = gravity * Time.deltaTime;

    }

    void inputUpdate()
    {
        //imaginemos un triangulo desde arriba. Tenemos un ángulo que el unity conoce.
        //para calcular la z tenemos que usar el cos y pasar los grados a radianes
        //gracias a esto podremos hacer que el personaje siempre se mueva hacia delante (hacia el sentido del ratón)
        Vector3 forward = getForward();
        Vector3 right = getRight();
        direction = new Vector3(0, 0, 0);
        if (Input.GetKey(forwardKey))
        {
            direction += forward;
        }
        if (Input.GetKey(leftKey))
        {
            direction -= right;
        }
        if (Input.GetKey(rightKey))
        {
            direction += right;
        }
        if (Input.GetKey(backwardsKey))
        {
            direction -= forward;
        }

        if (Input.GetKey(runKey))
        {
            currSpeed = runSpeed;
        }
        else
        {
            currSpeed = walkSpeed;
        }

        if (Input.GetKeyDown(jumpKey) && onGround)
        {

            //f'(t) = gt + v(initial)           |
            //f'(t) = 0                         |
            //0 = gt + v(initial)               |   velocity
            //v = -gt                           |

            //f(t) = gt*t /2 + vt + p(initial)  |
            //f(t) = gt*t/2 + (-g*t)t + 0       |
            //f(t) = -gt*t/2                    |   gravity
            //f(t) = -2h / t*t                  |

            verticalSpeed = jumpSpeed; //hacemos que salte y cambie la verticalSpeed del personaje

        }
    }

    public void RecalculateOrientation()
    {
        yaw = transform.rotation.eulerAngles.y;
        pitch = transform.rotation.eulerAngles.x;
    }

    Vector3 getForward()
    {
        return new Vector3(Mathf.Sin(yaw * Mathf.Deg2Rad), 0.0f, Mathf.Cos(yaw * Mathf.Deg2Rad));
    }

    Vector3 getRight()
    {
        return new Vector3(Mathf.Sin((yaw + 90.0f) * Mathf.Deg2Rad), 0.0f, Mathf.Cos((yaw + 90.0f) * Mathf.Deg2Rad));
    }
}
