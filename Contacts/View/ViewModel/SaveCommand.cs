using System;
using System.Windows.Input;
using View.Model;
using View.Model.Services;

namespace View.ViewModel
{
    public class SaveCommand : ICommand
    {
        private readonly ContactSerializer _contactSerializer;
        private readonly Contact _contact;
        private readonly Action _updateContact;

        public SaveCommand(ContactSerializer contactSerializer, Contact contact, Action updateContact)
        {
            _contactSerializer = contactSerializer ?? throw new ArgumentNullException(nameof(contactSerializer));
            _contact = contact ?? throw new ArgumentNullException(nameof(contact));
            _updateContact = updateContact ?? throw new ArgumentNullException(nameof(updateContact));
        }

        public bool CanExecute(object parameter) => true;

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _updateContact();
            _contactSerializer.SaveToJson(_contact);
        }
    }
}
