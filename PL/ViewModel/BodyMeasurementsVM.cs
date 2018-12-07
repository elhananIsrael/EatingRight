using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Events;
using Prism.Commands;
using PL.Events;
using PL.View;
using BL;
using BE.Entitys;
using PL.Tools;


namespace PL.ViewModel
{
    class BodyMeasurementsVM: BaseVM
    {

        public BodyMeasurementsVM(IEventAggregator eventAggregator, IMyMessageDialog myMessageDialog)
        {
            _eventAggregator = eventAggregator;
            _myMessageDialog = myMessageDialog;
            _eventAggregator.GetEvent<PL.Events.UpdateUserEvent>()
              .Subscribe(updateDetails);

            myBl = new Bl();

            SaveBodyMeasurementsCommand = new DelegateCommand<Type>(RunSaveBodyMeasurements, CanSaveBodyMeasurements);
        }

        public Bl myBl;
        private IEventAggregator _eventAggregator;
        private IMyMessageDialog _myMessageDialog;

        private BodyMeasurement myBodyMeasurements;


        public DateTime SelectedDate
        {
            get
            {
                if (myBodyMeasurements == null)
                {
                    updateMyBodyMeasurements();
                }
                
                return myBodyMeasurements.Date;
            }
            set
            {
                if (value != null && value!=myBodyMeasurements.Date)
                {
                    myBodyMeasurements = new BodyMeasurement();
                    myBodyMeasurements.Date = value;
                    updateMyBodyMeasurements();
                    /*OnPropertyChanged("Weight");
                    OnPropertyChanged("Height");*/
                    OnPropertyChanged();
                }
            }
        }


        public double? Weight
        {
            get
            {
                return myBodyMeasurements.Weight;
            }
            set
            {
                if (value != null)
                    myBodyMeasurements.Weight = value;

                OnPropertyChanged();
                ((DelegateCommand<Type>)SaveBodyMeasurementsCommand).RaiseCanExecuteChanged();
            }
        }

        public double? Height
        {
            get
            {
                return myBodyMeasurements.Height;

            }
            set
            {
                if (value != null)
                    myBodyMeasurements.Height = value;

                OnPropertyChanged();
                ((DelegateCommand<Type>)SaveBodyMeasurementsCommand).RaiseCanExecuteChanged();
            }
        }

        private bool CanSaveBodyMeasurements(Type arg)
        {
            if(myBodyMeasurements.Height<0 || myBodyMeasurements.Height == null ||
                myBodyMeasurements.Weight < 0 || myBodyMeasurements.Weight == null)
              return false;
            return true;
        }

        private async void RunSaveBodyMeasurements(Type obj)
        {
            await SaveBodyMeasurements();
        }

        private async Task SaveBodyMeasurements()
        {
            try
            {
                BodyMeasurement temp = new BodyMeasurement();
                temp.Date = myBodyMeasurements.Date;
                temp.Weight = myBodyMeasurements.Weight;
                temp.Height = myBodyMeasurements.Height;

                await myBl.AddBodyMeasurement(temp);
                await _myMessageDialog.ShowInfoDialogAsync("Body Measurement Saved!");
                OnPropertyChanged();
            }
            catch (Exception ex)
            {
                updateMyBodyMeasurements();
                await _myMessageDialog.ShowInfoDialogAsync(ex.Message);
            }
        }

        public async Task updateMyBodyMeasurements()
        {
            try
            {
                if (myBodyMeasurements == null)
                myBodyMeasurements = new BodyMeasurement();
            var tempBodyMeasurements = await myBl.GetBodyMeasurement(myBodyMeasurements.Date);
                if (tempBodyMeasurements == null)
                {
                    tempBodyMeasurements = new BodyMeasurement();
                    tempBodyMeasurements.Date = myBodyMeasurements.Date;
                    myBodyMeasurements = tempBodyMeasurements;
                    Weight = tempBodyMeasurements.Weight;
                    Height = tempBodyMeasurements.Height;
                    await _myMessageDialog.ShowInfoDialogAsync("Please Update Your Body Measurements For This Day.");
                }
                else
                {
                    myBodyMeasurements = tempBodyMeasurements;
                    Weight = tempBodyMeasurements.Weight;
                    Height = tempBodyMeasurements.Height;
                }


            }
            catch (Exception ex)
            {
                updateMyBodyMeasurements();
                await _myMessageDialog.ShowInfoDialogAsync(ex.Message);
            }

        }

        public async void updateDetails()
        {
        }

        //////////////////////////////////////////// Commands:
        public ICommand SaveBodyMeasurementsCommand { get; }
    }
}
