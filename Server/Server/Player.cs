using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Server
{
    class Player
    {
        public int id;
        public string username;

        public Vector3 position;
        public Quaternion rotation;

        public float[] samples;

        private float moveSpeed = 5f / Constants.TICKS_PER_SECOND;
        private bool[] inputs;
        DateTime lastAudioSend = DateTime.Now.AddSeconds(1);

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;

            inputs = new bool[4];
            samples = new float[1];
        }

        public void Update()
        {
            Vector2 _inputDirection = Vector2.Zero;
            if (inputs[0])
            {
                _inputDirection.Y += 1;
            }
            if (inputs[1])
            {
                _inputDirection.X -= 1;
            }
            if (inputs[2])
            {
                _inputDirection.Y -= 1;
            }
            if (inputs[3])
            {
                _inputDirection.X += 1;
            }

            Move(_inputDirection);
            if (lastAudioSend < DateTime.Now)
            {
                lastAudioSend = lastAudioSend.AddSeconds(1);
                ServerSend.Audio(this);
            }
        }

        private void Move(Vector2 _inputDirection)
        {
            Vector3 _forward = Vector3.Transform(new Vector3(0, 0, 1), rotation);
            Vector3 _right = Vector3.Normalize(Vector3.Cross(_forward, new Vector3(0, 1, 0)));

            Vector3 _moveDirection = _right * _inputDirection.X + _forward * _inputDirection.Y;
            position += _moveDirection * moveSpeed;

            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
        }

        public void SetInput(bool[] _inputs, Quaternion _rotation)
        {
            inputs = _inputs;
            rotation = _rotation;
        }

        public void SetAudio(float[] _samples)
        {
            samples = _samples;
        }
    }
}
