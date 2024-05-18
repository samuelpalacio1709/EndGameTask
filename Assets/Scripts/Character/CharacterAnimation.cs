using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Attack settings")]
    [SerializeField] private string attackLayerName = "Attack";
    [SerializeField] private float attackTransitionDuration = 0.4f;
    float attackTransitionSpeed = 0f;

    private float attackWeight = 0f;
    private void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        CharacterInput.OnInputMovement += UpdateMovementAnimation;
        CharacterInput.onInputAttack += UpdateAttackAnimation;
    }
    private void OnDisable()
    {
        CharacterInput.OnInputMovement -= UpdateMovementAnimation;
        CharacterInput.onInputAttack -= UpdateAttackAnimation;

    }
    private void Update()
    {
        SetSmoothAttackAnimation();
    }
    private void SetSmoothAttackAnimation()
    {
        var layer = animator.GetLayerIndex(attackLayerName);
        var currentWeight = animator.GetLayerWeight(layer);
        animator.SetLayerWeight(layer,
                Mathf.SmoothDamp(currentWeight, attackWeight, ref attackTransitionSpeed, attackTransitionDuration));
    }

    private void UpdateMovementAnimation(Vector2 vector)
    {
        animator.SetBool("Running", vector.magnitude > 0);
    }
    private void UpdateAttackAnimation(float attackWeight)
    {
        this.attackWeight = attackWeight;
    }


}
