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
    public class ControlViewFragment : Fragment
    {
        private View m_View;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (m_View == null)
                m_View = inflater.Inflate(Resource.Layout.view_control, container, false);

            return m_View;
        }
    }
}