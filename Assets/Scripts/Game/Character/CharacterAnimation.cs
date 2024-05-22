using UnityEngine;

/// <summary>
/// Handles character animations
/// </summary>
public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private CharacterSettings characterSettings;

    [Header("Attack enemySettings")]
    [SerializeField] private string attackLayerName = "Attack";
    [SerializeField] private float attackTransitionDuration = 0.4f;
    private float attackTransitionSpeed = 0f;
    private float attackWeight = 0f;
    private void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        characterSettings = GetComponent<IGameEntity>().GetSettings() as CharacterSettings;
    }
    private void OnDisable()
    {
        UpdateAttackAnimation(CharacterAttackState.Rest, Vector3.zero);
    }

    private void Update()
    {
        SetSmoothAttackAnimation();
    }

    /// <summary>
    /// Set attack layer animation
    /// </summary>
    private void SetSmoothAttackAnimation()
    {
        var layer = animator.GetLayerIndex(attackLayerName);
        var currentWeight = animator.GetLayerWeight(layer);
        animator.SetLayerWeight(layer,
                Mathf.SmoothDamp(currentWeight, attackWeight, ref attackTransitionSpeed, attackTransitionDuration));
    }

    public void UpdateMovementAnimation(Vector2 vector)
    {
        animator.SetBool("Running", vector.magnitude > 0);
    }
    public void UpdateAttackAnimation(CharacterAttackState state, Vector3 direction)
    {
        this.attackWeight = state == CharacterAttackState.Attack ? 1 : 0;
    }

}
