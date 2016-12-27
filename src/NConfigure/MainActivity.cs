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
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;

namespace NToolbox
{
    [Activity(Label = "NFE Toolbox", MainLauncher = true, Theme = "@style/Theme.NToolbox", Icon = "@drawable/icon", LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
    [IntentFilter(new string[] { Android.Hardware.Usb.UsbManager.ActionUsbDeviceAttached })]
    [MetaData(Android.Hardware.Usb.UsbManager.ActionUsbDeviceAttached, Resource = "@xml/usb_device_filter")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener //Activity, MainMenuAdapter.OnItemClickListener, View.IOnClickListener
    {
        private const string ACTION_USB_PERMISSION = "com.android.example.USB_PERMISSION";

        private DrawerLayout mDrawerLayout;
        private NavigationView mNavigationView;
        private Dictionary<int, Fragment> mFragmentCache = new Dictionary<int, Fragment>(); 

        private TextView mTxtDevice;
        private TextView mTxtFirmware;
        private CheckBox mChkConnected;
        private TextView mTxtConnected;

        private Boolean m_IsConnected;
        private Boolean m_IsFirstDeviceConnection = true;
        private int m_TxtConnectedStringId = Resource.String.text_disconnected;
       
        private HidUsbReceiver m_HidUsbReceiver;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

          

            _InitializeComponents();
            _InitializeUsb();
        }

        private void _InitializeComponents()
        {
            SetContentView(Resource.Layout.activity_main);

            //Toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_drawer);

            //Navigation View
            mNavigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            mNavigationView.SetNavigationItemSelectedListener(this);

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var actionbarDrawerToggle = new MainMenuActionBarDrawerToggle(this, mDrawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            mDrawerLayout.AddDrawerListener(actionbarDrawerToggle);
            actionbarDrawerToggle.SyncState();


            //mChkConnected = FindViewById<CheckBox>(Resource.Id.chk_connected);
            //mChkConnected.Checked = m_IsConnected;

            //mTxtConnected = FindViewById<TextView>(Resource.Id.txt_connected);
            //mTxtConnected.Text = GetString(m_TxtConnectedStringId);

            //mTxtDevice = FindViewById<TextView>(Resource.Id.txt_device);
            //mTxtFirmware= FindViewById<TextView>(Resource.Id.txt_firmware);
        }

        private void _InitializeUsb()
        {
            IntentFilter filter = new IntentFilter(ACTION_USB_PERMISSION);
            filter.AddAction(UsbManager.ActionUsbDeviceAttached);
            filter.AddAction(UsbManager.ActionUsbDeviceDetached);
            filter.AddAction(UsbManager.ActionUsbAccessoryAttached);
            filter.AddAction(UsbManager.ActionUsbAccessoryDetached);
            filter.AddAction(UsbManager.ActionUsbDeviceDetached);

            m_HidUsbReceiver = new HidUsbReceiver();

            RegisterReceiver(m_HidUsbReceiver, filter);

            HidConnectorInstance.HidConnector.DeviceConnected += HidConnector_DeviceConnected;
            HidConnectorInstance.HidConnector.RefreshState();

        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            ////Checking if the item is in checked state or not, if not make it in checked state
            //if (menuItem.IsChecked) menuItem.SetChecked(false);
            //else menuItem.SetChecked(true);

            //Closing drawer on item click
            mDrawerLayout.CloseDrawers();

            //Declare fragment to display
            Fragment fragment = null;

            if (!mFragmentCache.TryGetValue(menuItem.ItemId, out fragment))
            {

                //Check to see which item was being clicked and perform appropriate action
                switch (menuItem.ItemId)
                {
                    case Resource.Id.nav_configure:
                        fragment = new ConfigureViewFragment();
                        break;
                    //case Resource.Id.nav_profile1:
                    //    fragment = new ProfileViewFragment();
                    //    break;
                    //case Resource.Id.nav_profile2:
                    //    fragment = new ProfileViewFragment();
                    //    break;
                    //case Resource.Id.nav_profile3:
                    //    fragment = new ProfileViewFragment();
                    //    break;
                    //case Resource.Id.nav_profile4:
                    //    fragment = new ProfileViewFragment();
                    //    break;
                    //case Resource.Id.nav_profile5:
                    //    fragment = new ProfileViewFragment();
                    //    break;
                    //case Resource.Id.nav_profile6:
                    //    fragment = new ProfileViewFragment();
                    //    break;
                    //case Resource.Id.nav_profile7:
                    //    fragment = new ProfileViewFragment();
                    //    break;
                    //case Resource.Id.nav_profile8:
                    //    fragment = new ProfileViewFragment();
                    //    break;
                    //case Resource.Id.nav_control:
                    //    fragment = new ControlViewFragment();
                    //    break;
                    //case Resource.Id.nav_monitor:
                    //    fragment = new MonitorViewFragment();
                    //    break;
                    case Resource.Id.nav_debug:
                        fragment = new DebugViewFragment();
                        break;
                    default:
                        Toast.MakeText(ApplicationContext, "Invalid Menu Item", 0).Show();
                        return false;
                }

                mFragmentCache.Add(menuItem.ItemId, fragment);
            }

            if (fragment != null)
            {
                var fragmentManager = this.FragmentManager;
                var ft = fragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.frame, fragment);
                ft.Commit();
            }
            else
            {
                Toast.MakeText(ApplicationContext, "Error creating view", 0).Show();
                return false;
            }
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    if(mDrawerLayout.IsDrawerOpen(Android.Support.V4.View.GravityCompat.Start))
                        mDrawerLayout.CloseDrawer(Android.Support.V4.View.GravityCompat.Start);
                    else
                        mDrawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            return true;
        }

        protected override void OnResume()
        {
            base.OnResume();
            HidConnectorInstance.HidConnector.RefreshState();
        }

        private void HidConnector_DeviceConnected(bool connected)
        {
            m_IsConnected = connected;



            //m_TxtConnectedStringId = connected ? Resource.String.text_connected : Resource.String.text_disconnected;
            //if (mChkConnected != null)
            //    mChkConnected.Checked = connected;
            //if (mTxtConnected != null)
            //    mTxtConnected.Text = GetString(m_TxtConnectedStringId);

            if (connected)
            {
                if (m_IsFirstDeviceConnection)
                {
                    m_IsFirstDeviceConnection = false;
                }

                var data = HidConnector.Instance.ReadConfiguration();
                var info = BinaryStructure.Read<ArcticFoxConfiguration>(data);
                if (mTxtDevice != null)
                {

                    //mTxtDevice.Text = HidDeviceInfo.Get(info.Info.ProductId).Name;
                    //mTxtFirmware.Text = String.Format(
                    //    GetString(Resource.String.format_firmware_version),
                    //    new object[] {
                    //        (info.Info.FirmwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture),
                    //        info.Info.FirmwareBuild,
                    //        (info.Info.HardwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture)
                    //    });
                }
            }
        }       

     
    }  
}

