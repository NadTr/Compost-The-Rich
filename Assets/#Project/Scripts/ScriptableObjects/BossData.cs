using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "Game Design/BossData")]
public class BossData : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] public float speedMin = 2f;
    [SerializeField] public float speedMax = 6f;
    [SerializeField] public float jumpForceMin = 100f;
    [SerializeField] public float jumpForceMax = 250f;
    [SerializeField] public float stateChangeEveryXSecondsMin = 3f;
    [SerializeField] public float stateChangeEveryXSecondsMax = 7f;
    [SerializeField] public bool goRight = true;
    
    [Space]
    [Header("Health")]
    [SerializeField] public int hpMax = 100;
    [SerializeField] public int damage = 10;
    
}
