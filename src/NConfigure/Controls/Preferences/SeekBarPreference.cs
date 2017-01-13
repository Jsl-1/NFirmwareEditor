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
using Android.Preferences;
using static Android.Widget.SeekBar;
using Android.Util;
using Java.Lang;
using Android.Content.Res;
using Java.Interop;

namespace NToolbox.Controls.Preferences
{
    public class SeekBarPreference : Preference, IOnSeekBarChangeListener
    {
        //private SeekBar m_SeekBar;
        //private TextView m_ValueView;

        private int mMin;
        private int mProgress;
        private int mMax;
        private int mDivider;
        private bool mTrackingTouch;

        public SeekBarPreference(Context context) : base(context)
        {

        }

        public SeekBarPreference(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            _FillAttributes(context, attrs);
        }

        public SeekBarPreference(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            _FillAttributes(context, attrs);
        }

        private void _FillAttributes(Context context, IAttributeSet attrs)
        {
            var a = context.ObtainStyledAttributes(attrs, Resource.Styleable.EditIntPreference, 0, 0);
            try
            {

                mMax = a.GetInt(Resource.Styleable.EditIntPreference_max, Java.Lang.Integer.MaxValue);
                mMin = a.GetInt(Resource.Styleable.EditIntPreference_min, 0);
                mDivider = a.GetInt(Resource.Styleable.EditIntPreference_valuedivider, 1);
            }
            finally
            {
                a.Recycle();
            }

            LayoutResource = Resource.Layout.seekbar_preference;
        }


        protected override void OnBindView(View view)
        {
            base.OnBindView(view);

            SeekBar seekBar = view.FindViewById<SeekBar>(Resource.Id.seekbarpreference_seekbar);
            seekBar.SetOnSeekBarChangeListener(this);
            seekBar.Max = mMax;
            seekBar.Progress = mProgress;
            seekBar.Enabled = Enabled;
        }

        public string GetValueText()
        {
            if (mDivider != 0)
                return (Convert.ToSingle(mProgress) / Convert.ToSingle(mDivider)).ToString();
            else
                return mProgress.ToString();
        }

        protected override void OnSetInitialValue(bool restorePersistedValue, Java.Lang.Object defaultValue)
        {
            if(restorePersistedValue)
            {
                SetProgress(GetPersistedInt(mProgress));
            }
            else
            {
                var value = (Integer)defaultValue;
                if (defaultValue != null)
                {
                    SetProgress(value.IntValue());
                }
            }
        }

        //protected override Java.Lang.Object OnGetDefaultValue(TypedArray a, int index)
        //{
        //    return Integer.ValueOf(a.GetInt(index, 0));
        //}

        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
            if (fromUser && !mTrackingTouch)
            {
                SyncProgress(seekBar);
            }
        }

        public void OnStartTrackingTouch(SeekBar seekBar)
        {
            mTrackingTouch = true;
        }

        public void OnStopTrackingTouch(SeekBar seekBar)
        {
            mTrackingTouch = false;
            if (seekBar.Progress != mProgress)
            {
                SyncProgress(seekBar);
            }
        }

        public void SetMax(int max)
        {
            if (max != mMax)
            {
                mMax = max;
                NotifyChanged();
            }
        }

        public void SetProgress(int progress)
        {
            SetProgress(progress, true);
        }

        private void SetProgress(int progress, bool notifyChanged)
        {
            if (progress > mMax)
            {
                progress = mMax;
            }
            if (progress < 0)
            {
                progress = 0;
            }
            if (progress != mProgress)
            {
                mProgress = progress;
                PersistInt(progress);
                if (notifyChanged)
                {
                    NotifyChanged();
                }
            }
        }

        public int GetProgress()
        {
            return mProgress;
        }

        void SyncProgress(SeekBar seekBar)
        {
            int progress = seekBar.Progress;
            if (progress != mProgress)
            {
                if (CallChangeListener(progress))
                {
                    SetProgress(progress, false);
                }
                else
                {
                    seekBar.Progress = mProgress;
                }
            }
        }

        protected override IParcelable OnSaveInstanceState()
        {
            var baseState = base.OnSaveInstanceState();

            if (Persistent)
            {
                // No need to save instance state since it's persistent
                return baseState;
            }

            // Save the instance state
            var myState = new SavedState(baseState);
            myState.progress = mProgress;
            myState.max = mMax;
            myState.divider = mDivider;

            return myState;
        }


        private class SavedState : BaseSavedState
        {
        public int progress;
        public int max;
        public int divider;

        public SavedState(Parcel source) : base(source)
        {
            // Restore the click counter
            progress = source.ReadInt();
            max = source.ReadInt();
            divider = source.ReadInt();
        }

        public SavedState(IParcelable superState) : base(superState)
        {

        }

        public override void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            base.WriteToParcel(dest, flags);
            dest.WriteInt(progress);
            dest.WriteInt(max);
            dest.WriteInt(divider);
        }


        [ExportField("CREATOR")]
        static SavedStateCreator InititalizeCreator()
        {
            return new SavedStateCreator();
        }

        public class SavedStateCreator : Java.Lang.Object, IParcelableCreator
        {
            public Java.Lang.Object CreateFromParcel(Parcel source)
            {
                return new SavedState(source);
            }

            public Java.Lang.Object[] NewArray(int size)
            {
                return new SavedState[size];
            }
        }

        //@SuppressWarnings("unused")
        //public static final Parcelable.Creator<SavedState> CREATOR =
        //        new Parcelable.Creator<SavedState>()
        //        {
        //    public SavedState createFromParcel(Parcel in)
        //{
        //    return new SavedState(in);
        //}
        //public SavedState[] newArray(int size)
        //{
        //    return new SavedState[size];
        //}
        //};
    }
}
}