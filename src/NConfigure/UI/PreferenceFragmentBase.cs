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
using NToolbox.ViewModels;
using Android.Preferences;

namespace NToolbox.UI
{
    public abstract class PreferenceFragmentBase : PreferenceFragment, ISharedPreferencesOnSharedPreferenceChangeListener
    {       
        public abstract Int32 PreferenceId { get; }

        protected View m_View;


        protected ArcticFoxConfigurationViewModel ViewModel
        {
            get
            {
                return ((MainActivity)this.Activity).ViewModel;
            }
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RefreshUi();

           
        }

        public override void OnResume()
        {
            base.OnResume();
            PreferenceScreen.SharedPreferences.RegisterOnSharedPreferenceChangeListener(this);
            ViewModel.OnReload += ViewModel_OnReload;
        }

        private void ViewModel_OnReload(object sender, EventArgs e)
        {
            RefreshUi();           
        }

        public override void OnPause()
        {
            base.OnPause();
            PreferenceScreen.SharedPreferences.UnregisterOnSharedPreferenceChangeListener(this);
            ViewModel.OnReload -= ViewModel_OnReload;
        }


        public virtual void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            var preference = FindPreference(key);
            if (preference != null)
            {
                _UpdatePrefSummary(FindPreference(key));
            }
        }

        public virtual void RefreshUi()
        {
            if(PreferenceScreen != null)
                PreferenceScreen.RemoveAll();

            if (Tag != null)
                PreferenceManager.SharedPreferencesName = String.Format("{0}", Tag);

            this.AddPreferencesFromResource(PreferenceId);

            _InitSumary(PreferenceScreen);
        }


        private void _InitSumary(Preference p)
        {
            if (p is PreferenceGroup) {
                PreferenceGroup pGrp = (PreferenceGroup)p;
                for (int i = 0; i < pGrp.PreferenceCount; i++)
                {
                    _InitSumary(pGrp.GetPreference(i));
                }
            } else {
                _UpdatePrefSummary(p);
            }
        }

        private void _UpdatePrefSummary(Preference p)
        {
            try
            {
                if (p is ListPreference)
                {
                    ListPreference listPref = (ListPreference)p;
                    p.Summary = listPref.Entry;
                }
                else if (p is MultiSelectListPreference)
                {
                    EditTextPreference editTextPref = (EditTextPreference)p;
                    p.Summary = editTextPref.Text;
                }
                else if(p is Controls.Preferences.SeekBarPreference)
                {
                    p.Summary = ((Controls.Preferences.SeekBarPreference)p).GetValueText();
                }

            }
            catch(Exception ex)
            {
                Toast.MakeText(Context, "Error while reading some values",ToastLength.Long);
            }
        }


    }
}