using UnityEngine;

namespace _3C.Character.Statics
{
    public static class Animations
    {
        private static readonly string vertical = "Vertical";
        private static readonly string horizontal = "Horizontal";
        private static readonly string gather = "Gather";
        private static readonly string sprint = "Sprint";
        private static readonly string inventory = "Inventory";
        private static readonly string read = "Read";
        // private static string endLoad = "end";
        private static readonly string die = "die";
    
        public static int VERTICAL => Animator.StringToHash(vertical);
        public static int HORIZONTAL => Animator.StringToHash(horizontal);
        public static int SPRINT => Animator.StringToHash(sprint);
        public static int GATHER => Animator.StringToHash(gather);
        public static int INVENTORY => Animator.StringToHash(inventory);
        public static int READ => Animator.StringToHash(read);
        // public static int ENDLOAD => Animator.StringToHash(endLoad);
        public static int DIE => Animator.StringToHash(die);
    }
}