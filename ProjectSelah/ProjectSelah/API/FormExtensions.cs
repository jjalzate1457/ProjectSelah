﻿using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ProjectSelah.API
{
    public class FormExtensions
    {
        public static bool IsInDesignMode
        {
            get
            {
                bool isInDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime;

                if (!isInDesignMode)
                {
                    using (var process = Process.GetCurrentProcess())
                    {
                        var a = process.ProcessName.ToLowerInvariant().Contains("devenv");
                        return process.ProcessName.ToLowerInvariant().Contains("devenv");
                    }
                }

                return isInDesignMode;
            }
        }


        public static event PropertyChangedEventHandler PropertyChanged;

        public static void NotifyPropertyChanged(Window window, [CallerMemberName] string propName = null) =>
            PropertyChanged?.Invoke(window, new PropertyChangedEventArgs(propName));
    }
}
