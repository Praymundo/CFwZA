namespace CFwZA.Properties {
     // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    internal sealed partial class Settings {
        public bool Modified { get; private set; }

        public Settings() {
            this.SettingChanging += Settings_SettingChanging;
            this.SettingsSaving += Settings_SettingsSaving;
            this.PropertyChanged += this.Settings_PropertyChanged;
            this.SettingsLoaded += Settings_SettingsLoaded;
            this.Modified = false;
        }

        private void Settings_SettingsLoaded(object sender, System.Configuration.SettingsLoadedEventArgs e)
        {
            if (this.Modified) this.Modified = false;
        }

        private void Settings_SettingChanging(object sender, System.Configuration.SettingChangingEventArgs e)
        {
            if (!this.Modified) this.Modified = true;
        }

        private void Settings_SettingsSaving(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.Modified)
            {
                e.Cancel = true;
                return;
            }
            this.Modified = false;
        }

        private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!this.Modified) this.Modified = true;
        }
    }
}
