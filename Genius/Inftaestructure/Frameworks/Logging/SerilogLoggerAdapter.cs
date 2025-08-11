using Genius.Common.Abstractions;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Genius.Common.Lib;

namespace Genius.Inftaestructure.Frameworks.Logging;

public class SerilogLoggerAdapter : ILoggerAdapter
{
    public SerilogLoggerAdapter()
    {
        Log.Information("Logger configurado com sucesso (arquivo inicial {logFilePath}).", Utils.GetLogPath());
    }

    public void Fatal(Exception? exception, string messageTemplate) => Log.Logger.Fatal(exception, messageTemplate);
    public void Fatal(Exception? exception, string messageTemplate, params object?[]? propertyValues) => Log.Logger.Fatal(exception, messageTemplate, propertyValues);
    public void Fatal(string messageTemplate) => Log.Logger.Fatal(messageTemplate);
    public void Fatal(string messageTemplate, params object?[]? propertyValues) => Log.Logger.Fatal(messageTemplate, propertyValues);

    public void Error(Exception? exception, string messageTemplate) => Log.Logger.Error(exception, messageTemplate);
    public void Error(Exception? exception, string messageTemplate, params object?[]? propertyValues) => Log.Logger.Error(exception, messageTemplate, propertyValues);
    public void Error(string messageTemplate) => Log.Logger.Error(messageTemplate);
    public void Error(string messageTemplate, params object?[]? propertyValues) => Log.Logger.Error(messageTemplate, propertyValues);

    public void Warning(Exception? exception, string messageTemplate) => Log.Logger.Warning(exception, messageTemplate);
    public void Warning(Exception? exception, string messageTemplate, params object?[]? propertyValues) => Log.Logger.Warning(exception, messageTemplate, propertyValues);
    public void Warning(string messageTemplate) => Log.Logger.Warning(messageTemplate);
    public void Warning(string messageTemplate, params object?[]? propertyValues) => Log.Logger.Warning(messageTemplate, propertyValues);

    public void Information(Exception? exception, string messageTemplate) => Log.Logger.Information(exception, messageTemplate);
    public void Information(Exception? exception, string messageTemplate, params object?[]? propertyValues) => Log.Logger.Information(exception, messageTemplate, propertyValues);
    public void Information(string messageTemplate) => Log.Logger.Information(messageTemplate);
    public void Information(string messageTemplate, params object?[]? propertyValues) => Log.Logger.Information(messageTemplate, propertyValues);

    public void Debug(Exception? exception, string messageTemplate) => Log.Logger.Debug(exception, messageTemplate);
    public void Debug(Exception? exception, string messageTemplate, params object?[]? propertyValues) => Log.Logger.Debug(exception, messageTemplate, propertyValues);
    public void Debug(string messageTemplate) => Log.Logger.Debug(messageTemplate);
    public void Debug(string messageTemplate, params object?[]? propertyValues) => Log.Logger.Debug(messageTemplate, propertyValues);

    public void Verbose(Exception? exception, string messageTemplate) => Log.Logger.Verbose(exception, messageTemplate);
    public void Verbose(Exception? exception, string messageTemplate, params object?[]? propertyValues) => Log.Logger.Verbose(exception, messageTemplate, propertyValues);
    public void Verbose(string messageTemplate) => Log.Logger.Verbose(messageTemplate);
    public void Verbose(string messageTemplate, params object?[]? propertyValues) => Log.Logger.Verbose(messageTemplate, propertyValues);

}
