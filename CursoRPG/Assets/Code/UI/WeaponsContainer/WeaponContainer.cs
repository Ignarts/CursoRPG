using Items;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class WeaponContainer : MonoBehaviour
    {
        public static WeaponContainer Instance;

        #region Private Attributes

        [SerializeField] private Image weaponIcon;
        [SerializeField] private Image weaponSkillIcon;
        
        #endregion

        #region Properties

        public WeaponItem EquippedWeapon { get; private set; }
        
        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Equip the weapon and set the icons
        /// </summary>
        /// <param name="weaponItem"></param>
        public void EquipWeapon(WeaponItem weaponItem)
        {
            EquippedWeapon = weaponItem;
            
            Weapon weapon = weaponItem.Weapon;
            weaponIcon.sprite = weapon.WeaponIcon;
            weaponIcon.gameObject.SetActive(true);

            if(weapon.WeaponType == WeaponType.Melee)
                return;
            
            weaponSkillIcon.sprite = weapon.SkillIcon;
            weaponSkillIcon.gameObject.SetActive(true);
        }

        /// <summary>
        /// Remove the weapon and hide the icons
        /// </summary>
        public void RemoveWeapon()
        {
            EquippedWeapon = null;
            weaponIcon.gameObject.SetActive(false);
            weaponSkillIcon.gameObject.SetActive(false);
        }
        
        #endregion
    }
}
