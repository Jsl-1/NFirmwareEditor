using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Hardware.Usb;
using System.Collections.Generic;
using NCore.USB;
using System;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Support.V7.View;
using Android.Support.V4.View;
using Android.Graphics.Drawables;
using NCore;
using NToolbox.Models;
using System.Globalization;

namespace NToolbox
{
    [Activity(Label = "NFE Toolbox", MainLauncher = true, Icon = "@drawable/icon", LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
    [IntentFilter(new string[] { Android.Hardware.Usb.UsbManager.ActionUsbDeviceAttached })]
    [MetaData(Android.Hardware.Usb.UsbManager.ActionUsbDeviceAttached, Resource = "@xml/usb_device_filter")]
    public class MainActivity : Activity, MainMenuAdapter.OnItemClickListener, View.IOnClickListener
    {
        private const string ACTION_USB_PERMISSION = "com.android.example.USB_PERMISSION";

        private DrawerLayout mDrawerLayout;
        private RecyclerView mDrawerList;
        private ActionBarDrawerToggle mDrawerToggle;

        private TextView mTxtDevice;
        private TextView mTxtFirmware;
        private CheckBox mChkConnected;
        private TextView mTxtConnected;

        private Boolean m_IsConnected;
        private int m_TxtConnectedStringId = Resource.String.text_disconnected;
        private String[] mViewTitles;

       
        private HidUsbReceiver m_HidUsbReceiver;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            mViewTitles = new string[]
            {
                GetString(Resource.String.view_configuration_title),
                GetString(Resource.String.view_control_title),
                GetString(Resource.String.view_monitor_title),
                GetString(Resource.String.view_debug_title),
            };

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);   
            // set a custom shadow that overlays the main content when the drawer opens
            //mDrawerLayout.SetDrawerShadow(Resource.Drawable.drawer_shadow, GravityCompat.Start);

        

            mDrawerList = FindViewById<RecyclerView>(Resource.Id.left_drawer);
            // improve performance by indicating the list if fixed size.
            mDrawerList.HasFixedSize = true;
            mDrawerList.SetLayoutManager(new LinearLayoutManager(this));

            // set up the drawer's list view with items and click listener
            mDrawerList.SetAdapter(new MainMenuAdapter(mViewTitles, this));
            // enable ActionBar app icon to behave as action to toggle nav drawer
            this.ActionBar.SetDisplayHomeAsUpEnabled(true);
            this.ActionBar.SetHomeButtonEnabled(true);
            this.ActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_drawer);

            mDrawerList.SetOnClickListener(this);
            // ActionBarDrawerToggle ties together the the proper interactions
            // between the sliding drawer and the action bar app icon

            mDrawerToggle = new MainMenuActionBarDrawerToggle(
                this, mDrawerLayout,
                Resource.String.drawer_open,
                Resource.String.drawer_close);

            mDrawerLayout.AddDrawerListener(mDrawerToggle);
            
            if (bundle == null) //first launch
                mDrawerLayout.OpenDrawer(mDrawerList);

            HidConnectorInstance.HidConnector.DeviceConnected += HidConnector_DeviceConnected;
            HidConnectorInstance.HidConnector.RefreshState();

            mChkConnected = FindViewById<CheckBox>(Resource.Id.chk_connected);
            mChkConnected.Checked = m_IsConnected;

            mTxtConnected = FindViewById<TextView>(Resource.Id.txt_connected);
            mTxtConnected.Text = GetString(m_TxtConnectedStringId);

            mTxtDevice = FindViewById<TextView>(Resource.Id.txt_device);
            mTxtFirmware= FindViewById<TextView>(Resource.Id.txt_firmware);

            _InitUsb();
        }


        protected override void OnResume()
        {
            base.OnResume();
            HidConnectorInstance.HidConnector.RefreshState();
        }

        private void HidConnector_DeviceConnected(bool connected)
        {
            m_IsConnected = connected;
            m_TxtConnectedStringId = connected ? Resource.String.text_connected : Resource.String.text_disconnected;
            if (mChkConnected != null)
                mChkConnected.Checked = connected;
            if (mTxtConnected != null)
                mTxtConnected.Text = GetString(m_TxtConnectedStringId);

            if (connected)
            {
                var data = HidConnector.Instance.ReadConfiguration();
                var info = BinaryStructure.Read<ArcticFoxConfiguration>(data);
                if (mTxtDevice != null)
                {

                    mTxtDevice.Text = HidDeviceInfo.Get(info.Info.ProductId).Name;
                    mTxtFirmware.Text = String.Format(
                        GetString(Resource.String.format_firmware_version),
                        new object[] {
                        (info.Info.FirmwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture),
                        info.Info.FirmwareBuild,
                        (info.Info.HardwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture)
                        });
                }
            }
        }       

        private void _InitUsb()
        {
            IntentFilter filter = new IntentFilter(ACTION_USB_PERMISSION);
            filter.AddAction(UsbManager.ActionUsbDeviceAttached);
            filter.AddAction(UsbManager.ActionUsbDeviceDetached);
            filter.AddAction(UsbManager.ActionUsbAccessoryAttached);
            filter.AddAction(UsbManager.ActionUsbAccessoryDetached);
            filter.AddAction(UsbManager.ActionUsbDeviceDetached);

            m_HidUsbReceiver = new HidUsbReceiver();

            RegisterReceiver(m_HidUsbReceiver, filter);
        }

        private void ShowDeviceInfo(UsbDevice device)
        {
           // m_DebugTextView.Append(device.GetDeviceInfoText());
        }

        public void OnClick(View view, int position)
        {
            var title = mViewTitles[position];
            Fragment fragment = null;
            if(title == GetString(Resource.String.view_debug_title))
            {
                fragment = DebugViewFragment.Instance;
            }
            else if(title == GetString(Resource.String.view_configuration_title))
            {
                fragment = ConfigureViewFragment.Instance;
            }
            else if (title == GetString(Resource.String.view_control_title))
            {
                fragment = ConfigureViewFragment.Instance;
            }
            else if (title == GetString(Resource.String.view_monitor_title))
            {
                fragment = MonitorViewFragment.Instance;
            }
            if (fragment != null)
            {
                var fragmentManager = this.FragmentManager;
                var ft = fragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.content_frame, fragment);
                ft.Commit();
            }
            mDrawerLayout.CloseDrawer(mDrawerList);
        }

        public void OnClick(View v)
        {
            if (mDrawerLayout.IsDrawerOpen(mDrawerList))
            {
                mDrawerLayout.CloseDrawer(mDrawerList);
            }
            else
            {
                mDrawerLayout.OpenDrawer(mDrawerList);
            }
           
        }
    }


  
}

