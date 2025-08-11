using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genius.Api.Common;

public static class AppConstants
{
    public static string CorsPolicyName = "wasm";   // O nome é indiferente

    public static string DefaultBackendUrl = "http://localhost:8081";
    public static string DefaultBackendHost = "localhost";
    public static int DefaultBackendPort = 8080;

    public static string DefaultQRCodeApiUrl = "http://localhost:8082";

}