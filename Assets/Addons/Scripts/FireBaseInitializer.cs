using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Addons.Scripts
{
    public class FireBaseInitializer : MonoBehaviour
    {
        private void Start()
        {
            FetchDataAsync();
        
            SampleWebView.Instance.url = FirebaseRemoteConfig.DefaultInstance.GetValue("url").StringValue;

            if (SampleWebView.Instance.url.Length != 0) return;

            SceneManager.LoadScene(1);
        }
    
        public Task FetchDataAsync() {
            Debug.Log("Fetching data...");
            Task fetchTask =
                FirebaseRemoteConfig.DefaultInstance.FetchAsync(
                    TimeSpan.Zero);
            return fetchTask.ContinueWithOnMainThread(FetchComplete);
        }

        private void FetchComplete(Task fetchTask) 
        {
            if (!fetchTask.IsCompleted) {
                Debug.LogError("Retrieval hasn't finished.");
                return;
            }

            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var info = remoteConfig.Info;
            if(info.LastFetchStatus != LastFetchStatus.Success) {
                Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
                return;
            }

            remoteConfig.ActivateAsync()
                .ContinueWithOnMainThread(
                    task => {
                        Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");
                    });
        }
    }
}
