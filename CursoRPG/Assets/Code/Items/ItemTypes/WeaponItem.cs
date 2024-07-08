using Items;
using UnityEngine;
using Weapons;
namespace Items
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 3)]
    public class WeaponItem : InventoryItems
    {
        [Header("Weapon Attributes")]
        [SerializeField] private Weapon _weapon;
    }
}
