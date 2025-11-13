using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game Design/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 25f;

    [Space]
    [Header("Health")]
    private int hp;
    [SerializeField] private int hpMax = 100;
    [SerializeField] private int damage = 10;
}
