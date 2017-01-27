using System;

namespace Instagram.Pages
{
    public static class InstPages
    {      

        private static T OpenPage<T>() where T : new()
        {            
            return (T)Activator.CreateInstance(typeof(T));
        }


        public static BaseInstPage BaseInstP => OpenPage<BaseInstPage>();

        public static InstagramSignUpPage InstagramSignUpP => OpenPage<InstagramSignUpPage>();
    }
}
