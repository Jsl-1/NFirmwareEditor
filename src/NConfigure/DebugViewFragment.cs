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
using NCore.USB;

namespace NToolbox
{
    public class DebugViewFragment : Fragment
    {

        private View m_View;
        private TextView m_DebugTextView;
        private Button m_TestButton;
           

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (m_View == null)
            {
                m_View = inflater.Inflate(Resource.Layout.view_debug, container, false);

                //DebugTextView
                m_DebugTextView = m_View.FindViewById<TextView>(Resource.Id.UsbTextView);

                //Test Button
                m_TestButton = m_View.FindViewById<Button>(Resource.Id.TestButton);
                m_TestButton.Click += M_TestButton_Click;

   
            }
            return m_View;
        }

        private void M_TestButton_Click(object sender, System.EventArgs e)
        {
            try
            {

                if (HidConnector.Instance.IsDeviceConnected)
                {
                    HidConnector.Instance.RestartDevice();
                    //var dataflash = HidConnectorInstance.HidConnector.ReadDataflash();
                    //HidConnectorInstance.HidConnector.MakePuff(1);
                }
            }
            catch(Exception ex)
            {
                m_DebugTextView.Append("\n");
                m_DebugTextView.Text = ex.Message;
                m_DebugTextView.Append("\n");
                if (ex.InnerException != null)
                {
                    m_DebugTextView.Append(ex.InnerException.Message);
                    m_DebugTextView.Append("\n");
                }
                m_DebugTextView.Append(ex.StackTrace);

            }

        }
    }
}