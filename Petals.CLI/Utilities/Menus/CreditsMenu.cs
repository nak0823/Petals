using Petals.CLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petals.CLI.Utilities.Menus
{
    internal class CreditsMenu : Menu
    {
        public override string Label => "Credits";

        public override void OnSelect()
        {
           Interface.SetTitle(Label);
        }
    }
}
