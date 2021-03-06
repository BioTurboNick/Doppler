﻿using Doppler.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Doppler.Controls
{
    public sealed partial class ChannelControlPanel : UserControl
    {
        public ChannelControlPanel(ChannelViewModel channel = null)
        {
            ViewModel = channel;

            this.InitializeComponent();
        }
        

        public ChannelViewModel ViewModel { get; set; }

        public bool? Neither(bool value1, bool value2) => !(value1 || value2);
    }
}
