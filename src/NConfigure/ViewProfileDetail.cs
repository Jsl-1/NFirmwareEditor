using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NToolbox.UI;
using NToolbox.ViewModels;
using Android.Support.Design.Widget;
using Android.Text;
using Java.Lang;

namespace NToolbox
{
    public class ViewProfileDetail : FragmentBase
    {
        public const Int32 LayoutResourceId = Resource.Layout.view_profile_detail;

        public override int LayoutId
        {
            get
            {
                return LayoutResourceId;
            }
        }

        private EditText m_nameEdit;
        private EditText m_PowerEdit;
        private InputFilterDecimalMinMax m_PowerEditFilter;
        private Spinner m_PreheatTypeSpinner;
        private EditText m_PrehitPowerEdit;
        private Spinner m_PreheatCurveSpinner;

        protected override void InitializeControls()
        {
            var nameEditWrapper = m_View.FindViewById<TextInputLayout>(Resource.Id.view_profile_detail_name_wrapper);
            nameEditWrapper.Hint = "Profile Name";
            m_nameEdit = m_View.FindViewById<EditText>(Resource.Id.view_profile_detail_name);

            var powerEditWrapper = m_View.FindViewById<TextInputLayout>(Resource.Id.view_profile_detail_power_wrapper);
            powerEditWrapper.Hint = "Power (W)";
            m_PowerEdit = m_View.FindViewById<EditText>(Resource.Id.view_profile_detail_power);
            m_PowerEditFilter = new InputFilterDecimalMinMax() { Min = 0, Max = 200 };
            m_PowerEditFilter.Decimals = 1;
            m_PowerEdit.SetFilters(new IInputFilter[] { m_PowerEditFilter });
            m_PowerEdit.InputType = InputTypes.ClassNumber | InputTypes.NumberFlagDecimal;

            var preheatTypeEditWrapper = m_View.FindViewById<TextInputLayout>(Resource.Id.view_profile_detail_preheattype_wrapper);
            preheatTypeEditWrapper.Hint = "Preheat Type";
            m_PreheatTypeSpinner = m_View.FindViewById<Spinner>(Resource.Id.view_profile_detail_preheattype);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleSpinnerItem, ArcticFoxConfigurationViewModel.PreheatTypes.Keys.ToArray());
            m_PreheatTypeSpinner.Adapter = adapter;

            var preheatPowerEditWrapper = m_View.FindViewById<TextInputLayout>(Resource.Id.view_profile_detail_power_wrapper);
            preheatPowerEditWrapper.Hint = "Prehit Power (W)";
            m_PrehitPowerEdit = m_View.FindViewById<EditText>(Resource.Id.view_profile_detail_preheatpower);
            m_PowerEditFilter = new InputFilterDecimalMinMax() { Min = 0, Max = 200 };
            m_PowerEditFilter.Decimals = 1;
            m_PrehitPowerEdit.SetFilters(new IInputFilter[] { m_PowerEditFilter });
            m_PrehitPowerEdit.InputType = InputTypes.ClassNumber | InputTypes.NumberFlagDecimal;


        }

        private void PowerEdit_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e)
        {

        }

        protected override void SetValuesToControls()
        {
            ProfileSelection profile;
            if (System.Enum.TryParse<ProfileSelection>(this.Tag, out profile))
            {
                var profileViewModel = ViewModel.GetProfileViewModel(profile);

                m_nameEdit.Text = profileViewModel.ProfileName;

                m_PowerEditFilter.Max = Convert.ToInt32(profileViewModel.MaxPower);                
                m_PowerEdit.Text = new Java.Text.DecimalFormat("#0.##").Format(Convert.ToDouble(profileViewModel.Power));

                _SetSpinnerValue(m_PreheatTypeSpinner, profileViewModel.PreheatType);

                m_PrehitPowerEdit.Text = new Java.Text.DecimalFormat("#0.##").Format(Convert.ToDouble(profileViewModel.PreHitPower));
            }

        }

        private void _SetSpinnerValue(Spinner spinner, string value)
        {
            int index = 0;
            for (int i = 0; i < spinner.Count; i++)
            {
                if (spinner.GetItemAtPosition(i).ToString().Equals(value))
                {
                    spinner.SetSelection(i);
                    break;
                }
            }
        }

        public class InputFilterDecimalMinMax : Java.Lang.Object, IInputFilter
        {

            public Decimal Min { get; set; } = 0;

            public Decimal Max { get; set; } = 0;

            public Int32    Decimals { get; set; } = 2;

            public ICharSequence FilterFormatted(ICharSequence source, int start, int end, ISpanned dest, int dstart, int dend)
            {
                decimal value = 0;

                var finalString = dest.ToString() + source.ToString();
                if(finalString.EndsWith(",")|| finalString.EndsWith("."))
                    finalString += "0";
                if (Decimal.TryParse(finalString, out value))
                {
                    if (value >= Min && value <= Max)
                    {
                        var pointPosition = finalString.IndexOfAny(new char[] { ',', '.' });
                        if (pointPosition == -1 || finalString.Length > pointPosition + 1 + Decimals)
                            return null;
                    }
                }

                var numberOfDecimals = 0;
               

                return new Java.Lang.String("");              
            }         
        }
    }
}