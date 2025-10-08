namespace Genius.QRCode.Middleware.Application.Abstractions;

/// <summary>
/// Define um contrato para serviços que notificam sobre eventos de WebSocket de QR Code.
/// </summary>
public interface IQRCodeEventNotifier
{
    event Func<string, Task>? OnMessageReceived;
}