using MudBlazor;
using System;

namespace zhgut2app.Services
{
    public class ThemeService
    {
        public bool IsDarkMode { get; private set; } = false;
        public MudTheme Theme { get; }

        public event Action OnThemeChanged;

        public ThemeService()
        {
            Theme = new MudTheme
            {
                PaletteLight = _lightPalette,
                PaletteDark = _darkPalette,
                LayoutProperties = new LayoutProperties()
            };
        }

        public void ToggleDarkMode()
        {
            IsDarkMode = !IsDarkMode;
            OnThemeChanged?.Invoke();
        }

        private readonly PaletteLight _lightPalette = new()
        {
            Black = "#000000",
            AppbarText = "#000000",
            AppbarBackground = "#ffffff",
            DrawerBackground = "#ffffff",
            GrayLight = "#e8e8e8",
            GrayLighter = "#f9f9f9",
            Background = "#f3f3f3",
            Primary = "#ff5900",
            TextPrimary = "#000000",
        };

        private readonly PaletteDark _darkPalette = new()
        {
            Primary = "#7e6fff",
            Surface = "#1e1e2d",
            Background = "#1a1a27",
            BackgroundGray = "#151521",
            AppbarText = "#92929f",
            AppbarBackground = "rgba(26,26,39,0.8)",
            DrawerBackground = "#1a1a27",
            ActionDefault = "#74718e",
            ActionDisabled = "#9999994d",
            ActionDisabledBackground = "#605f6d4d",
            TextPrimary = "#b2b0bf",
            TextSecondary = "#92929f",
            TextDisabled = "#ffffff33",
            DrawerIcon = "#92929f",
            DrawerText = "#92929f",
            GrayLight = "#2a2833",
            GrayLighter = "#1e1e2d",
            Info = "#4a86ff",
            Success = "#3dcb6c",
            Warning = "#ffb545",
            Error = "#ff3f5f",
            LinesDefault = "#33323e",
            TableLines = "#33323e",
            Divider = "#292838",
            OverlayLight = "#1e1e2d80",
        };
    }
}
