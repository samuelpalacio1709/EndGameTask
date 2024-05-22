using UnityEngine;

/// <summary>
/// Handles the visibility of the aim rectangular bar
/// </summary>
public class AimHelper : MonoBehaviour
{
    [SerializeField] private GameObject aimHelper;
    private void OnEnable()
    {
        CharacterInput.onInputAttack += ChangeAimState;
    }
    private void OnDisable()
    {
        CharacterInput.onInputAttack += ChangeAimState;
    }

    private void ChangeAimState(CharacterAttackState state, Vector3 target)
    {
        aimHelper.gameObject.SetActive(state == CharacterAttackState.Aim);
    }
}
