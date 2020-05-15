/**
 * Copyright (c) 2017 The Campfire Union Inc - All Rights Reserved.
 *
 * Licensed under the MIT license. See LICENSE file in the project root for
 * full license information.
 *
 * Email:   info@campfireunion.com
 * Website: https://www.campfireunion.com
 * 
 * File editted by the OSU ARC Senior Project Team
 * Carson Pemble
 * May 12, 2020
 * 
 * This was a file from the VRKeys asset that we have copied into our own 
 * project and adjusted it to the needs of our own personal project.
 * 
 */

using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Collections;
using VRKeys;

namespace VRKeys
{

    public class OurKeyboard : MonoBehaviour
    {

        /// <summary>
        /// Reference to the VRKeys keyboard.
        /// </summary>
        public Keyboard keyboard;

        public TextMesh newtext;

        private void OnEnable()
        {

            // Automatically creating camera here to show how
            GameObject camera = new GameObject("Main Camera");
            Camera cam = camera.AddComponent<Camera>();
            cam.nearClipPlane = 0.1f;
            camera.AddComponent<AudioListener>();

            // Improves event system performance
            Canvas canvas = keyboard.canvas.GetComponent<Canvas>();
            canvas.worldCamera = cam;

            keyboard.Enable();
            //keyboard.SetPlaceholderMessage("Make a note");

            keyboard.OnUpdate.AddListener(HandleUpdate);        //
            keyboard.OnSubmit.AddListener(HandleSubmit);        // Set up Listeners
            keyboard.OnCancel.AddListener(HandleCancel);        //
        }

        private void OnDisable()
        {
            keyboard.OnUpdate.RemoveListener(HandleUpdate);
            keyboard.OnSubmit.RemoveListener(HandleSubmit);
            keyboard.OnCancel.RemoveListener(HandleCancel);

            keyboard.Disable();
        }

        /// <summary>
        /// Press space to show/hide the keyboard.
        ///
        /// Press Q for Qwerty keyboard, D for Dvorak keyboard, and F for French keyboard.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))            // Space still works to disable the keyboard
            {
                if (keyboard.disabled)
                {
                    keyboard.Enable();
                }
                else
                {
                    keyboard.Disable();
                }
            }

            if (keyboard.disabled)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                keyboard.SetLayout(KeyboardLayout.Qwerty);
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                keyboard.SetLayout(KeyboardLayout.French);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                keyboard.SetLayout(KeyboardLayout.Dvorak);
            }
        }

        /// <summary>
        /// Hide the validation message on update. Connect this to OnUpdate.
        /// </summary>
        public void HandleUpdate(string text)
        {
            keyboard.HideValidationMessage();
        }

        /// <summary>
        /// Validate the email and simulate a form submission. Connect this to OnSubmit.
        /// </summary>
        public void HandleSubmit(string text)
        {
            //keyboard.DisableInput ();

            newtext.text = text;
            keyboard.SetText("");

        }

        public void HandleCancel()
        {
            Debug.Log("Cancelled keyboard input!");         // For debugging purposes
        }
    }
}