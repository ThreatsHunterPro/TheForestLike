using Player;
using UnityEngine;

namespace _3C.Character
{
    public class Player : MonoBehaviour
    {
        [Header("Player values")]
        [SerializeField] private Inputs inputs = new Inputs();
        [SerializeField] private Animator animator = null;
        [SerializeField] private PlayerMovement movement = null;
        
        public Inputs Inputs => inputs;
        public Animator Animator => animator;
        public PlayerMovement Movement => movement;

        void Start() => Init();

        void Init()
        {
            
        }
    }
}