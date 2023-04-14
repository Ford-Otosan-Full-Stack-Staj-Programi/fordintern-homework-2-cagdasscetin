using System.ComponentModel;

namespace OdevApi.Base;

public enum RoleEnum
{
    [Description(Role.Admin)]
    Admin = 1,

    [Description(Role.Editor)]
    EditorQTNS = 2,

    [Description(Role.User)]
    EditorQTDA = 3,
}

public class Role
{
    public const string Admin = "admin";
    public const string Editor = "editor";
    public const string User = "user";
}
