using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Commands
{
    class SearchCommand_EXAMPLE
    {
    }
}

/*
////////////////////////////////////
אינטרפיס

    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMVVM
{
   public interface ISearch
    {
       void SearchStudents(string Topic);
    }
}

//////////////////////////////////////

קומנד



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestMVVM.Commands
{
    public class SearchCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private ISearch CurrentVM;
        public SearchCommand(ISearch CurrentVM)
        {
            this.CurrentVM = CurrentVM;
        }
        public bool CanExecute(object parameter)
        {
            bool result = false;

            if (!(parameter is null))
            {
                var p = parameter.ToString();
                if (p.Length > 2) result = true;
            }
            return result;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is null))
            {
                var p = parameter.ToString();
                CurrentVM.SearchStudents(p);
            }
        }
    }
}
*/
