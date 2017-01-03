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
    public class GeneralViewFragment : FragmentBase
    {
        public static Int32 LayoutResourceId = Resource.Layout.view_general;

        private TextView m_txtDeviceName;
        private TextView m_txtFwVer;
        private TextView m_txtBuild;
        private TextView m_txtHwVer;

        public override int LayoutId
        {
            get
            {
               return LayoutResourceId;
            }
        }

        protected override void InitializeControls()
        {
            //m_txtDeviceName = m_View.FindViewById<TextView>(Resource.Id.view_general_devicename);
            //m_txtFwVer = m_View.FindViewById<TextView>(Resource.Id.view_general_fw_ver);
            //m_txtBuild = m_View.FindViewById<TextView>(Resource.Id.view_general_build);
            //m_txtHwVer = m_View.FindViewById<TextView>(Resource.Id.view_general_hw_ver);
        }
        
        protected override void SetValuesToControls()
        {
            //m_txtDeviceName.Text = ViewModel.DeviceName;
            //m_txtFwVer.Text = ViewModel.FwVer;
            //m_txtBuild.Text = ViewModel.Build;
            //m_txtHwVer.Text = ViewModel.HwVer;
        }
    }
}