using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Emnd
{
    public partial class SurveyEntryCell : ViewCell
    {
        public SurveyEntryCell()
        {
            InitializeComponent();
        }
        public string LabelText
        {
            set
            {
                ProfileEntryLabel.Text = value;
            }
        }

        public bool IsVisible
        {
            get
            {
                return (View == null) ? true : View.IsVisible;
            }
            set
            {
                if (View != null)
                {
                    View.IsVisible = value;
                }
            }
        }

        public string Icon
        {
            set
            {
                bool ShowIcon = !string.IsNullOrWhiteSpace(value);
                if (ShowIcon)
                {
                    ProfileEntryIcon.Icon = value;
                    ProfileEntryIcon.IconColor = Color.Gray;
                }
            }
        }

        public string Placeholder
        {
            set
            {
                ProfileEntry.Placeholder = value;
            }
        }

        public Keyboard Keyboard
        {
            get
            {
                return ProfileEntry.Keyboard;
            }
            set
            {
                ProfileEntry.Keyboard = value;
            }
        }

        public string BindingField
        {
            get
            {
                return ProfileEntry.Text;
            }
            set
            {
                Binding fieldBinding = new Binding(value);
                //fieldBinding.Source = MyProfilePageModel.BindingContext;
                ProfileEntry.SetBinding(Entry.TextProperty, fieldBinding);
            }
        }

        public Entry Entry
        {
            get
            {
                return ProfileEntry;
            }
        }

    }
}
