using Common;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public static PlayerMovement Instance { get; private set; }
        
        [Header("Managers and handlers")] 
        [SerializeField] private TouchHandler touchHandler;
        [SerializeField] private GameObject graphic;
    
        [Header("Movement properties")] 
        [SerializeField] private float speedMovement = 6f;
    
        [Header("Direction point")]
        [SerializeField] private Transform beginPoint;
        [SerializeField] private Transform endPoint;

        private Rigidbody2D _rigidbody;
        private Vector3 _currentTarget;

        public GameObject Graphic => graphic;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _currentTarget = beginPoint.position;

            touchHandler.OnTouchStart += () =>
            {
                ChangeTarget();
            };
        }

        private void Update()
        {
            CheckTarget();
        }

        private void FixedUpdate()
        {
            MoveToTarget();
        }

        private void MoveToTarget()
        {
            Vector3 newPosition = Vector3.MoveTowards(
                _rigidbody.position,
                _currentTarget,
                speedMovement * Time.fixedDeltaTime);
        
            _rigidbody.MovePosition(newPosition);
        }

        private void CheckTarget()
        {
            if (transform.position == _currentTarget)
            {
                ChangeTarget();
            }
        }

        private void ChangeTarget()
        {
            if (_currentTarget == beginPoint.position)
            {
                _currentTarget = endPoint.position;
                return;
            }
        
            _currentTarget = beginPoint.position;
        }
    
    }
}
