using UnityEngine;

namespace InternetService
{
    public static class InternetManager //: MonoBehaviour
    {
        public static bool IsInternetWorking => CheckIsInternetConnected();
        public static NetworkTypes NetworkType => CheckInternetType();

        public enum NetworkTypes
        {
            None,
            Mobile,
            WiFi
        }

        private static bool CheckIsInternetConnected()
        {
            return Application.internetReachability is NetworkReachability.ReachableViaCarrierDataNetwork
                                                    or NetworkReachability.ReachableViaLocalAreaNetwork;
        }

        private static NetworkTypes CheckInternetType()
        {
            return Application.internetReachability switch
            {
                NetworkReachability.NotReachable => NetworkTypes.None,
                NetworkReachability.ReachableViaCarrierDataNetwork => NetworkTypes.Mobile,
                NetworkReachability.ReachableViaLocalAreaNetwork => NetworkTypes.WiFi,
                _ => NetworkTypes.None
            };
        }
    }
}