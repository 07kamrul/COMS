using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface ICOMSLogger
    {
        void Information(string logText);
        void Warning(string logText);
        void Error(string logText);
        void Debug(string logText);
        void Fatal(string logText);
    }
}
