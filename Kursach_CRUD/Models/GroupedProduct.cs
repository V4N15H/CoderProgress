using Kursach_CRUD.Models;
using System.ComponentModel;
using System.Collections.Generic;

namespace Kursach_CRUD.Models
{
    public class GroupedProduct : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

        public List<string> SerialNumbers { get; set; }

        public int Quantity => SerialNumbers?.Count ?? 0;

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged(nameof(IsExpanded));
                    OnPropertyChanged(nameof(ToggleButtonText));
                }
            }
        }

        public string ToggleButtonText => IsExpanded ? "Скрыть номера" : "Показать номера";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
