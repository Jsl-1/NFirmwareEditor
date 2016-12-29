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
    public class InitialViewFragment : Fragment
    {

        private View m_View;
        private TextView m_TxtMinVersion;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (m_View == null)
            {
                m_View = inflater.Inflate(Resource.Layout.view_welcome, container, false);
                m_TxtMinVersion = m_View.FindViewById<TextView>(Resource.Id.txt_welcome_version);
                m_TxtMinVersion.Text = FirmwareMinVersion;
            }

            return m_View;
        }

        private string m_FirmwareMinVersion;
        public String FirmwareMinVersion
        {
            get { return m_FirmwareMinVersion;  }
            set {
                m_FirmwareMinVersion = value;
                if (m_TxtMinVersion != null)
                    m_TxtMinVersion.Text = value;
            }
        }


    }
}