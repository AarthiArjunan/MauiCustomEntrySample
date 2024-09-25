

















#if ANDROID
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Microsoft.Maui.Handlers;
using AndroidX.AppCompat.Widget;
using Android.Widget;
using Android.Text;

#endif

namespace CustomEntry
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
        }


    }

    public class CustomEntry : Entry
    {

    }


#if ANDROID

    public class AppCompatEditTextExt : AppCompatEditText
    {
        private CustomEntry entry;


        /// <summary>
        /// Initializes a new instance of the <see cref="AppCompatEditTextExt"/> class.
        /// </summary>
        /// <param name="context"></param>
        public AppCompatEditTextExt(Context context, CustomEntry? _entry) : base(context)
        {
            this.entry = _entry;
        }

        public override IInputConnection? OnCreateInputConnection(EditorInfo? outAttrs)
        {
            // Referenced from Xamarin UpdateInputType method in Android mapping for SfAutoCompleteMapping
            var connection = new CustomInputConnection(this, false);

            if (entry != null && outAttrs != null)
            {


                //if (entry.Keyboard == Keyboard.Numeric)
                //{
                //    outAttrs.InputType = InputTypes.ClassNumber | InputTypes.NumberFlagDecimal | InputTypes.NumberFlagSigned;
                //}
            }


            return connection;
        }
    }

    public partial class InputViewHandler: EntryHandler
    {
        private AppCompatEditText? textExt;
        private CustomEntry? _entry;

        protected override AppCompatEditText CreatePlatformView()
        {
            if (VirtualView == null)
            {
                throw new InvalidOperationException($"{nameof(VirtualView)} must be set to create a AppCompatEditText");
            }
            else
            {
                _entry = (CustomEntry)this.VirtualView;
            }

            textExt = new AppCompatEditTextExt(Context, _entry);
            return textExt;
        }

        protected override void DisconnectHandler(AppCompatEditText platformView)
        {
            base.DisconnectHandler(platformView);
            if (textExt != null)
            {
                
                textExt.Dispose();
                textExt = null;
            }
        }

        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class CustomInputConnection : BaseInputConnection
    {
        /// <summary>
        /// Custom input connection method.
        /// </summary>
        /// <param name="targetView">The target view.</param>
        /// <param name="fullEditor">The full editor.</param>
        public CustomInputConnection(Android.Views.View targetView, bool fullEditor) : base(targetView, fullEditor)
        {
        }

    }

#endif
}

