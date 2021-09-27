using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownFramework
{
    public class Ability : Player
    {
        protected Player player;

        protected override void Initialization()
        {
            base.Initialization();

            player = GetComponent<Player>();
        }

    }
}
