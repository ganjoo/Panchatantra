using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif
using DlibFaceLandmarkDetector;

namespace DlibFaceLandmarkDetectorExample
{
    /// <summary>
    /// Cat Detection Example
    /// An example of detecting cat face landmarks in a Texture2D image.
    /// </summary>
    public class CatDetectionExample : MonoBehaviour
    {
        /// <summary>
        /// The texture2D.
        /// </summary>
        public Texture2D texture2D;

        /// <summary>
        /// The FPS monitor.
        /// </summary>
        FpsMonitor fpsMonitor;

        /// <summary>
        /// The frontal_cat_face_svm_filepath.
        /// </summary>
        string frontal_cat_face_svm_filepath;

        /// <summary>
        /// The sp_cat_face_68_dat_filepath.
        /// </summary>
        string sp_cat_face_68_dat_filepath;

        #if UNITY_WEBGL && !UNITY_EDITOR
        Stack<IEnumerator> coroutines = new Stack<IEnumerator> ();
        #endif

        // Use this for initialization
        void Start ()
        {
            fpsMonitor = GetComponent<FpsMonitor> ();

            #if UNITY_WEBGL && !UNITY_EDITOR
            var getFilePath_Coroutine = GetFilePath ();
            coroutines.Push (getFilePath_Coroutine);
            StartCoroutine (getFilePath_Coroutine);
            #else
            frontal_cat_face_svm_filepath = Utils.getFilePath ("frontal_cat_face.svm");
            sp_cat_face_68_dat_filepath = Utils.getFilePath ("sp_cat_face_68.dat");
            Run ();
            #endif
        }

        #if UNITY_WEBGL && !UNITY_EDITOR
        private IEnumerator GetFilePath ()
        {
            var getFilePathAsync_frontal_cat_face_svm_filepath_Coroutine = Utils.getFilePathAsync ("frontal_cat_face.svm", (result) => {
                frontal_cat_face_svm_filepath = result;
            });
            coroutines.Push (getFilePathAsync_frontal_cat_face_svm_filepath_Coroutine);
            yield return StartCoroutine (getFilePathAsync_frontal_cat_face_svm_filepath_Coroutine);

        var getFilePathAsync_sp_cat_face_68_dat_filepath_Coroutine = Utils.getFilePathAsync ("sp_cat_face_68.dat", (result) => {
                sp_cat_face_68_dat_filepath = result;
            });
            coroutines.Push (getFilePathAsync_sp_cat_face_68_dat_filepath_Coroutine);
            yield return StartCoroutine (getFilePathAsync_sp_cat_face_68_dat_filepath_Coroutine);

            coroutines.Clear ();

            Run ();
        }
        #endif

        private void Run ()
        {
            gameObject.transform.localScale = new Vector3 (texture2D.width, texture2D.height, 1);
            Debug.Log ("Screen.width " + Screen.width + " Screen.height " + Screen.height + " Screen.orientation " + Screen.orientation);
            
            float width = gameObject.transform.localScale.x;
            float height = gameObject.transform.localScale.y;
            
            float widthScale = (float)Screen.width / width;
            float heightScale = (float)Screen.height / height;
            if (widthScale < heightScale) {
                Camera.main.orthographicSize = (width * (float)Screen.height / (float)Screen.width) / 2;
            } else {
                Camera.main.orthographicSize = height / 2;
            }

            FaceLandmarkDetector faceLandmarkDetector = new FaceLandmarkDetector (frontal_cat_face_svm_filepath, sp_cat_face_68_dat_filepath);
            faceLandmarkDetector.SetImage (texture2D);

            //detect face rects
            List<Rect> detectResult = faceLandmarkDetector.Detect ();

            foreach (var rect in detectResult) {
                Debug.Log ("face : " + rect);

                //detect landmark points
                List<Vector2> points = faceLandmarkDetector.DetectLandmark (rect);
                
                Debug.Log ("face points count : " + points.Count);
                foreach (var point in points) {
                    Debug.Log ("face point : x " + point.x + " y " + point.y);
                }

                //draw landmark points
                faceLandmarkDetector.DrawDetectLandmarkResult (texture2D, 0, 255, 0, 255);

            }

            //draw face rects
            faceLandmarkDetector.DrawDetectResult (texture2D, 255, 0, 0, 255, 3);

            faceLandmarkDetector.Dispose ();

            gameObject.GetComponent<Renderer> ().material.mainTexture = texture2D;

            if (fpsMonitor != null){
                fpsMonitor.Add ("dlib object detector", "frontal_cat_face.svm");
                fpsMonitor.Add ("dlib shape predictor", "sp_cat_face_68.dat");
                fpsMonitor.Add ("width", width.ToString());
                fpsMonitor.Add ("height", height.ToString());
                fpsMonitor.Add ("orientation", Screen.orientation.ToString());
            }
        }

        // Update is called once per frame
        void Update ()
        {

        }

        /// <summary>
        /// Raises the destroy event.
        /// </summary>
        void OnDestroy ()
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
            foreach (var coroutine in coroutines) {
                StopCoroutine (coroutine);
                ((IDisposable)coroutine).Dispose ();
            }
            #endif
        }

        /// <summary>
        /// Raises the back button click event.
        /// </summary>
        public void OnBackButtonClick ()
        {
            #if UNITY_5_3 || UNITY_5_3_OR_NEWER
            SceneManager.LoadScene ("DlibFaceLandmarkDetectorExample");
            #else
            Application.LoadLevel ("DlibFaceLandmarkDetectorExample");
            #endif
        }
    }
}