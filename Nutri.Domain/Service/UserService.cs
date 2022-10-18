using Nutri.Domain.Model;

namespace Nutri.Domain.Service;

public class UserService
{
    private List<UserSetting> _userSettings = new();

    public UserService()
    {
        _userSettings.Add(new("David", new DateTime(2003, 4, 13), 73, new() { RecCalories = 2000 }));
    }

    public UserSetting CurrentUserSetting => _userSettings.First();
}