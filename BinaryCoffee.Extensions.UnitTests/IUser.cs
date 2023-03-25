using CommunityToolkit.Mvvm.Input;
using Mvvm.Extensions.Generator.Attributes;

namespace Mvvm.Extensions.UnitTests
{
    public enum Role
    {
        Admin,
        Moderator,
        Guest,
        User
    }

    [ObservableModel]
    public interface IUser
    {
        bool IsValidCredential { get; set; }
        Visibility VisibleBasedOnAuth => IsLoggedOn ? Visibility.Visible : Visibility.Collapsed;
        bool IsLoggedOn => Service.IsLoggedOn && IsValidCredential;
        string TopLabelText => IsLoggedOn ? "Hi, user" : "Initializating...";
        [Ignore]
        IMultiBotService Service { get; }
        AsyncRelayCommand ExitCommand { get; }
    }

    public enum Visibility {
        Visible,
        Collapsed
    }
    public interface IMultiBotService { bool IsLoggedOn { get; } }

    public partial class User
    {
        private partial bool CanExecuteExit() => true;

        private partial Task ExecuteExitAsync() => Task.CompletedTask;
    }
}
