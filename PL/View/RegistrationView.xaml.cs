﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.ViewModel;
using PL.ViewModel;
using Prism.Events;
using PL.Tools;


namespace PL.View
{
    /// <summary>
    /// Interaction logic for registrationView.xaml
    /// </summary>
    public partial class RegistrationView : UserControl
    {
        private RegistrationVM _viewModel;

        public RegistrationView(IEventAggregator eventAggregator, IMyMessageDialog myMessageDialog)
        {
            InitializeComponent();
            _viewModel = new RegistrationVM(eventAggregator, myMessageDialog);
            DataContext = _viewModel;
        }
    }
}
