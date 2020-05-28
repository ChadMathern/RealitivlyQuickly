using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RQ
{
    public class PlayerMaster : MonoBehaviour
    {
        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventInventoryChanged;
        public event GeneralEventHandler EventAmmoChanged;

        public delegate void AmmoPickupEventHandler(string ammoType, int quantity);
        public event AmmoPickupEventHandler EventPickedUpAmmo;

        public delegate void PlayerHealthEventHandler(int healthChange);
        public event PlayerHealthEventHandler EventPlayerHealthDecrease;
        public event PlayerHealthEventHandler EventPlayerHealthIncrease;

        public void CallEventInventoryChanged()
        {
            if (EventInventoryChanged != null)
            {
                EventInventoryChanged();
            }
        }
        public void CallEventAmmoChanged()
        {
            if (EventAmmoChanged != null)
            {
                EventAmmoChanged();
            }
        }

        public void CallEventPickedUpAmmo(string ammoType, int quantity)
        {
            if (EventPickedUpAmmo != null)
            {
                EventPickedUpAmmo(ammoType, quantity);
            }
        }

        public void CalleEventPlayerHealthDecrease(int Damage)
        {
            if (EventPlayerHealthDecrease != null)
            {
                EventPlayerHealthDecrease(Damage);
            }
        }

        public void PlayerHealthIncrease(int Increase)
        {
            if (EventPlayerHealthIncrease != null)
            {
                EventPlayerHealthIncrease(Increase);
            }
        }

    }
}
