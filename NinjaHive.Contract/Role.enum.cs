using System;

namespace NinjaHive.Contract
{
    [Flags]
    public enum Role
    {
        Member = 1 << 0,
        Admin = 1 << 1,
        GameDesigner = 1 << 2,
    };
}
