using Shoot;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootSettings", menuName = "ScriptableObjects/ShootSettings")]
public class ShootSettings : ScriptableObject
{
    [Header("Время перезарядки пули")]
    public float CurrentTime = 5f;
    [Header("Количество в магазине")]
    public int MaxCountClip = 1;

    [Header("Время перезарядки магазина(заполнять при количестве в магазине более 1)")]
    public float CurrentTimeClip = 5f;

    [Header("Режим управления")]
    public ModeShoot ModeShoot;

}
