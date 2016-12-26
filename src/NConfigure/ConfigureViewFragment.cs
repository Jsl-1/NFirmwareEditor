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
using static Android.Widget.TabHost;

namespace NToolbox
{
    public class ConfigureViewFragment : Fragment
    {
        private static ConfigureViewFragment s_Instance;
        public static ConfigureViewFragment Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new ConfigureViewFragment();
                return s_Instance;
            }
        }

        private ConfigureViewFragment()
        {

        }

        private View m_View;
        private TabHost m_TabHost;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (m_View == null)
            {
                m_View = inflater.Inflate(Resource.Layout.view_configure, container, false);

                m_TabHost = m_View.FindViewById<TabHost>(Resource.Id.configureTabHost);
                m_TabHost.Setup();

                TabSpec spec1 = m_TabHost.NewTabSpec("tabProfile");
                spec1.SetContent(Resource.Id.configureTabProfile);
                spec1.SetIndicator("Profile");

                TabSpec spec2 = m_TabHost.NewTabSpec("tabGeneral");
                spec2.SetIndicator("General");
                spec2.SetContent(Resource.Id.configureTabGeneral);

                TabSpec spec3 = m_TabHost.NewTabSpec("tabAdvanced");
                spec3.SetIndicator("Advanced");
                spec3.SetContent(Resource.Id.configureTabAdvanced);

                m_TabHost.AddTab(spec1);
                m_TabHost.AddTab(spec2);
                m_TabHost.AddTab(spec3);
            }
            return m_View;
        }
    }
}