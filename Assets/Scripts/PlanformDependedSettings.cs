namespace Assets.Scripts
{
    public static class PlanformDependedSettings
    {

        #if UNITY_ANDROID

        //public const string StorePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAjNt0mFbvqqDklpRYZtS4QKWV3dy0AwH5i9r9/hkxm0d2BM83vbZEe0/csOoJIdW5tBDbMHgf5opHvw7Pya9oRRtXQU7LTgAFZMo/Y4U5cc7k1n/Msc8EwjwJNfOfdAl/QjZmocRC876gsE3KR/bjskzk2YGlpBRb3F1+/t5B60c/wKjD1WTmKE+o/MUMNE+x/4HDim1dkkNWEI67DKr809JQGMe2jGHqyEX3et9o59Y+WfnrHs6iP9ZXiXH+uUCUuG9viuhJvC157mTIWTuI/mAFmhnUL5QzHd6COLzEFc5K56Fs7zA2fijND/LD4++oLT2QdzJ8cgZteyj+7Feq7QIDAQAB";
        //public const string StorePublicKeyXml = "<RSAKeyValue><Modulus>jNt0mFbvqqDklpRYZtS4QKWV3dy0AwH5i9r9/hkxm0d2BM83vbZEe0/csOoJIdW5tBDbMHgf5opHvw7Pya9oRRtXQU7LTgAFZMo/Y4U5cc7k1n/Msc8EwjwJNfOfdAl/QjZmocRC876gsE3KR/bjskzk2YGlpBRb3F1+/t5B60c/wKjD1WTmKE+o/MUMNE+x/4HDim1dkkNWEI67DKr809JQGMe2jGHqyEX3et9o59Y+WfnrHs6iP9ZXiXH+uUCUuG9viuhJvC157mTIWTuI/mAFmhnUL5QzHd6COLzEFc5K56Fs7zA2fijND/LD4++oLT2QdzJ8cgZteyj+7Feq7Q==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        //public const string UnityAdsId = "80967";
        //public static string StoreName = OpenIAB_Android.STORE_GOOGLE;
        public const string StoreLink = "https://play.google.com/store/apps/details?id=com.BullshitBingo";
        //public const string HippoLink = "https://play.google.com/store/apps/developer?id=Hippo";
        //public const float GameServicesDelay = 0;

        #endif

        #if UNITY_IPHONE

        //public const string StorePublicKey = "8034c613f5494922b5da8f26bf2dca95";
        //public const string UnityAdsId = "80966";
        //public static string StoreName = OpenIAB_iOS.STORE;
        public const string StoreLink = "itms-apps://itunes.apple.com/app/1049802406";
        //public const string HippoLink = "itms-apps://itunes.apple.com/ru/artist/farida-yarullina/id846630437";
        //public const float GameServicesDelay = 5;

        #endif
    }
}