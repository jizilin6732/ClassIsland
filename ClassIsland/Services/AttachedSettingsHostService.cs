﻿using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using ClassIsland.Controls;
using ClassIsland.Interfaces;
using ClassIsland.Models;
using Microsoft.Extensions.Hosting;

namespace ClassIsland.Services;

public class AttachedSettingsHostService : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
    }

    public ObservableCollection<Type> TimePointSettingsAttachedSettingsControls { get; } = new();
    public ObservableCollection<Type> TimeLayoutSettingsAttachedSettingsControls { get; } = new();
    public ObservableCollection<Type> ClassPlanSettingsAttachedSettingsControls { get; } = new();
    public ObservableCollection<Type> SubjectSettingsAttachedSettingsControls { get; } = new();

    public static T? GetAttachedSettingsByPriority<T>(Guid id, Subject? subject = null,
        TimeLayoutItem? timeLayoutItem = null, ClassPlan? classPlan = null, TimeLayout? timeLayout = null)
    {
        var l = new AttachableSettingsObject?[] { subject, timeLayoutItem, classPlan, timeLayout };
        foreach (var i in l)
        {
            if (i == null) continue;
            var o = i.GetAttachedObject<T>(id);
            if (((IAttachedSettings?)o)?.IsAttachSettingsEnabled == true)
            {
                return o;
            }
        }

        return default;
    }
}