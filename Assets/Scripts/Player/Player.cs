using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private readonly Inputs inputs = new Inputs();

        public Inputs Inputs => inputs;
        
        void Start()
        {
        
        }

        void Update()
        {
        
        }
    }
}