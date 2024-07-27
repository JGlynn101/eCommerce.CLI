using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.Models;

namespace AmazonApp.ViewModels
{
    public class CustomerViewModel
    {
        private Customer _customer;
        public Customer? Customer
        {
            get => _customer;
            set
            {
                if (_customer != value)
                {
                    _customer = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Name
        {
            get => _customer.Name;
            set
            {
                if (_customer.Name != value)
                {
                    _customer.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get => _customer.Email;
            set
            {
                if (_customer.Email != value)
                {
                    _customer.Email = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Address
        {
            get => _customer.Address;
            set
            {
                if (_customer.Address != value)
                {
                    _customer.Address = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Id
        {
            get => _customer.Id;
            set
            {
                if (_customer.Id != value)
                {
                    _customer.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public CustomerViewModel(Customer c)
        {
            _customer = c;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
