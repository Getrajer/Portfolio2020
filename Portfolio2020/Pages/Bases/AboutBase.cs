using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Pages.Bases
{
    public class AboutBase : ComponentBase
    {
        protected bool display_ActiveIS = false;
        protected bool display_freelance = false;
        protected bool display_Ezsat = false;

        protected string ISicon = "+";
        protected string freeIcon = "+";
        protected string EzsatIcon = "+";


        public void DisplayIS()
        {
            if (display_ActiveIS)
            {
                display_ActiveIS = false;
                ISicon = "+";
            }
            else
            {
                display_ActiveIS = true;
                ISicon = "-";
            }
        }

        public void DisplayFreelance()
        {
            if (display_freelance)
            {
                display_freelance = false;
                freeIcon = "+";
            }
            else
            {
                display_freelance = true;
                freeIcon = "-";
            }
        }

        public void DisplayEzsat()
        {
            if (display_Ezsat)
            {
                display_Ezsat = false;
                EzsatIcon = "+";
            }
            else
            {
                display_Ezsat = true;
                EzsatIcon = "-";
            }
        }

    }
}
