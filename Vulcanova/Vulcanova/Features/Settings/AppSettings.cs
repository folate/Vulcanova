using System;
using System.Globalization;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Essentials;

namespace Vulcanova.Features.Settings;

public class AppSettings : ReactiveObject
{
    private const string DisableReadReceiptsKey = "Options_DisableReadReceipts";
    [Reactive] public bool DisableReadReceipts { get; set; } = Preferences.Get(DisableReadReceiptsKey, false);

    public ModifiersSettings Modifiers { get; } = new();

    public AppSettings()
    {
        this.WhenAnyValue(v => v.DisableReadReceipts)
            .Skip(1)
            .Subscribe(v =>
                Preferences.Set(DisableReadReceiptsKey, v));
    }
}

public class ModifiersSettings : ReactiveObject
{
    public ModifierSettings PlusSettings { get; } = new("Options_ValueOfPlus", 0.5m);
    public ModifierSettings MinusSettings { get; } = new("Options_ValueOfMinus", -0.25m);
}

public class ModifierSettings : ReactiveObject
{
    [Reactive] public decimal SelectedValue { get; set; }

    [Reactive] public bool UsesCustomValue { get; set; }

    public ModifierSettings(string key, decimal defaultValue)
    {
        SelectedValue = decimal.Parse(Preferences.Get(key, defaultValue.ToString(CultureInfo.InvariantCulture)),
            CultureInfo.InvariantCulture);

        UsesCustomValue = Preferences.Get($"{key}IsCustom", false);

        this.WhenAnyValue(v => v.SelectedValue)
            .Subscribe(value 
                => Preferences.Set(key, value.ToString(CultureInfo.InvariantCulture)));
            
        this.WhenAnyValue(vm => vm.UsesCustomValue)
            .Subscribe(value =>
            {
                Preferences.Set($"{key}IsCustom", value);
            });
    }
}