using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinBookStoreApp.Pickers
{

    // Thanks yWayne Creasey, Xamarin Forms EnumBindablePicker


    public class EnumBindablePicker<T> : Picker where T : struct
    {
        public EnumBindablePicker()
        {
            SelectedIndexChanged += OnSelectedIndexChanged;
            //Fill the Items from the enum
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                Items.Add(value.ToString());
            }
        }

        public static BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(T), typeof(EnumBindablePicker<T>), default(T), propertyChanged: OnSelectedItemChanged, defaultBindingMode: BindingMode.TwoWay);

        public T SelectedItem
        {
            get
            {
                return (T)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
            {
                SelectedItem = default(T);
            }
            else
            {
                SelectedItem = (T)Enum.Parse(typeof(T), Items[SelectedIndex]);
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var picker = bindable as EnumBindablePicker<T>;
            if (newvalue != null)
            {
                picker.SelectedIndex = picker.Items.IndexOf(newvalue.ToString());
            }
        }
    }
}
