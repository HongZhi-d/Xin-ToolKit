namespace Xin_ToolKit.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}