using System;
using System.Collections.Generic;
using System.Text;

namespace ManDown.Models
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
