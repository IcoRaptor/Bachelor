﻿using Framework.Debugging;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Messaging
{
    /// <summary>
    /// Global Messaging-System
    /// </summary>
    public sealed class MessagingSystem : SingletonAsComponent<MessagingSystem>
    {
        #region Variables

        private static bool _trigger;

        private const float _MAX_PROCESSING_TIME = -1f;

        private Dictionary<string, List<MessageHandler>> _listenerDict
            = new Dictionary<string, List<MessageHandler>>();

        private Queue<BaseMessage> _messageQueue = new Queue<BaseMessage>();

        private Stack<string> _addTypeStack = new Stack<string>();

        private Stack<MessageHandler> _addHandlerStack =
            new Stack<MessageHandler>();

        private Stack<string> _removeTypeStack = new Stack<string>();

        private Stack<MessageHandler> _removeHandlerStack =
            new Stack<MessageHandler>();

        #endregion

        #region Properties

        public static MessagingSystem Instance
        {
            get { return (MessagingSystem)_Instance; }
        }

        #endregion

        private void Update()
        {
            float timer = 0;

            // Iterate the messages or return early if it takes too long

            while (_messageQueue.Count > 0)
            {
#pragma warning disable 0162
                if (_MAX_PROCESSING_TIME > 0)
                    if (timer > _MAX_PROCESSING_TIME)
                        return;

                BaseMessage msg = _messageQueue.Dequeue();
                TriggerMessage(msg);

                if (_MAX_PROCESSING_TIME > 0)
                    timer += Time.unscaledDeltaTime;
#pragma warning restore 0162
            }
        }

        /// <summary>
        /// Calls handler functions
        /// </summary>
        /// <param name="msg">Message</param>
        /// <returns>Could the message be handled by the listener?</returns>
        private void TriggerMessage(BaseMessage msg)
        {
            _trigger = true;
            string msgName = msg.name;

            if (!_listenerDict.ContainsKey(msgName))
            {
                Debugger.LogFormat(LOG_TYPE.WARNING, "\"{0}\" is not registered!\n",
                    msgName);
                _trigger = false;
                return;
            }

            // Add listeners
            while (_addTypeStack.Count > 0)
                _listenerDict[_addTypeStack.Pop()].Add(_addHandlerStack.Pop());

            // Remove listeners
            while (_removeTypeStack.Count > 0)
                _listenerDict[_removeTypeStack.Pop()].Remove(_removeHandlerStack.Pop());

            // Iterate the handler functions
            for (int i = 0; i < _listenerDict[msgName].Count; ++i)
            {
                if (_listenerDict[msgName][i](msg))
                {
                    _trigger = false;
                    return;
                }
            }

            _trigger = false;
            return;
        }

        /// <summary>
        /// Adds a listener
        /// </summary>
        /// <param name="type">Message type</param>
        /// <param name="handler">Handler</param>
        /// <returns>Was the listener added successfully?</returns>
        public bool AttachListener(Type type, MessageHandler handler)
        {
            if (type == null)
            {
                Debugger.Log(LOG_TYPE.WARNING,
                    "AttachListener failed! Message was null!\n");
                return false;
            }

            string msgName = type.Name;

            if (!_listenerDict.ContainsKey(msgName))
                _listenerDict.Add(msgName, new List<MessageHandler>());

            if (_listenerDict[msgName].Contains(handler))
                return false;

            // If this was called from TriggerMessage add the listener later
            if (_trigger)
            {
                _addTypeStack.Push(msgName);
                _addHandlerStack.Push(handler);
                return true;
            }

            _listenerDict[msgName].Add(handler);
            return true;
        }

        /// <summary>
        /// Removes a listener
        /// </summary>
        /// <param name="type">Message type</param>
        /// <param name="handler">Handler</param>
        /// <returns>Was the listener removed successfully?</returns>
        public bool DetachListener(Type type, MessageHandler handler)
        {
            if (type == null)
            {
                Debugger.Log(LOG_TYPE.WARNING,
                    "DetachListener failed! Message was null!\n");
                return false;
            }

            string msgName = type.Name;

            if (!_listenerDict.ContainsKey(msgName))
                return false;

            if (!_listenerDict[msgName].Contains(handler))
                return false;

            // If this was called from TriggerMessage remove the listener later
            if (_trigger)
            {
                _removeTypeStack.Push(msgName);
                _removeHandlerStack.Push(handler);
                return true;
            }

            _listenerDict[msgName].Remove(handler);
            return true;
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="msg">Message</param>
        public void QueueMessage(BaseMessage msg)
        {
            if (!_listenerDict.ContainsKey(msg.name))
            {
                Debugger.LogFormat(LOG_TYPE.WARNING,
                    "{0} is not registered!\n", msg.name);
                return;
            }

            _messageQueue.Enqueue(msg);
        }

        /// <summary>
        /// Delegate for listeners
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Was the message handled?</returns>
        public delegate bool MessageHandler(BaseMessage message);
    }
}