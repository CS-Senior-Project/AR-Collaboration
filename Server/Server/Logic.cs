﻿/*
Handles ensuring server updates are continuous.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class Logic
    {
        public static void Update()
        {
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    _client.player.Update();
                }
            }

            ThreadManager.UpdateMain();
        }
    }
}
