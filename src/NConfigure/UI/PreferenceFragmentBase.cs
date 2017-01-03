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
                return ArcticFoxConfigurationViewModel.Instance;
            }
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if(Tag != null)
                PreferenceManager.SharedPreferencesName = String.Format("{0}", Tag);

            this.AddPreferencesFromResource(PreferenceId);

            _InitSumary(PreferenceScreen);
        }

        public override void OnResume()
        {
            base.OnResume();
            PreferenceScreen.SharedPreferences.RegisterOnSharedPreferenceChangeListener(this);
        }

        public override void OnPause()
        {
            base.OnPause();
            PreferenceScreen.SharedPreferences.UnregisterOnSharedPreferenceChangeListener(this);
        }

        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            _UpdatePrefSummary(FindPreference(key));
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
            if (p is ListPreference) {
                ListPreference listPref = (ListPreference)p;
                p.Summary = listPref.Entry;
            }
            if (p is EditTextPreference) {
                EditTextPreference editTextPref = (EditTextPreference)p;
                if (p.Title.ToLower().Contains("password"))
                {
                    p.Summary = "******";
                }
                else
                {
                    p.Summary = editTextPref.Text;
                }
            }
            if (p is MultiSelectListPreference) {
                EditTextPreference editTextPref = (EditTextPreference)p;
                p.Summary = editTextPref.Text;
            }
        }


    }
}