using TopDownFramework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TopDownFramework.Interface
{
    public interface IWeapon
    {
        AttackMode attackMode { get; }

        void Attack(Vector3 attackerPossition, Quaternion attackerRotation, Vector3 targetPossition);
    }
}
