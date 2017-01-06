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
using Android.Preferences;

namespace NToolbox
{
    public class ViewProfileDetailPreferences : PreferenceFragmentBase
    {
        public const Int32 XmlPreferenceId = Resource.Xml.preferences_profile_detail;
        
        public override int PreferenceId
        {
            get
            {
                return XmlPreferenceId;
            }
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
           

            base.OnCreate(savedInstanceState);

           

        }



    }
}