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

namespace NToolbox
{
    public class GeneralViewFragment : Fragment
    {

        private View m_View;

        private TextView m_txtDeviceName;
        private TextView m_txtFwVer;
        private TextView m_txtBuild;
        private TextView m_txtHwVer;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (m_View == null)
            {
                m_View = inflater.Inflate(Resource.Layout.view_general, container, false);
                m_txtDeviceName = m_View.FindViewById<TextView>(Resource.Id.view_general_devicename);
                m_txtFwVer = m_View.FindViewById<TextView>(Resource.Id.view_general_fw_ver);
                m_txtBuild = m_View.FindViewById<TextView>(Resource.Id.view_general_build);
                m_txtHwVer = m_View.FindViewById<TextView>(Resource.Id.view_general_hw_ver);

                m_txtDeviceName.Text = DeviceName;
                m_txtFwVer.Text = FwVer;
                m_txtBuild.Text = Build;
                m_txtHwVer.Text = HwVer;
            }
            return m_View;
        }


        private string m_DeviceName;
        public String DeviceName
        {
            get { return m_DeviceName; }
            set
            {
                m_DeviceName = value;
                if(m_txtDeviceName != null)
                    m_txtDeviceName.Text = value;
            }
        }

        private string m_FwVer;
        public String FwVer
        {
            get { return m_FwVer;  }
            set
            {
                m_FwVer = value;
                if (m_txtFwVer != null)
                    m_txtFwVer.Text = value;
            }
        }

        private string m_HwVer;
        public String HwVer
        {
            get { return m_HwVer; }
            set
            {
                m_HwVer = value;
                if (m_txtHwVer != null)
                    m_txtHwVer.Text = value;
            }
        }

        private string m_Build;
        public String Build
        {
            get { return m_Build; }
            set
            {
                m_Build = value;
                if (m_txtBuild != null)
                    m_txtBuild.Text = value;
            }
        }
    }
}