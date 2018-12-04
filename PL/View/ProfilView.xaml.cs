using System;
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
using Prism.Events;
using Prism.Commands;
using PL.Tools;

namespace PL.View
{
    /// <summary>
    /// Interaction logic for ProfilView.xaml
    /// </summary>
    public partial class ProfilView : UserControl
    {

        private ProfilVM _viewModel;


        public ProfilView(IEventAggregator eventAggregator , IMyMessageDialog myMessageDialog)
        {
            InitializeComponent();
            _viewModel = new ProfilVM(eventAggregator, myMessageDialog);
            DataContext = _viewModel;
        }
    }
}


