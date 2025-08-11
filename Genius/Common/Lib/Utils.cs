using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genius.Common.Lib;

public static class Utils
{
    public static string GetLogPath()
    {
        return $"logs\\{System.AppDomain.CurrentDomain.FriendlyName}.log";
    }

}
