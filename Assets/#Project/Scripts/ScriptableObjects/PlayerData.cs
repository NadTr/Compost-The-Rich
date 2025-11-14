using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game Design/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] public bool frontDirectionRight = true;
    [SerializeField] public float speed = 3f;
    [SerializeField] public float firstJumpForce = 220f;
    [SerializeField] public float secondJumpForce = 50f;

    [Space]
    [Header("Health")]
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int damage = 10;
}
