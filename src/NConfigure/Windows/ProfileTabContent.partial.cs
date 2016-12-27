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

namespace NToolbox.Windows
{
    partial class ProfileTabContent
    {
        //private static PowerCurveComboBox PowerCurveComboBox


        private void InitializeComponent()
        {

        }

        private View m_View;
        Models.ArcticFoxConfiguration.Profile Profile { get; set; }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (m_View == null)
            {
                m_View = inflater.Inflate(Resource.Layout.view_profile, container, false);



            }
            return m_View;
        }
    }
}