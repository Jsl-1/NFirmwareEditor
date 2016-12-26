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
using Android.Support.V7.App;
using Android.Support.V4.Widget;

namespace NToolbox
{
    internal class MainMenuActionBarDrawerToggle : ActionBarDrawerToggle
    {
        MainActivity owner;

        public MainMenuActionBarDrawerToggle(MainActivity activity, DrawerLayout layout, int openRes, int closeRes)
            : base(activity, layout, openRes, closeRes)
        {
            owner = activity;           
        }

        public override void OnDrawerClosed(View drawerView)
        {
            owner.ActionBar.Title = owner.Title;
            owner.InvalidateOptionsMenu();
        }

        public override void OnDrawerOpened(View drawerView)
        {
            owner.ActionBar.Title = $"{owner.Title}";
            owner.InvalidateOptionsMenu();
        }
    }   
}