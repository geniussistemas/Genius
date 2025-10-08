using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genius.QRCode.Middleware.Common;

public static class AppConstants
{
    public const string CorsPolicyName = "wasm";   // O nome é indiferente

    public const string DefaultBackendUrl = "http://localhost:8081";
    public const string DefaultBackendHost = "localhost";
    public const int DefaultBackendPort = 8080;

    public const string DefaultQRCodeApiUrl = "http://localhost:8082";

}