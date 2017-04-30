using System;
using System.Collections.Generic;
using System.Text;

namespace ManDown
{
    public interface IDialer
    {
        bool Dial(string number);
    }
}
