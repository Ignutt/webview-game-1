using System;
using UnityEngine;

namespace Obstacles
{
    public class Square : MonoBehaviour
    {
        [SerializeField] private float speedRotation = 7f;
        
        private Rigidbody2D _rigidbody;
        
        public Vector3 TargetVelocity { get; set; }

        public event Action OnDie;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward * speedRotation * Time.deltaTime);            
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = TargetVelocity;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnDie?.Invoke();
            
            Destroy(gameObject);
        }
    }
}
