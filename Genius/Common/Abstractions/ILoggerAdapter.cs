using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genius.Common.Abstractions;

public interface ILoggerAdapter
{
    void Fatal(Exception? exception, string messageTemplate);
    void Fatal(Exception? exception, string messageTemplate, params object?[]? propertyValues);
    void Fatal(string messageTemplate);
    void Fatal(string messageTemplate, params object?[]? propertyValues);

    void Error(Exception? exception, string messageTemplate);
    void Error(Exception? exception, string messageTemplate, params object?[]? propertyValues);
    void Error(string messageTemplate);
    void Error(string messageTemplate, params object?[]? propertyValues);

    void Warning(Exception? exception, string messageTemplate);
    void Warning(Exception? exception, string messageTemplate, params object?[]? propertyValues);
    void Warning(string messageTemplate);
    void Warning(string messageTemplate, params object?[]? propertyValues);

    void Information(Exception? exception, string messageTemplate);
    void Information(Exception? exception, string messageTemplate, params object?[]? propertyValues);
    void Information(string messageTemplate);
    void Information(string messageTemplate, params object?[]? propertyValues);

    void Debug(Exception? exception, string messageTemplate);
    void Debug(Exception? exception, string messageTemplate, params object?[]? propertyValues);
    void Debug(string messageTemplate);
    void Debug(string messageTemplate, params object?[]? propertyValues);

    void Verbose(Exception? exception, string messageTemplate);
    void Verbose(Exception? exception, string messageTemplate, params object?[]? propertyValues);
    void Verbose(string messageTemplate);
    void Verbose(string messageTemplate, params object?[]? propertyValues);


}
