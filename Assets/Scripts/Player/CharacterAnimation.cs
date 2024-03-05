using UnityEngine;

namespace Player
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
    
        private static readonly int AttackAnim = Animator.StringToHash("Attack");
        private static readonly int StrongAttackAnim = Animator.StringToHash("StrongAttack");
        private static readonly int DieAnim = Animator.StringToHash("Die");
        private static readonly int Speed = Animator.StringToHash("Speed");

        public void PlayLightAttackAnim() => _animator.SetTrigger(AttackAnim);
        public void PlayStrongAttackAnim() => _animator.SetTrigger(StrongAttackAnim);

        public void PlayDieAnim() => _animator.SetTrigger(DieAnim);

        public void PlayAnimateMovement(Vector3 dir) => _animator.SetFloat(Speed, dir == Vector3.zero ? 0f : 1f);
    }
}