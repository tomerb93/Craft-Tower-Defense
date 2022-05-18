using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Interfaces
{
    internal interface IViewWithButton : IView
    {
        void SetOnEventHandlers();
    }
}