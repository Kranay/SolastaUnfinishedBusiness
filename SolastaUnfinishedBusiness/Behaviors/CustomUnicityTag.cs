﻿namespace SolastaUnfinishedBusiness.Behaviors;

internal interface ICustomUnicityTag
{
    string UnicityTag { get; }
}

internal class CustomUnicityTag(string tag) : ICustomUnicityTag
{
    public string UnicityTag { get; } = tag;
}
