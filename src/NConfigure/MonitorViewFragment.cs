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
    public class MonitorViewFragment : Fragment
    {
        private static MonitorViewFragment s_Instance;
        public static MonitorViewFragment Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new MonitorViewFragment();
                return s_Instance;
            }
        }

        private MonitorViewFragment()
        {

        }

        private View m_View;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (m_View == null)
                m_View = inflater.Inflate(Resource.Layout.view_monitor, container, false);

            return m_View;
        }
    }
}