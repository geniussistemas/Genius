using Genius.Api.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genius.Api;

public static class Configuration
{
    public static string BackendUrl { get; set; } = AppConstants.DefaultBackendUrl;
    public static string QRCodeApiUrl { get; set; } = AppConstants.DefaultQRCodeApiUrl;
    public static string BackendHost { get; set; } = AppConstants.DefaultBackendHost;
    public static int BackendPort { get; set; } = AppConstants.DefaultBackendPort;

    public static string ConnectionString { get; set; } = string.Empty;
}