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

using Fragment = Android.App.Fragment;
using NToolbox.UI;

namespace NToolbox
{
    public class InitialViewFragment : FragmentBase
    {
        public const Int32 LayoutResourceId = Resource.Layout.view_welcome;

        private TextView m_TxtMinVersion;

        public override int LayoutId
        {
            get
            {
                return LayoutResourceId;
            }
        }

        protected override void InitializeControls()
        {
            m_TxtMinVersion = View.FindViewById<TextView>(Resource.Id.txt_welcome_version);
        }

        protected override void SetValuesToControls()
        {
            if(m_TxtMinVersion != null)
                m_TxtMinVersion.Text = ViewModel.MinimumBuildNumber;
        }      
    }
}