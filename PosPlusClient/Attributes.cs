using System;
namespace PosPlusClient;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class PermissionAttribute(string permission) : Attribute
{
    public string Permission { get; } = permission;
}