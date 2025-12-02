using UnityEngine;
using UnityEngine.SceneManagement;



public class SharkMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    private Rigidbody2D rd;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    public static float speedMultiplier = 1f;
    
    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        if (_targetDirection == Vector2.zero)
        {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection) * Quaternion.Euler(0,0,-90);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        rd.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
        {
            rd.linearVelocity = Vector2.zero;
        }
        else
        {
            rd.linearVelocity = -transform.right * (_speed * speedMultiplier);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Audio.Instance != null)
            Audio.Instance.GameOverSound();

            SceneManager.LoadScene("GameOver");
        }
        else if (other.CompareTag("BabyDuck"))
        {
            if (Audio.Instance != null)
            Audio.Instance.DeathSound();

            BabyDucksCode babyManager = FindFirstObjectByType<BabyDucksCode>();
            if (babyManager != null)
                babyManager.removeDuck(other.transform);

            MotherDuckCode mother = FindFirstObjectByType<MotherDuckCode>();
            if (mother != null)
                mother.DucklingEaten();
        }
    }

    //OnCollision check for layer or tag for a duck. if so destroy game object. 


}
