using CharlotteSchou_Booking.Viewmodel;
using CharlotteSchou_Booking.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CharlotteSchou_Booking.Commands
{
    public class DeleteBookingCommand: ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            bool result = true;

            if (parameter is BookingController bc)
            {
                if (bc.SelectedCustomerVM == null)
                    return false;
            }
            return result;
        }

        public void Execute(object? parameter)
        {
            if (parameter is BookingController bc)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Du er nu ved at slette en Booking.\nEr du sikker på at du vil fortsætte?", "Advarsel!", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes)
                {
                    bc.Remove(bc.SelectedCustomerVM.BookingVM);

                }
            }
        }
    }
}
