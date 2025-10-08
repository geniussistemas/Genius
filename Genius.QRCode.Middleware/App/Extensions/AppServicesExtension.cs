using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genius.QRCode.Middleware.App.Extensions;

public static class AppServicesExtension
{
    public static WebApplication UseServices(this WebApplication app)
    {
        return app;
    }
}