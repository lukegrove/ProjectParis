using System.Collections.Generic;

namespace ProjectParis.Combat.Ships
{
    public class ShipDestroyer : Ship
    {
        public ShipDestroyer()
        {
            SetShipType(ShipType.Destroyer);
            SetMaxSpeed(10f);
            SetAcceleration(1f);
            SetTurnSpeed(3f);
            SetHull(10f);
            SetWeaponLimit(2f);
            List<WeaponType> loadout = new()
            {
                new WeaponShort(),
                new WeaponLong()
            };
            SetWeaponLoadout(loadout);
        }
    }
}