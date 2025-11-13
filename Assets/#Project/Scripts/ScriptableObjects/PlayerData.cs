using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game Design/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] public float speed = 3f;
    [SerializeField] public float jumpForce = 25f;

    [Space]
    [Header("Health")]
    [SerializeField] public int hpMax = 100;
    [SerializeField] public int damage = 10;
}
