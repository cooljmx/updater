﻿namespace Installer.StateMachine;

public interface IApplicationContext
{
    TValue GetValue<TValue>(string key)
        where TValue : notnull;

    void SetValue<TValue>(string key, TValue value)
        where TValue : notnull;
}