﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JJWATQuery
{
    /// <summary>
    /// SelReportControl.xaml 的交互逻辑
    /// </summary>
    public partial class SelReportControl : UserControl
    {
        public SelReportControl()
        {
            InitializeComponent();
        }

        public static Visifire.Charts.Chart rePort;

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            rePort = rePortChar;
        }
    }
}
