using CharlotteSchou_Booking.View;
using CharlotteSchou_Booking.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CharlotteSchou_Booking.Commands
{
    public class DeleteArtistCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            bool result = true;

            if (parameter is ArtistController ac)
            {
                if (ac.SelectedArtistVM == null)
                    return false;
            }
            return result;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ArtistController ac)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Du er nu ved at slette artisten.\nEr du sikker på at du vil fortsætte?", "Advarsel!", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes)
                {
                    ac.Remove(ac.SelectedArtistVM);

                }
            }
        }
    }
}
