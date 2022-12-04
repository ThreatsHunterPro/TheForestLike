using _3C.Character.Movement;
using _3C.Character.Needs;
using _3C.Character.Widget;
using UnityEngine;

namespace _3C.Character
{
    [RequireComponent(typeof(Animator), typeof(CharacterController), typeof(PlayerWidget))]
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerNeeds)/*, typeof(SightBehavior)*/)]
    // [RequireComponent(typeof(GatherBehavior), typeof(PlayerInventory), typeof(BuildBehavior))]
    public class Player : MonoBehaviour
    {
        [Header("Player values")]
        [SerializeField] private Animator animator = null;
        [SerializeField] private PlayerWidget widget = null;
        [SerializeField] private PlayerMovement movement = null;
        [SerializeField] private PlayerNeeds needs = null;
        
        public Animator Animator => animator;
        public PlayerWidget Widget => widget;
        public PlayerMovement Movement => movement;
        public PlayerNeeds Needs => needs;

        private void Start() => Init();

        private void Init()
        {
            
        }
    }
}