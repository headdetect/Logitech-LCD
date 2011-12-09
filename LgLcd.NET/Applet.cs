﻿using System;
using System.Collections.Generic;
using System.Text;
using LgLcdNET;
using System.ComponentModel;

namespace LgLcdNET
{
	public abstract class Applet
	{
		// The connection handle
		public int Handle { get; private set; }

		// Properties
		public string FriendlyName { get; private set; }
		public bool Autostartable { get; private set; }
		public AppletCapabilities CapabilitiesSupported { get; private set; }

		// Notifications
		protected abstract void OnDeviceArrival(DeviceType deviceType);
		protected abstract void OnDeviceRemoval(DeviceType deviceType);
		protected abstract void OnAppletEnabled();
		protected abstract void OnAppletDisabled();
		protected abstract void OnCloseConnection();

		// Called when the user wishes to configure our application from the LCDMon
		protected virtual void OnConfigure() { }

		public void Connect(string friendlyName, bool autostartable, AppletCapabilities appletCaps)
		{
			FriendlyName = friendlyName;
			Autostartable = autostartable;
			CapabilitiesSupported = appletCaps;
			ConnectContextEx ctx = new ConnectContextEx()
			{
				AppFriendlyName = friendlyName,
				AppletCapabilitiesSupported = appletCaps,
				IsAutostartable = autostartable,
				IsPersistent = true, // deprecated as of 3.00
				OnConfigure = new ConfigureContext()
				{
					Context = IntPtr.Zero,
					OnConfigure = new ConfigureDelegate(ConfigureHandler),
				},
				OnNotify = new NotificationContext()
				{
					Context = IntPtr.Zero,
					OnNotification = new NotificationDelegate(NotifyHandler),
				},
				Reserved1 = 0,
			};
			ReturnValue error = LgLcd.ConnectEx(ref ctx);
			if (error != ReturnValue.ErrorSuccess)
			{
				if (error == ReturnValue.ErrorServiceNotActive)
				{
					throw new Exception("lgLcdInit() has not been called yet.");
				}
				else if (error == ReturnValue.ErrorInvalidParameter)
				{
					throw new ArgumentException("friendlyName must not be null");
				}
				else if (error == ReturnValue.ErrorFileNotFound)
				{
					throw new Exception("LCDMon is not running on the system.");
				}
				else if (error == ReturnValue.ErrorAlreadyExists)
				{
					throw new Exception("The same client is already connected.");
				}
				else if (error == ReturnValue.RcpXWrongPipeVersion)
				{
					throw new Exception("LCDMon does not understand the protocol.");
				}
				throw new Win32Exception((int)error);
			}
			Handle = ctx.Connection;
		}

		public void Disconnect()
		{
			ReturnValue error = LgLcd.Disconnect(Handle);
			if (error != ReturnValue.ErrorSuccess)
			{
				if (error == ReturnValue.ErrorServiceNotActive)
				{
					throw new Exception("lgLcdInit() has not been called yet.");
				}
				else if (error == ReturnValue.ErrorInvalidParameter)
				{
					throw new Exception("Not connected.");
				}
				throw new Win32Exception((int)error);
			}
		}

		private int ConfigureHandler(int connection, IntPtr context)
		{
			OnConfigure();
			return 0;
		}

		private int NotifyHandler(
			int connection,
			IntPtr context,
			NotificationCode notificationCode,
			int notifyParam1,
			int notifyParam2,
			int notifyParam3,
			int notifyParam4)
		{
			switch (notificationCode)
			{
				case NotificationCode.DeviceArrival:
					OnDeviceArrival((DeviceType)notifyParam1);
					break;
				case NotificationCode.DeviceRemoval:
					// All devices of the given type got disabled
					OnDeviceRemoval((DeviceType)notifyParam1);
					break;
				case NotificationCode.AppletEnabled:
					OnAppletEnabled();
					break;
				case NotificationCode.AppletDisabled:
					OnAppletDisabled();
					break;
				case NotificationCode.CloseConnection:
					OnCloseConnection();
					break;
			}
			return 0;
		}
	}
}
